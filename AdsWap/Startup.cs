using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdsWap.Startup))]
namespace AdsWap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
