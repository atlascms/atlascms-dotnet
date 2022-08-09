using Atlas.Core.Configuration;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public class AtlasManagementClient : ClientBase, IAtlasManagementClient
    {
        public AtlasManagementClient(AtlasOptions options)
        {
            _options = options;
            _http = new RestClient(options.BaseUrl);
            _http.UseNewtonsoftJson(options.SerializerOptions);
        }
    }
}
