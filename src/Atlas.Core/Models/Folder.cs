using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class Folder
    {
        /// <summary>
        /// The Folder Id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// The Folder name
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// The Folder full path
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The Sub folders
        /// </summary>
        public List<Folder> Children { get; private set; }
    }
}
