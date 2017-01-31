using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Models.Manufacturers
{
    public class ManufacturerWithPicModel
    {
        public static Func<Manufacturer, ManufacturerWithPicModel> From = x =>
        {
            ManufacturerWithPicModel result = new ManufacturerWithPicModel()
            {
                Id = x.Id, Description = x.Description, Name = x.Name, PicPath = x.LogoPicPath, PicName = x.Name
            };

            return result;
        };

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string PicPath { get; set; }

        public string PicName { get; set; }

        public HttpPostedFileBase PostedFile { get; set; }
    }
}