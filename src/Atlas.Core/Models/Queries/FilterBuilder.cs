using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Queries
{
    public class FilterBuilder
    {
        private readonly IList<string> _filters;

        FilterBuilder()
        {
            _filters = new List<string>();
        }

        public static FilterBuilder Create()
        {
            return new FilterBuilder();
        }

        public FilterBuilder Add<T>(string field, FilterOperatorType type, T value)
        {
            if (value != null)
            {
                if (double.TryParse(value.ToString(), out double output))
                {
                    this._filters.Add($"filter[{field}][{ParseOperator(type)}]={output.ToString(CultureInfo.InvariantCulture)}");
                }
                else
                { 
                    this._filters.Add($"filter[{field}][{ParseOperator(type)}]={value}");
                }
            }
            return this;
        }

        public override string ToString()
        {
            return string.Join("$", _filters.ToArray());
        }

        public static implicit operator String(FilterBuilder builder)
        {
            return builder.ToString();
        }

        private string ParseOperator(FilterOperatorType @operator)
        {
            return @operator switch
            {
                FilterOperatorType.All => "all",
                FilterOperatorType.Contains => "contains",
                FilterOperatorType.EndsWith => "ends",
                FilterOperatorType.Equal => "eq",
                FilterOperatorType.GreaterThan => "gt",
                FilterOperatorType.GreaterThanOrEqual => "gte",
                FilterOperatorType.Any => "any",
                FilterOperatorType.LessThan => "lt",
                FilterOperatorType.LessThanOrEqual => "lte",
                FilterOperatorType.NotContains => "ncontains",
                FilterOperatorType.NotEndsWith => "nends",
                FilterOperatorType.NotEqual => "neq",
                FilterOperatorType.NotAny => "nany",
                FilterOperatorType.NotStatsWith => "nstarts",
                FilterOperatorType.StartsWith => "starts",
                _ => "eq"
            };
        }
    }
}
