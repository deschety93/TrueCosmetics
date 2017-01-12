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

        public IDbSet<Gender> Genders { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<GenderCategory> GenderCategories { get; set; }

        public IDbSet<GenderCategoryPicture> GenderCategoryPictures { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<ProductPicture> ProductPictures { get; set; }

        public IDbSet<Manufacturer> Manufacturers { get; set; }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<OrderDetail> OrderDetails { get; set; }

        public IDbSet<OrderStatus> OrderStatuses { get; set; }

        public IDbSet<ApplicationUserAddress> ApplicationUserAddresses { get; set; }
    }
}
