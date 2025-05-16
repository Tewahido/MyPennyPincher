using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyPennyPincherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));

builder.Services.AddControllers();

builder.Services.AddScoped<AuthService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapScalarApiReference();

app.MapControllers();

app.Run();
