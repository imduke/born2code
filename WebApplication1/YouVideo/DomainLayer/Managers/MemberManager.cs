using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DataLayer;
using YouVideo.DomainLayer;
using YouVideo.DomainLayer.Exceptions;
using YouVideo.DomainLayer.Services;
using YouVideo.Models;

namespace YouVideo.DomainLayer.Managers
{
    internal sealed class MemberManager : MemberManagerBase
    {
        protected override IEnumerable<Video> GetMemberVideosCore(int memberId)
        {
            return DataService.GetMemberVideos(memberId);  
        }

        protected override Member AuthenticateMemberCore(string userName, string password)
        {
            var member = DataService.GetMemberInfo(userName, password);
            if (member == null)
                throw new InvalidUserNamePasswordCombinationException("The UserName and/or Password provided in not correct");
            return member;
        }

        protected override void NotifyFriendsOfNewVideoCore(int memberId, VideoInfo videoInfo)
        {
            var members = GetMemberFriends(memberId);
            EmailService.SendNotificationEmail(memberId, videoInfo, members);
        }

        protected override IEnumerable<Member> GetMemberFriendsCore(int memberId)
        {
            return DataService.GetMemberFriends(memberId);
        }
    }
}
