using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tofix.Startup))]
namespace tofix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
