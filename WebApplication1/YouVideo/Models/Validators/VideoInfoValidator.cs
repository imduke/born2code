using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DomainLayer.Exceptions;
using YouVideo.Models;

namespace YouVideo.Models.Validators
{
    internal sealed class VideoInfoValidator : ValidatorBase<VideoInfo>
    {
        protected override void ValidateCore(VideoInfo instance)
        {
            if (instance == null)
                throw new VideoInfoInvalidException("The videoInfo parameter must not be null");
            if (instance.Title == null)
                throw new VideoInfoInvalidException("The Title of the Video must be set to a valid Title");
            if (instance.Category == null)
                throw new VideoInfoInvalidException("The Category of the Video must be set to a valid Category");
            if (instance.Tags == null)
                throw new VideoInfoInvalidException("The Video must have some Tags associated with it");
        }
    }
}
