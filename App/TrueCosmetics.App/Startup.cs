using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrueCosmetics.App.Startup))]
namespace TrueCosmetics.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
