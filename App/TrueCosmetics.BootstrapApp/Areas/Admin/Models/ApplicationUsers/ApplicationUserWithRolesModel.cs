using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrueCosmetics.Data.Models;

namespace TrueCosmetics.BootstrapApp.Areas.Admin.Models.ApplicationUsers
{
    public class ApplicationUserWithRolesModel
    {
        public static Func<ApplicationUser, IEnumerable<SelectListItem>, ApplicationUserWithRolesModel> From = (x, y) =>
        {
            ApplicationUserWithRolesModel result = new ApplicationUserWithRolesModel();
            result.Id = x.Id;
            result.Email = x.Email;
            result.Password = x.PasswordHash;
            result.PhoneNumber = x.PhoneNumber;
            result.Roles = x.Roles.Join(y, f => f.RoleId, s => s.Value, (f, s) => s).ToList();
            result.RoleIds = result.Roles.Select(f => f.Value).ToList();
            return result;
        };

        public static Func<IdentityRole, SelectListItem> ConvertRoles = x => new SelectListItem()
        {
            Value = x.Id,
            Text = x.Name,
            Selected = true
        };

        public static async Task To( ApplicationUserWithRolesModel x, ApplicationUser y, ApplicationUserManager m, IEnumerable<SelectListItem> r)
        {
            y.Email = x.Email;
            y.PhoneNumber = x.PhoneNumber;
            var result = m.RemoveFromRolesAsync(y.Id, m.GetRolesAsync(y.Id).Result.ToArray()).Result;
            if (result.Succeeded)
                await m.AddToRolesAsync(y.Id, r.Where(z => x.RoleIds.Contains(z.Value)).Select(z => z.Text).ToArray());
        }

        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        public string PhoneNumber { get; set; }

        public List<SelectListItem> Roles { get; set; }

        public List<string> RoleIds { get; set; }
    }
}