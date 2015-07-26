using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouVideo.ServiceAgent
{
    /// <summary>
    /// This enum provides for the accepted media types for serialization/deserialization
    /// </summary>
    internal enum AcceptedMediaType
    {
        /// <summary>
        /// JSON Media Type
        /// </summary>
        Json,

        /// <summary>
        /// XML Media Type
        /// </summary>
        Xml,

        /// <summary>
        /// HalMedia JSON
        /// </summary>
        HalMediaJson,

        /// <summary>
        /// HalMedia XML
        /// </summary>
        HalMediaXml
    }
}
