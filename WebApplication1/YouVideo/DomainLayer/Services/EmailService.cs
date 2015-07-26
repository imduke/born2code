using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.Models;

namespace YouVideo.DomainLayer.Services
{
    internal sealed class EmailService : EmailServiceBase
    {
        public EmailService()
        {
        }

        protected override void SendNotificationEmailCore(int memberId, VideoInfo videoInfo, IEnumerable<Member> members)
        {
            var fgColor = Console.ForegroundColor;
            var hlColor = ConsoleColor.Green;

            foreach (var friend in members)
            {
                Console.ForegroundColor = fgColor;
                Console.Write("Hello ");
                Console.ForegroundColor = hlColor;
                Console.WriteLine("{0},", friend.FirstName);
                Console.ForegroundColor = fgColor;
                Console.WriteLine("Your friend with Id: {0},\r\njust uploaded a new video titled:", memberId);
                Console.ForegroundColor = hlColor;
                Console.WriteLine("{0}", videoInfo.Title);
                Console.ForegroundColor = fgColor;
                Console.WriteLine("check it out here");
                Console.WriteLine("");
            }

            Console.WriteLine("\r\n");
            Console.ForegroundColor = fgColor;
        }
    }
}
