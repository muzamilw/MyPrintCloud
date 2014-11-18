using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MPC.Webstore.Startup))]
namespace MPC.Webstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
