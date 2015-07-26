using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DomainLayer.Exceptions;
using YouVideo.Models;

namespace YouVideo.Models.Validators
{
    internal sealed class HttpFileValidator : ValidatorBase<UploadedFileInfo>
    {
        protected override void ValidateCore(UploadedFileInfo instance)
        {
            if (instance == null)
                throw new HttpFileInvalidException("The HttpFile parameter must not be null");
            if (instance.FileName == null)
                throw new HttpFileInvalidException("The name of the file being uploaded must not be null");
            if (instance.MimeType == null)
                throw new HttpFileInvalidException("The mime type of the file being uploaded must not be null");
            if (instance.Stream == null)
                throw new HttpFileInvalidException("The File stream of the file being uploaded must not be null");
            if (instance.Stream.Length == 0)
                throw new HttpFileInvalidException("The File being uploaded must not be an empty file (zero length)");            
        }
    }
}
