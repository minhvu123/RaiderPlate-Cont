using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RaiderPlate_Cont.Startup))]
namespace RaiderPlate_Cont
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
