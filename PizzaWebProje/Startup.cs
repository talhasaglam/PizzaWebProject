using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzaWebProje.Startup))]
namespace PizzaWebProje
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
