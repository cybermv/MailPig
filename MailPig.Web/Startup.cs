using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MailPig.Web.Startup))]
namespace MailPig.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
