using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DataLayer;
using YouVideo.DomainLayer.Services;
using YouVideo.Models;

namespace YouVideo.DomainLayer.Managers
{
    internal abstract class MemberManagerBase : ManagerBase
    {
        private EmailServiceBase emailService;
        protected EmailServiceBase EmailService { get { return emailService ?? (emailService = MakeEmailService()); } }

        protected virtual EmailServiceBase MakeEmailService()
        {
            return ServiceLocator.CreateEmailService();
        }

        public Member AuthenticateMember(string userName, string password)
        {
            return AuthenticateMemberCore(userName, password);
        }

        public IEnumerable<Video> GetMemberVideos(int memberId)
        {
            return GetMemberVideosCore(memberId);
        }

        public void NotifyFriendsOfNewVideo(int memberId, VideoInfo videoInfo)
        {
            NotifyFriendsOfNewVideoCore(memberId, videoInfo);
        }

        public IEnumerable<Member> GetMemberFriends(int memberId)
        {
            return GetMemberFriendsCore(memberId);
        }

        protected abstract IEnumerable<Video> GetMemberVideosCore(int memberId);
        protected abstract Member AuthenticateMemberCore(string userName, string password);
        protected abstract void NotifyFriendsOfNewVideoCore(int memberId, VideoInfo videoInfo);
        protected abstract IEnumerable<Member> GetMemberFriendsCore(int memberId);
    }
}
