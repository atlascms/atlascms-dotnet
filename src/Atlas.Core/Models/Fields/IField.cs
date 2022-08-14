using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Fields
{
    public class IField
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public string Help { get; set; }
        public int Order { get; set; }
        public string Type { get; init; }
        public bool Localizable { get; set; }
        public bool Hidden { get; set; }
        public bool ReadOnly { get; set; }
        public bool Required { get; set; }
    }
}
