using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class Content<T> where T : class
    {
        /// <summary>
        /// Content Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Content Locale
        /// </summary>
        public string Locale { get; }

        /// <summary>
        /// Created At (UTC Date)
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; }

        /// <summary>
        /// Modified At (UTC Date)
        /// </summary>
        public DateTime ModifiedAt { get; }

        /// <summary>
        /// Modified By
        /// </summary>
        public string ModifiedBy { get; }

        /// <summary>
        /// Content Hash
        /// </summary>
        public string Hash { get; }

        /// <summary>
        /// Content Data
        /// </summary>
        public T Attributes { get; set; }

        /// <summary>
        /// Available Translations
        /// </summary>
        public List<ContentLocale> Locales { get; set; } = new List<ContentLocale>();
    }
}
