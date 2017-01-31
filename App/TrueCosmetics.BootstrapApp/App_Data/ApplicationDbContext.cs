using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenderCategoryPicture>()
                .HasKey(g => new { g.CategoryId, g.GenderId})
                .HasRequired(g => g.GenderCategory)
                .WithOptional(g => g.Picture)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProductPicture>()
                .HasKey(g => g.Id)
                .HasRequired(g => g.Product)
                .WithMany(g => g.Pictures)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<GenderCategory> GenderCategories { get; set; }

        public DbSet<GenderCategoryPicture> GenderCategoryPictures { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPicture> ProductPictures { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<ApplicationUserAddress> ApplicationUserAddresses { get; set; }
    }
}
