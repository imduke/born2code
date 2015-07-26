using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouVideo.Models
{
    public sealed class VideoMetadata
    {
        public string VideoCodec { get; set; }
        public int VideoBitrate { get; set; }
        public string AudioCodec { get; set; }
        public int AudioBitrate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long FileSize { get; set; }
    }
}
