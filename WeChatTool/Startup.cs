using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeChatTool.Startup))]
namespace WeChatTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
