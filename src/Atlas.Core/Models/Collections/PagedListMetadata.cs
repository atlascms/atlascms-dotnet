using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Collections
{
    public class PagedListMetadata
    {
        public int CurrentPage { get; }
        public int PageSize { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
        public int TotalPages { get; }
        public int Count { get; }
    }
}
