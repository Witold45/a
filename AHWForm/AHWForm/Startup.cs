using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AHWForm.Startup))]
namespace AHWForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
