using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Collections
{
    [Serializable]
    public class PagedListMetadata
    {
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public int TotalPages { get; private set; }
        public int Count { get; private set; }
    }
}
