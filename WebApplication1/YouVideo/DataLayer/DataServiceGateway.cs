using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using YouVideo.ServiceAgent;
using YouVideo.Models;
using System.Diagnostics.CodeAnalysis;

namespace YouVideo.DataLayer
{
    internal class DataServiceGateway : DataServiceBase
    {
        private const string serviceUrl = "http://async.azurewebsites.net/";

        [ExcludeFromCodeCoverage]
        protected virtual ServiceAgentHttpClient MakeServiceAgentHttpClient()
        {
            return new ServiceAgentHttpClient(serviceUrl);
        }

        protected override IEnumerable<Video> GetMemberVideosCore(int memberId)
        {
            using (var serviceAgent = MakeServiceAgentHttpClient())
            {
                var videosTask = serviceAgent.GetAsync<IEnumerable<Video>>("api/MemberVideos/" + memberId.ToString(), new FormDataCollection(""));
                return videosTask.Result;
            }
        }

        protected override Member GetMemberInfoCore(string userName, string password)
        {
            using (var serviceAgent = MakeServiceAgentHttpClient())
            {
                var kvps = new List<KeyValuePair<string, string>>
                {
                    { new KeyValuePair<string, string>("userName", userName) },
                    {  new KeyValuePair<string, string>("password", password) }
                };

                var memberTask = serviceAgent.GetAsync<Member>("api/Member/", new FormDataCollection(kvps));
                return memberTask.Result;
            }            
        }

        protected override IEnumerable<Member> GetMemberFriendsCore(int memberId)
        {
            using (var serviceAgent = MakeServiceAgentHttpClient())
            {
                var videosTask = serviceAgent.GetAsync<IEnumerable<Member>>("api/MemberFriends/" + memberId.ToString(), new FormDataCollection(""));
                return videosTask.Result;
            }            
        }

        protected override int CreateVideoRecordCore(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile, VideoMetadata videoMetadata)
        {
            using (var serviceAgent = MakeServiceAgentHttpClient())
            {
                var data = new CreateVideoRecordInfo { MemberId = memberId, VideoInfo = videoInfo, VideoMetadata = videoMetadata };
                var videosTask = serviceAgent.PostAsync<CreateVideoRecordInfo, int>("api/MemberVideos/", data, new JsonMediaTypeFormatter());
                return videosTask.Result;
            }
        }
    }

    public class CreateVideoRecordInfo
    {
        public int MemberId { get; set; }
        public VideoInfo VideoInfo { get; set; }
        public UploadedFileInfo HttpFile { get; set; }
        public VideoMetadata VideoMetadata { get; set; }
    }

}
