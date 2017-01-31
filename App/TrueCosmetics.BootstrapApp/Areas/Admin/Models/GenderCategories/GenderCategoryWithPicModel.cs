using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Models.GenderCategories
{
    public class GenderCategoryWithPicModel
    {
        public static Func<GenderCategory, GenderCategoryWithPicModel> From = x =>
        {
            GenderCategoryWithPicModel result = new GenderCategoryWithPicModel()
            {
                CategoryId = x.CategoryId,
                GenderId = x.GenderId,
                CategoryName = x.Category.Name,
                GenderName = x.Gender.Name,
                PicPath = x.Picture != null ? x.Picture.Path : "",
                PicName = x.Picture != null ? x.Picture.Name : ""
            };
            return result;
        };

        public int CategoryId { get; set; }

        public int GenderId { get; set; }

        public string CategoryName { get; set; }

        public string GenderName { get; set; }

        public HttpPostedFileBase PostedFile { get; set; }

        public string PicPath { get; set; }

        public string PicName { get; set; }
    }
}