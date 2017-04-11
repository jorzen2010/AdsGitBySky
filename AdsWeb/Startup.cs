using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdsWeb.Startup))]
namespace AdsWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
