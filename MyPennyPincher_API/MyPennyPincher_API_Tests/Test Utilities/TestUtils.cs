using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;

namespace MyPennyPincher_API_Tests.Test_Utilities;

public class TestUtils
{
    public static MyPennyPincherDbContext GenerateInMemoryDB()
    {
        var options = new DbContextOptionsBuilder<MyPennyPincherDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        return new MyPennyPincherDbContext(options);
    }

    public static string GenerateRandomEmail()
    {
        var randomPrefix = Guid.NewGuid().ToString().Substring(0, 8); // or Random
        return $"{randomPrefix}@example.com";
    }

}
