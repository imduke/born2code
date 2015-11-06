using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.Models;

namespace YouVideo.DataLayer
{
    internal abstract class DataServiceBase
    {
        public IEnumerable<Video> GetMemberVideos(int memberId)
        {
            return GetMemberVideosCore(memberId);
        }

        public Member GetMemberInfo(string userName, string password)
        {
            return GetMemberInfoCore(userName, password);
        }

        public IEnumerable<Member> GetMemberFriends(int memberId)
        {
            return GetMemberFriendsCore(memberId);
        }

        public IEnumerable<Member> GetMemberFriends()
        {
            return GetMemberFriendsCore();
        }

        public int CreateVideoRecord(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile, VideoMetadata videoMetadata)
        {
            return CreateVideoRecordCore(memberId, videoInfo, httpFile, videoMetadata);
        }

        protected abstract IEnumerable<Video> GetMemberVideosCore(int memberId);
        protected abstract Member GetMemberInfoCore(string userName, string password);
        protected abstract IEnumerable<Member> GetMemberFriendsCore(int memberId);
        protected abstract IEnumerable<Member> GetMemberFriendsCore();
        protected abstract int CreateVideoRecordCore(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile, VideoMetadata videoMetadata);
    }
}
