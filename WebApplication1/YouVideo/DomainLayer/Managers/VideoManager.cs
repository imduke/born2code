using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DataLayer;
using YouVideo.Models.Validators;
using YouVideo.DomainLayer.Exceptions;
using YouVideo.Models;
using YouVideo.Utils;

namespace YouVideo.DomainLayer.Managers
{
    internal sealed class VideoManager : VideoManagerBase
    {
        protected override void ValidateVideoInfo(VideoInfo videoInfo)
        {
            VideoInfoValidator.Validate(videoInfo);
        }

        protected override void ValidateHttpFile(UploadedFileInfo httpFile)
        {
            HttpFileValidator.Validate(httpFile);
        }

        protected override void EnsureMimeTypeIsSupported(string mimeType)
        {
            VideoMimeTypeUtility.EnsureMimeTypeSupported(mimeType);
        }

        protected override VideoMetadata ExtractMetadata(UploadedFileInfo httpFile)
        {
            return new VideoMetadata
            {
                FileSize = httpFile.Stream.Length,
                Width = 1280,
                Height = 720,
                VideoBitrate = 5000,
                VideoCodec = "H.264",
                AudioBitrate = 256,
                AudioCodec = "AAC"
            };            
        }

        protected override void ValidateVideoUsingMetadata(VideoMetadata videoMetadata)
        {            
        }

        protected override void QueueVideoIntoTranscodeQueue(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile)
        {            
        }

        protected override int CreateVideoRecord(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile, VideoMetadata videoMetadata)
        {
            return DataService.CreateVideoRecord(memberId, videoInfo, httpFile, videoMetadata);
        }
    }
}
