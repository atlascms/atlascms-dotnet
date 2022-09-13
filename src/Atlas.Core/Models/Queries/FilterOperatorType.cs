using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Queries
{
    public enum FilterOperatorType
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        All,
        Any,
        Contains,
        NotContains,
        NotAny,
        StartsWith,
        NotStatsWith,
        EndsWith,
        NotEndsWith
    }
}
