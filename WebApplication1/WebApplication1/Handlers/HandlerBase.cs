using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YouVideo.DomainLayer;

namespace WebApplication1.Handlers
{
    public abstract class HandlerBase : IHttpHandler
    {
        public DomainFacade DomainFacade { 
            get
            { return ((Global)HttpContext.Current.ApplicationInstance).DomainFacade;
            }
        }
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequestCore(context);
        }

        protected abstract void ProcessRequestCore(HttpContext context);
        
    }
}