using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class AssetMetadata
    {
        /// <summary>
        /// The Alternate text
        /// </summary>
        public string Alt { get; }

        /// <summary>
        /// The Title of asset
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The notes related to the asset
        /// </summary>
        public string Notes { get; }
    }
}
