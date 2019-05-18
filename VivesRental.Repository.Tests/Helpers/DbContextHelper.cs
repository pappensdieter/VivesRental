using System.Data.Entity;

namespace VivesRental.Repository.Tests.Helpers
{
    public static class DbContextHelper
    {
        public static void Clear(DbContext context)
        {
            context.Database.ExecuteSqlCommand("delete from [RentalOrderLine]");
            context.Database.ExecuteSqlCommand("delete from [RentalOrder]");
            context.Database.ExecuteSqlCommand("delete from [RentalItem]");
            context.Database.ExecuteSqlCommand("delete from [Item]");
            context.Database.ExecuteSqlCommand("delete from [User]");
        }
    }
}
