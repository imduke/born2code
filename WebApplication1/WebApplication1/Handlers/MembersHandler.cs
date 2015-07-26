using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YouVideo.Models;

namespace WebApplication1.Handlers
{
    public class MembersHandler : HandlerBase
    {
        protected override void ProcessRequestCore(HttpContext context)
        {
            if (context.Request.HttpMethod == "GET")
            {
                dynamic output = null;
                if (!string.IsNullOrEmpty(context.Request.PathInfo))
                {
                    var memberId = int.Parse(context.Request.PathInfo.Substring(1));
                    output = GetMemberFriends(memberId);
                }
                var json = JsonConvert.SerializeObject(output);
                context.Response.Write(json);
            }
        }

        private IEnumerable<Member> GetMemberFriends(int memberId)
        {
            return DomainFacade.GetMemberFriends(memberId);

        }
    }
}