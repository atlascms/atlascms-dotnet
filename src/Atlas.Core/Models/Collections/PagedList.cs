using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Collections
{
    public class PagedList<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public PagedListMetadata Metadata { get; }
    }
}
