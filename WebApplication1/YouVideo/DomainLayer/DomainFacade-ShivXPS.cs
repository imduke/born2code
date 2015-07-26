using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.AbstractFactory;
using YouVideo.DataLayer;
using YouVideo.DomainLayer.Managers;
using YouVideo.Models;

namespace YouVideo.DomainLayer
{
    public class DomainFacade : DomainFacadeBase
    {
        public DomainFacade()
        {
        }

        protected override IEnumerable<Video> GetMemberVideosCore(int memberId)
        {
            return MemberManager.GetMemberVideos(memberId);
        }

        protected override Member AuthenticateMemberCore(string userName, string password)
        {

            return MemberManager.AuthenticateMember(userName, password);
        }

        protected override int SaveVideoCore(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile)
        {
            var updatedVideoInfo = VideoManager.SaveVideo(memberId, videoInfo, httpFile);
            MemberManager.NotifyFriendsOfNewVideo(memberId, updatedVideoInfo);
            return updatedVideoInfo.Id;
        }

        protected override IEnumerable<Member> GetMemberFriendsCore(int memberId)
        {
            return MemberManager.GetMemberFriends(memberId);
        }
    }
}
