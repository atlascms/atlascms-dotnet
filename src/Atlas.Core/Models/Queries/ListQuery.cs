using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Queries
{
    public abstract class ListQuery
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 25;
        public string Sort { get; set; }
    }
}
