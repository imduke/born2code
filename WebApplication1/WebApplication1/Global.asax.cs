using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using YouVideo.DomainLayer;
namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {
        public DomainFacade DomainFacade { get; private set; }
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        public override void Init()
        {
            base.Init();
            DomainFacade = new DomainFacade();
        }
    }
}