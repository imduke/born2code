using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouVideo.Models
{
    public sealed class Video
    {
        public string Title { get; private set; }
        public string Category { get; private set; }
        public string ImageUrl { get; set; }

        public Video(string title, string category, string imageUrl)
        {
            Title = title;
            Category = category;
            ImageUrl = imageUrl;
        }
    }
}
