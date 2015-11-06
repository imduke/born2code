using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouVideo.DomainLayer.Exceptions;
using YouVideo.Models;

namespace YouVideo.DataLayer
{
    internal sealed class DataService : DataServiceBase
    {
        private IEnumerable<Member> members = new List<Member>
        {
            { new Member(1, "luke") { FirstName = "Luke", LastName = "Skywalker", Email = "luke.skywalker@starwars.com" } },
            { new Member(2, "obi") { FirstName = " Obi-Wan", LastName = "Kenobi", Email = "obi-wan.kenobi@starwars.com" } },
            { new Member(3, "anakin") { FirstName = " Anakin", LastName = "Skywalker", Email = "anakin.skywalker@starwars.com" } }
        };

        private Dictionary<int, List<Video>> memberVideos = new Dictionary<int, List<Video>>
        {
            { 1, new List<Video>
                     {
                        { new Video("Star Wars Episode IV: A New Hope", "Sci-fi", "http://matlusstorage.blob.core.windows.net/membervideos/StarWarsEpisodeIV.jpg") },
                        { new Video("Star Wars Episode V: The Empire Strikes Back", "Sci-fi", "http://matlusstorage.blob.core.windows.net/membervideos/StarWarsEpisodeV.jpg") },
                        { new Video("Star Wars Episode VI: Return of the Jedi", "Sci-fi", "http://matlusstorage.blob.core.windows.net/membervideos/StarWarsEpisodeVI.jpg") }
                     }
            }
        };

        public DataService()
        {
        }

        protected override IEnumerable<Video> GetMemberVideosCore(int memberId)
        {
            var videos = from mv in memberVideos
                         where mv.Key == memberId
                         select mv.Value;

            return videos.FirstOrDefault() ?? new List<Video>();
        }

        protected override Member GetMemberInfoCore(string userName, string password)
        {
            var result = from m in members
                         where m.UserName == userName && m.LastName.ToLower() == password
                         select m;
            return result.FirstOrDefault();
        }

        protected override IEnumerable<Member> GetMemberFriendsCore(int memberId)
        {
            if (members.Where(m => m.Id == memberId).Count() != 0)
                return from m in members
                       where m.Id != memberId
                       select m;

            else
                return new Member[0];
        }

        protected override IEnumerable<Member> GetMemberFriendsCore()
        {
                return members;
        }

        protected override int CreateVideoRecordCore(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile, VideoMetadata videoMetadata)
        {
            if (memberVideos.ContainsKey(memberId))
            {
                var memberVids = memberVideos[memberId];
                memberVids.Add(new Video(videoInfo.Title, videoInfo.Category, null));                
            }
            else
            {
                memberVideos.Add(memberId,
                    new List<Video>
                    {
                        { new Video(videoInfo.Title, videoInfo.Category, null) }
                    });
            }

            return memberVideos.Count();            
        }
    }
}
