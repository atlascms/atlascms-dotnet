using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Atlas.Core.Sereializer.NewtonsoftJson
{
    public class MultipleContractResolver : IContractResolver, IEnumerable<IContractResolver>
    {
        private readonly IList<IContractResolver> _contractResolvers = new List<IContractResolver>();

        public JsonContract ResolveContract(Type type)
        {
            return
                _contractResolvers
                    .Select(x => x.ResolveContract(type))
                    .FirstOrDefault(x => x != null);
        }

        public void Add([NotNull] IContractResolver contractResolver)
        {
            if (contractResolver == null) throw new ArgumentNullException(nameof(contractResolver));
            _contractResolvers.Add(contractResolver);
        }

        public IEnumerator<IContractResolver> GetEnumerator()
        {
            return _contractResolvers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
