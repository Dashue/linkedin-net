using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LinkedIn.Api.Client.Samples.ASPNetMvc5.Startup))]
namespace LinkedIn.Api.Client.Samples.ASPNetMvc5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
