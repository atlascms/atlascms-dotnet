using Atlas.Core.Models.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class Model
    {
        public Model()
        {
            this.Type = SchemaType.Model;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public SchemaType Type { get; init; }
        public List<IField> Attributes { get; set; } = new List<IField>();
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime ModifiedAt { get; private set; }
        public string ModifiedBy { get; private set; }
        public bool IsSingle { get; set; }
        public bool System { get; set; }
        public bool Localizable { get; set; }
    }

    public enum SchemaType
    {
        Model,
        Component,
        User
    }
}
