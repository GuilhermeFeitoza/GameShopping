using Microsoft.EntityFrameworkCore;

namespace GameShopping.ProductAPI.Model.Context
{
    public class MySqlContext : DbContext
    {

        public MySqlContext()
        {
                
        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
            

        }


        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Red dead redemption 2",
                Price = 200,
                Description = "RD2",
                ImageURL = "",
                CategoryName = "Games",
             });
        }

    }
}
