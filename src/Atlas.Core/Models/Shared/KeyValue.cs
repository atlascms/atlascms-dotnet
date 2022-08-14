using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Shared
{
    [Serializable]
    public class KeyValue<Tk, Tv>
    {
        public Tk Key { get; set; }
        public Tv Value { get; set; }


        public KeyValue() { }

        public KeyValue(Tk key, Tv value)
        {
            Key = key;
            Value = value;
        }
    }

    public static class KeyValue
    {
        public static KeyValue<Tk, Tv> Create<Tk, Tv>(Tk key, Tv value)
        {
            return new KeyValue<Tk, Tv>(key, value);
        }
    }
}
