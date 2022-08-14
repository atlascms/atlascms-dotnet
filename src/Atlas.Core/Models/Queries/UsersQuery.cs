using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Queries
{
    public class UsersQuery : ListQuery
    {
        public string Username { get; set; }
        public string Search { get; set; }
        public string RoleId { get; set; }
        public ContentResolver Resolvers { get; set; } = ContentResolver.None;
    }
}
