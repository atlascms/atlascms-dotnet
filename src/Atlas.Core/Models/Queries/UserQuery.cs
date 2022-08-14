using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Queries
{
    public class UserQuery : ListQuery
    {
        public string Username { get; set; }
        public string Search { get; set; }
        public ContentResolver Resolvers { get; set; } = ContentResolver.None;
    }
}
