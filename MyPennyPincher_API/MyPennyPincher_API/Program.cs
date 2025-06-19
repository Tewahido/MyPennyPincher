using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Services;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Repositories;
using MyPennyPincher_API.Services.Interfaces;
using System.Text.Json;
using MyPennyPincher_API.CustomExceptionMiddleware;
using MyPennyPincher_API.Models.ConfigModels;
using Microsoft.AspNetCore.RateLimiting;
using MyPennyPincher_API.Models.DTO;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<MyPennyPincherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
}

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddOpenApi();

JwtOptions jwtOptions = builder.Configuration.GetSection(JwtOptions.JwtSection)
                        .Get<JwtOptions>()!;

builder.Services.AddSingleton(jwtOptions);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "User not authenticated.",
                statusCode = 401
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsJsonAsync(json);
        }
    };
});

SlidingRateLimitOptions myOptions  = builder.Configuration.GetSection(SlidingRateLimitOptions.SlidingRateLimitSection)
                                                    .Get<SlidingRateLimitOptions>()!;

var slidingPolicy = "sliding";

builder.Services.AddRateLimiter(options =>
{
    options.AddSlidingWindowLimiter(policyName: slidingPolicy, options =>
    {
        options.PermitLimit = myOptions.PermitLimit;
        options.Window = TimeSpan.FromSeconds(myOptions.Window);
        options.SegmentsPerWindow = myOptions.SegmentsPerWindow;
        options.QueueLimit = myOptions.QueueLimit;
    });

    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:3000";
        context.HttpContext.Response.Headers["Access-Control-Allow-Credentials"] = "true";

        ErrorResponse errorResponse = new ErrorResponse
        {
            StatusCode = StatusCodes.Status429TooManyRequests,
            Message = "Too many attempts, try again shortly"
        };

        await context.HttpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
    };
    
});

builder.Services.AddOpenApi();

builder.Services.AddAuthorization();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}

app.UseRateLimiter();

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapScalarApiReference();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "api");
});

app.UseCookiePolicy();

app.UseMiddleware<CustomExceptionMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }