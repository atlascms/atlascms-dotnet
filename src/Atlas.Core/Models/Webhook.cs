using Atlas.Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class Webhook
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Enabled { get; set; }
        public bool IncludePayload { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public List<KeyValue<string, string>> Headers { get; set; } = new List<KeyValue<string, string>>();
        public string EntityType { get; set; }
        public List<string> Events { get; set; }
        public List<string> EntityTypeIds { get; set; } = new List<string>();
    }
}
