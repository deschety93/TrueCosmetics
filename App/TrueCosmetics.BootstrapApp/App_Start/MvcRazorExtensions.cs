using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrueCosmetics.BootstrapApp
{
    public static class MvcRazorExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string name, object htmlAttributes = null)
        {
            TagBuilder img = new TagBuilder("img");
            img.Attributes.Add("src", name);
            if(htmlAttributes != null)
            {
                foreach (var prop in htmlAttributes.GetType().GetProperties())
                {
                    img.Attributes.Add(prop.Name, prop.GetValue(htmlAttributes).ToString());
                }
            }
            return new MvcHtmlString(img.ToString());
        }
    }
}