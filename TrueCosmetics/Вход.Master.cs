using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace TrueCosmetics
{
    public partial class Начало : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void LoginView2_ViewChanged(object sender, EventArgs e)
        {

        }

        protected void NachaloBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Начало.aspx");
        }

        protected void NachaloBtn_Click1(object sender, EventArgs e)
        {

        }

        protected void MoqtProfilBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Моят профил.aspx");
        }

        protected void KolichkaBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Количка.aspx");
        }

        protected void IstoriqNaPorychkataBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/История на поръчката.aspx");
        }

        protected void Uslugibtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Услуги.aspx");
        }

        protected void ZaNasbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/За нас.aspx");
        }

        protected void KontaktiBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Контакти.aspx");
        }

        protected void KartaNaSaitaBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Карта на сайта.aspx");
        }
    }
}