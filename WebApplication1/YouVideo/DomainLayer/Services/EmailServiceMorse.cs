using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DataLayer;
using YouVideo.DomainLayer;
using YouVideo.DomainLayer.Services;
using YouVideo.Models;

namespace YouVideo.DomainLayer.Services
{
    [ExcludeFromCodeCoverage]
    internal sealed class EmailServiceMorse : EmailServiceBase
    {
        static Random rnd = new Random();
        private string[] morseSounds = { "♦", "▬" };
        private int[] durations = { 100, 300 };

        protected override void SendNotificationEmailCore(int memberId, VideoInfo videoInfo, IEnumerable<Member> members)
        {
            for (int i = 0; i < 50; i++)
            {
                var idx = rnd.Next(morseSounds.Length);
                Console.Beep(1000, durations[idx]);
                Console.Write(morseSounds[idx] + " ");
            }
            Console.WriteLine("\r\n");
        }
    }
}
