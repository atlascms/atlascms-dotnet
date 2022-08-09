using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Queries
{
    [Flags]
    public enum ContentResolver
    {
        None = 0,
        Media = 1,
        MediaGallery = 2,
        References = 4
    }
}
