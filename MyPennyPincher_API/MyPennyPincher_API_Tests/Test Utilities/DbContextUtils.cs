using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;

namespace MyPennyPincher_API_Tests.Test_Utilities;

public class DbContextUtils
{
    public static MyPennyPincherDbContext GenerateInMemoryDB()
    {
        var options = new DbContextOptionsBuilder<MyPennyPincherDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        return new MyPennyPincherDbContext(options);
    }
}
