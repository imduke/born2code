using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.Models;

namespace YouVideo.DomainLayer.Services
{
    internal abstract class EmailServiceBase : ServiceBase
    {
        public void SendNotificationEmail(int memberId, VideoInfo videoInfo, IEnumerable<Member> members)
        {
            SendNotificationEmailCore(memberId, videoInfo, members);
        }

        protected abstract void SendNotificationEmailCore(int memberId, VideoInfo videoInfo, IEnumerable<Member> members);
    }
}
