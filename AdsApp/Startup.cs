using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdsApp.Startup))]
namespace AdsApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
