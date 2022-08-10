using Atlas.Core.Configuration;
using Atlas.Core.Extensions;
using Atlas.Core.Models;
using Atlas.Core.Models.Api;
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
            _options = options;
            _http = new RestClient(options.BaseUrl);
            _http.UseNewtonsoftJson(options.SerializerOptions);
        }

        #region -- contents --

        public async Task<string> CreateContent<T>(string modelKey, T content, string locale = "", CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/contents/{model}")
                                .AddUrlSegment("model", modelKey)
                                .AddJsonBody(
                                    new
                                    {
                                        Locale = locale,
                                        Attributes = content
                                    }
                                );

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        public async Task DeleteContent(string modelKey, string id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Content<T>> GetContent<T>(string modelKey, string id, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/contents/{model}/{id}").AddUrlSegment("model", modelKey).AddUrlSegment("id", id);

            return await GetAsync<Content<T>>(request, cancellation);            
        }

        public async Task<PagedList<Content<T>>> GetContents<T>(string modelKey, ContentsQuery query, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/contents/{model}").AddUrlSegment("model", modelKey).AddQuery(query);

            return await GetAsync<PagedList<Content<T>>>(request, cancellation);
        }

        public async Task UpdateContent<T>(string modelKey, string id, T content, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/contents/{model}/{id}")
                                .AddUrlSegment("model", modelKey)
                                .AddUrlSegment("id", id)
                                .AddJsonBody(
                                    new
                                    {
                                        Id = id,
                                        Attributes = content
                                    }
                                );

            await PutAsync(request, cancellation);
        }

        #endregion

        #region -- assets --

        public async Task<Asset> GetAsset(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media/{id}").AddUrlSegment("id", id);

            return await GetAsync<Asset>(request, cancellation);
        }

        public async Task<PagedList<Asset>> GetAssets(AssetsQuery query, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media").AddQuery(query);

            return await GetAsync<PagedList<Asset>>(request, cancellation);
        }

        public async Task<List<Folder>> GetFolders(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders");

            return await GetAsync<List<Folder>>(request, cancellation);
        }

        public Task<List<Folder>> GetFolders(AssetsQuery query, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        #endregion

        public IAtlasClient UseToken(string token)
        {
            base.SetToken(token);

            return this;
        }
    }
}
