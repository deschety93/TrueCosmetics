using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrueCosmetics.BootstrapApp.Startup))]
namespace TrueCosmetics.BootstrapApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
