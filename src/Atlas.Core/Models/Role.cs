using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class Role
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public bool System { get; private set; }

        public List<string> Permissions { get; set; } = new List<string>();
    }
}
