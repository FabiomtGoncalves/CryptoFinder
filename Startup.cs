using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CryptoExchange.Startup))]
namespace CryptoExchange
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
