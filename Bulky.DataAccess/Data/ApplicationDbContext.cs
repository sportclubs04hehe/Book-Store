using Bukly.Models;
using Microsoft.EntityFrameworkCore;

namespace WebBanSach.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id =1,
                    Title= "Book 1",
                    Description = "Combo Sách Đầu Tư Bất Động Sản: Bất Động Sản Căn Bản + Donald Trump - Chiến Lược Đầu Tư Bất Động Sản (Tái Bản) + Trump - 100 Lời Khuyên Đầu Tư Bất Động Sản Khôn Ngoan Nhất + Đầu Tư Bất Động Sản - Cách Thức Khởi Nghiệp Và Thu Lợi Nhuận Lớn",
                    Author = "Jone Wick",
                    IBSN = "W874UI",
                    ListPrice = 99,
                    Price= 90,
                    Price50 = 85,
                    Price100= 80,
                    CategoryId= 1,
                    Image = ""
                },
                   new Product
                   {
                       Id = 2,
                       Title = "Emotional Intelligence (Reissue 2021)",
                       Description = "Everyone knows that a high IQ score does not guarantee a successful, happy and virtuous life",
                       Author = "Daniel Goleman",
                       IBSN = "JT87D02L",
                       ListPrice = 200,
                       Price = 170,
                       Price50 = 150,
                       Price100 = 80,
                       CategoryId= 3,
                       Image = ""
                   }
                );
        }

    }
}
