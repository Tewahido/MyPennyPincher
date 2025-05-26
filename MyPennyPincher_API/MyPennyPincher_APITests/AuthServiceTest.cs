using Microsoft.Extensions.Configuration;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Services;
using NSubstitute;

namespace MyPennyPincher_APITests;

public class Tests
{
    private AuthService _authService;
    private MyPennyPincherDbContext _mockContext;
    private IConfiguration _mockConfiguration;

    [SetUp]
    public void Setup()
    {
        _mockContext = Substitute.For<MyPennyPincherDbContext>();
        _mockConfiguration = Substitute.For<IConfiguration>();
        _authService = new AuthService(_mockContext, _mockConfiguration);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [TearDown]
    public void Teardown()
    {
        _mockContext.Dispose();
    }
}
