using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouVideo.DomainLayer.Exceptions;

namespace YouVideo.Utils
{
    internal static class VideoMimeTypeUtility
    {
        private static HashSet<string> supportedMimeTypes = new HashSet<string> { { "video/mp4" }, { "video/x-flv" }, { "video/x-ms-wmv" } };
        private static Dictionary<string, string> mimeTypeToFileExtMapping = new Dictionary<string, string> { { "video/mp4", ".mp4" }, { "video/x-flv", ".flv" }, { "video/x-ms-wmv", "wmv" } };

        public static void EnsureMimeTypeSupported(string mimeType)
        {
            if (!supportedMimeTypes.Contains(mimeType))
                throw new MimeTypeNotSupportedException(string.Format("The mime type: {0}  is not supported at this time", mimeType));
        }

        public static string GetFileExtensionForMimeType(string mimeType)
        {
            EnsureMimeTypeSupported(mimeType);
            return mimeTypeToFileExtMapping[mimeType];
        }

        public static string GetMimeTypeFromFileExtension(string fileExtension)
        {
            foreach (var item in mimeTypeToFileExtMapping)
            {
                if (fileExtension.Equals(item.Value, StringComparison.OrdinalIgnoreCase))
                    return item.Key; 
            }
            return string.Empty;
        }
    }
}
