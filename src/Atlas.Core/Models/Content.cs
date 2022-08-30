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
        public string Locale { get; set; }

        /// <summary>
        /// Created At (UTC Date)
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// The Model Id to which the content belongs
        /// </summary>
        public string ModelId { get; private set; }

        /// <summary>
        /// The Model Key to which the content belongs
        /// </summary>
        public string ModelKey { get; private set; }

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
        /// Content Hash
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// Content Data
        /// </summary>
        public T Attributes { get; set; }

        /// <summary>
        /// Content Stage
        /// </summary>
        public Stages? Stage { get; set; }

        /// <summary>
        /// Available Translations
        /// </summary>
        public List<ContentLocale> Locales { get; private set; }
    }
}
