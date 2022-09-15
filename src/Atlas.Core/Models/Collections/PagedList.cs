using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Collections
{
    [Serializable]
    public class PagedList<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public PagedListMetadata Metadata { get; private set; }
    }
}
