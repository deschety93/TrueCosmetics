using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Models.Products
{
    public class ProductWithPicModel
    {
        public static Func<Product, ProductWithPicModel> From = x =>
        {
            ProductWithPicModel result = new ProductWithPicModel()
            {
                Id = x.Id, Name = x.Name, Description = x.Description, Price = x.Price, QuantityInStore = x.QuanityInStore,
                GenderCategoriesIds = x.GenderCategories != null ? x.GenderCategories.Select(y => y.CategoryId + " " + y.GenderId) : null,
                OrderDetailsIds = x.OrderDetails != null ? x.OrderDetails.Select( y => y.OrderId + " " + y.ProductId) : null,
                ManufacturerId = x.ManufacturerId,
                ManufacturerName = x.Manufacturer != null ? x.Manufacturer.Name : ""
            };

            if(x.GenderCategories != null)
            {
                result.GenderCategoriesNames = new List<SelectListItem>();
                foreach (var item in x.GenderCategories)
                {
                    result.GenderCategoriesNames.Add(new SelectListItem()
                    {
                        Value = item.CategoryId + " " + item.GenderId,
                        Text = item.Category.Name + " за " + item.Gender.Name
                    });
                }
            }

            return result;
        };

        public async Task To(Product pr, IQueryable<GenderCategory> gc, IQueryable<Manufacturer> m, HttpServerUtilityBase server)
        {
            pr.Name = Name;
            pr.Description = Description;
            pr.Price = Price;
            pr.QuanityInStore = QuantityInStore;
            if(GenderCategoriesIds != null)
            {
                foreach (var item in GenderCategoriesIds)
                {
                    var ids = item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => int.Parse(x)).ToArray();
                    int catId = ids[0], genId = ids[1];
                    var g = await gc.Include(x => x.Category)
                        .Include(x => x.Gender)
                        .FirstOrDefaultAsync(x => x.CategoryId == catId && x.GenderId == genId);
                    if (g != null)
                    {
                        pr.GenderCategories.Add(g);
                    }
                }
            }
            var ma = await m.FirstOrDefaultAsync(x => x.Id == ManufacturerId);
            if(ma != null)
            {
                pr.Manufacturer = ma;
            }
            SetPictures(pr, server);
        }

        private void SetPictures(Product pr, HttpServerUtilityBase server)
        {
            if (PostedFiles != null && PostedFiles.First() != null)
            {
                pr.Pictures.Clear();
                foreach (var item in PostedFiles)
                {
                    string path = "~/Content/images/Products/" + Path.GetFileName(item.FileName);
                    item.SaveAs(server.MapPath(path));
                    pr.Pictures.Add(new ProductPicture()
                    {
                        Product = pr,
                        Path = path,
                        Name = item.FileName
                    });
                }
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStore { get; set; }

        public IEnumerable<string> GenderCategoriesIds { get; set; }

        public List<SelectListItem> GenderCategoriesNames { get; set; }

        public IEnumerable<HttpPostedFileBase> PostedFiles { get; set; }

        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public IEnumerable<string> OrderDetailsIds { get; set; }
    }
}