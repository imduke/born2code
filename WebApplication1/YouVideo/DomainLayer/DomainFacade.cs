using System.Collections.Generic;
using YouVideo.DomainLayer.ServiceLocator;
using YouVideo.DataLayer;
using YouVideo.DomainLayer.Managers;
using YouVideo.DomainLayer.Services;
using YouVideo.Models;

namespace YouVideo.DomainLayer
{
    public sealed partial class DomainFacade
    {
        public IEnumerable<Video> GetMemberVideos(int memberId)
        {
            return MemberManager.GetMemberVideos(memberId);
        }

        public Member AuthenticateMember(string userName, string password)
        {
            return MemberManager.AuthenticateMember(userName, password);
        }

        public int SaveVideo(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile)
        {
            var updatedVideoInfo = VideoManager.SaveVideo(memberId, videoInfo, httpFile);
            MemberManager.NotifyFriendsOfNewVideo(memberId, updatedVideoInfo);
            return updatedVideoInfo.Id;
        }

        public IEnumerable<Member> GetMemberFriends(int memberId)
        {
            return MemberManager.GetMemberFriends(memberId);
        }
    }
}
