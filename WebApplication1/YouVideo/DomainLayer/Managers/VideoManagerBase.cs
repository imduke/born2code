using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DataLayer;
using YouVideo.Models.Validators;
using YouVideo.Models;

namespace YouVideo.DomainLayer.Managers
{
    internal abstract class VideoManagerBase : ManagerBase
    {
        #region Protected Properties

        private VideoInfoValidator videoInfoValidator;
        protected VideoInfoValidator VideoInfoValidator { get { return videoInfoValidator ?? (videoInfoValidator = MakeVideoInfoValidator()); } }

        private HttpFileValidator httpFileValidator;
        protected HttpFileValidator HttpFileValidator { get { return httpFileValidator ?? (httpFileValidator = MakeHttpFileValidator()); } }

        #endregion Protected Properties

        #region Factory Methods

        protected virtual VideoInfoValidator MakeVideoInfoValidator()
        {
            return new VideoInfoValidator();
        }

        protected virtual HttpFileValidator MakeHttpFileValidator()
        {
            return new HttpFileValidator();
        }

        #endregion Factory Methods

        public VideoInfo SaveVideo(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile)
        {
            ValidateVideoInfo(videoInfo);
            ValidateHttpFile(httpFile);
            EnsureMimeTypeIsSupported(httpFile.MimeType);
            var videoMetadata = ExtractMetadata(httpFile);
            ValidateVideoUsingMetadata(videoMetadata);
            QueueVideoIntoTranscodeQueue(memberId, videoInfo, httpFile);
            videoInfo.Id = CreateVideoRecord(memberId, videoInfo, httpFile, videoMetadata);
            return videoInfo;
        }

        protected abstract void ValidateVideoInfo(VideoInfo videoInfo);
        protected abstract void ValidateHttpFile(UploadedFileInfo httpFile);
        protected abstract void EnsureMimeTypeIsSupported(string mimeType);
        protected abstract VideoMetadata ExtractMetadata(UploadedFileInfo httpFile);
        protected abstract void ValidateVideoUsingMetadata(VideoMetadata videoMetadata);
        protected abstract void QueueVideoIntoTranscodeQueue(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile);
        protected abstract int CreateVideoRecord(int memberId, VideoInfo videoInfo, UploadedFileInfo httpFile, VideoMetadata videoMetadata);
    }
}
