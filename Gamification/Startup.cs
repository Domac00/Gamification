using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gamification.Startup))]
namespace Gamification
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
