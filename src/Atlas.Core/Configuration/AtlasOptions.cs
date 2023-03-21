using Atlas.Core.Serializer.NewtonsoftJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Configuration
{
    public class AtlasOptions
    {
        /// <summary>
        /// The base url of the Atlas Instance
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// The Global Api Key used for authorization
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The Organization Key
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// The Project Key to interact with (not used for organization level actions)  
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// The Newtonsoft.Json.Net Serializer Settings
        /// </summary>
        public JsonSerializerSettings SerializerOptions { get; set; } = GetSerializerSettings();

        private static JsonSerializerSettings GetSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter(new CamelCaseNamingStrategy()));

            return settings;
        }
    }
}
