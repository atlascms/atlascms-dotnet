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
        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasClient"/> class.
        /// </summary>
        /// <param name="options">The configuration options <see cref="AtlasOptions"/>.</param>
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
            var request = new RestRequest("/api/contents/{model}/{id}")
                                .AddUrlSegment("model", modelKey)
                                .AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
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

        public async Task<string> UploadAsset(string fileName, byte[] bytes, string folder = "/", CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media/upload")
                                .AddHeader("Content-Type", "multipart/form-data")
                                .AddParameter("folder", folder, ParameterType.RequestBody)
                                .AddFile("file", bytes, fileName);
           
            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        public async Task<string> UploadAsset(string filePath, string folder = "/", CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media/upload")
                                .AddHeader("Content-Type", "multipart/form-data")
                                .AddParameter("folder", folder, ParameterType.RequestBody)
                                .AddFile("file", filePath);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        public async Task<byte[]> DownloadAsset(string id, CancellationToken cancellation = default)
        {
            var asset = await GetAsset(id, cancellation);

            if (asset != null)
            {
                return await new RestClient().DownloadDataAsync(new RestRequest(asset.Url), cancellation);
            }

            return null;
        }

        public async Task<Stream> DownloadAssetStream(string id, CancellationToken cancellation = default)
        {
            var asset = await GetAsset(id, cancellation);

            if (asset != null)
            {
                return await new RestClient().DownloadStreamAsync(new RestRequest(asset.Url), cancellation);
            }

            return null;
        }

        public async Task DeleteAsset(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        public async Task<List<Folder>> GetFolders(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders");

            return await GetAsync<List<Folder>>(request, cancellation);
        }

        public async Task<string> CreateFolder(string folder, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders").AddJsonBody(new { folder = folder });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        public async Task<string> RenameFolder(string folder, string newName, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders/rename").AddJsonBody(new { folder = folder, newName = newName });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        public async Task<string> MoveFolder(string folder, string moveTo, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders/move").AddJsonBody(new { folder = folder, moveTo = moveTo });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        public async Task DeleteFolder(string folder, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders").AddQueryParameter("folder", folder);

            await DeleteAsync(request, cancellation);
        }

        #endregion

        public IAtlasClient UseToken(string token)
        {
            base.SetToken(token);

            return this;
        }
        
    }
}
