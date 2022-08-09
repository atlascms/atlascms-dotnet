using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class Asset
    {
        /// <summary>
        /// The Id of Media
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The Code 
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// The Folder Id where it stands
        /// </summary>
        public string Folder { get; private set; }

        /// <summary>
        /// Type of Media, can be image, video or document
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Created At (UTC Date)
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; private set; }

        /// <summary>
        /// Modified At (UTC Date)
        /// </summary>
        public DateTime ModifiedAt { get; private set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public string ModifiedBy { get; private set; }

        /// <summary>
        /// Author
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// Copyrights
        /// </summary>
        public string Copyright { get; private set; }

        /// <summary>
        /// The Original File Name
        /// </summary>
        public string OriginalFileName { get; private set; }

        /// <summary>
        /// The Name in Atlas DB
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The Extension of file
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// The Hash Of file
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// The mime-type
        /// </summary>
        public string MimeType { get; private set; }

        /// <summary>
        /// The size in bytes
        /// </summary>
        public long Size { get; private set; }

        /// <summary>
        /// List of Automatic Tags assigned when uploaded
        /// </summary>
        public List<string> AutomaticTags { get; private set; }

        /// <summary>
        /// List of Manual Tags
        /// </summary>
        public List<string> Tags { get; private set; }

        /// <summary>
        /// The height in pixel (if it is an image)
        /// </summary>
        public int? Height { get; private set; }

        /// <summary>
        /// The wdith in pixel (if it is an image)
        /// </summary>
        public int? Width { get; private set; }

        /// <summary>
        /// The horizontal resolution in dpi (if it is an image)
        /// </summary>
        public double? HorizontalResolution { get; private set; }

        /// <summary>
        /// The vertical resolution in dpi (if it is an image)
        /// </summary>
        public double? VerticalResolution { get; private set; }

        /// <summary>
        /// The duration in seconds (if it is a video)
        /// </summary>
        public double? Duration { get; private set; }

        /// <summary>
        /// The frame per second (if it is a video)
        /// </summary>
        public double? Fps { get; private set; }

        /// <summary>
        /// The video codec (if it is a video)
        /// </summary>
        public string Codec { get; private set; }

        /// <summary>
        /// The dictionary of Exif Data if available
        /// </summary>
        public Dictionary<string, object> Exif { get; private set; }

        /// <summary>
        /// The asset Url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Metadata information
        /// </summary>
        public Dictionary<string, AssetMetadata> Metadata { get; private set; } = new Dictionary<string, AssetMetadata>();
    }
}
