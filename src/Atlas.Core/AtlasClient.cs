using Atlas.Core.Configuration;
using Atlas.Core.Extensionis;
using Atlas.Core.Models;
using Atlas.Core.Models.Collections;
using Atlas.Core.Models.Queries;
using RestSharp;
using RestSharp.Serializers.Json;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public class AtlasClient : ClientBase, IAtlasClient
    {
        public AtlasClient(AtlasOptions options)
        {
            _http = new RestClient(options.BaseUrl);
            _http.UseNewtonsoftJson(options.SerializerOptions);
        }

        public async Task<string> CreateContent<T>(string modelKey, T content, CancellationToken cancellation = default) where T : Content<T>
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteContent<T>(string modelKey, string id, CancellationToken cancellation = default) where T : Content<T>
        {
            throw new NotImplementedException();
        }

        public async Task<Asset> GetAsset(string id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Asset>> GetAssets(AssetsQuery query, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media")
                                                                .AddQuery(query);



            var response = await _http.GetAsync<PagedList<Asset>>(request, cancellation);

            return response;
        }

        public async Task<Content<T>> GetContent<T>(string modelKey, string id, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/contents/{model}/{id}")
                                .AddUrlSegment("model", modelKey)
                                .AddUrlSegment("id", id);


            var response = await _http.GetAsync<Content<T>>(request, cancellation);

            return response;
        }

        public async Task<PagedList<Content<T>>> GetContents<T>(string modelKey, ContentsQuery query, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/contents/{model}")
                                .AddUrlSegment("model", modelKey)
                                .AddQuery(query);

            var response = await _http.GetAsync<PagedList<Content<T>>>(request, cancellation);

            return response;
        }

        public async Task<string> UpdateContent<T>(string modelKey, T content, CancellationToken cancellation = default) where T : Content<T>
        {
            throw new NotImplementedException();
        }
    }
}
