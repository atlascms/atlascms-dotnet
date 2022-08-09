using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Sereializer.NewtonsoftJson
{
    public class PrivateSetterContractResolver : CamelCasePropertyNamesContractResolver
    {
        public PrivateSetterContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            if (!jsonProperty.Writable)
            {
                if (member is PropertyInfo propertyInfo)
                {
                    jsonProperty.Writable = propertyInfo.GetSetMethod(true) != null;
                }
            }

            return jsonProperty;
        }
    }
}
