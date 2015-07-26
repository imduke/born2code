using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouVideo.Models
{
    public sealed class Member
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Member(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }
}
