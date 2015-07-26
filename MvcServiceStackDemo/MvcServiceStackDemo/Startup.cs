using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcServiceStackDemo.Startup))]
namespace MvcServiceStackDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
