using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaccoSystem.Startup))]
namespace SaccoSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
