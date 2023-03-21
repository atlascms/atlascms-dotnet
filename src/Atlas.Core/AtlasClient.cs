using Atlas.Core.Configuration;
using Atlas.Core.Models;
using Atlas.Core.Models.Shared;
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
using Atlas.Core.Infrastructure;

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
            var http = CreateClient(options);

            var userClient = new AtlasUserClient(http, options);
            var managementClient = new AtlasManagementClient(http, options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasClient"/> class.
        /// </summary>
        /// <param name="options">The configuration options <see cref="AtlasOptions"/>.</param>
        /// <param name="http">The <see cref="RestClient"/>.</param>
        /// <param name="userClient">The <see cref="IAtlasUserClient"/>.</param>
        /// <param name="managementClient">The <see cref="IAtlasManagementClient"/>.</param>
        public AtlasClient(AtlasOptions options, RestClient http)
        {
            SetClient(http, options);
        }

        #region -- contents --

        /// <inheritdoc/>
        public async Task<string> CreateContentAsync<T>(string modelKey, T content, string locale = "", CancellationToken cancellation = default) where T : class
        {
            return await CreateContentAsync(_options.Project, modelKey, content, locale, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> CreateContentAsync<T>(string project, string modelKey, T content, string locale = "", CancellationToken cancellation = default) where T : class
        {

            var request = CreateContentRequest(project, modelKey, "")
                                .AddJsonBody(
                                    new
                                    {
                                        Locale = locale,
                                        Attributes = content
                                    }
                                );

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <inheritdoc/>
        public async Task<string> CreateContentAsync(string modelKey, Dictionary<string, object> content, string locale = "", CancellationToken cancellation = default)
        {
            return await CreateContentAsync<Dictionary<string, object>>(_options.Project, modelKey, content, locale, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> CreateContentAsync(string project, string modelKey, Dictionary<string, object> content, string locale = "", CancellationToken cancellation = default)
        {
            return await CreateContentAsync<Dictionary<string, object>>(project, modelKey, content, locale, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> CreateTranslationAsync(string modelKey, string id, string locale, CancellationToken cancellation = default)
        {
            return await CreateTranslationAsync(_options.Project, modelKey, id, locale, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> CreateTranslationAsync(string project, string modelKey, string id, string locale, CancellationToken cancellation = default)
        {
            var request = CreateContentRequest(project, modelKey, "{id}/create-translation").AddUrlSegment("id", id)
                               .AddJsonBody(
                                   new
                                   {
                                       Locale = locale
                                   }
                               );

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <inheritdoc/>
        public async Task DeleteContentAsync(string modelKey, string id, CancellationToken cancellation = default)
        {
            await DeleteContentAsync(_options.Project, modelKey, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task DeleteContentAsync(string project, string modelKey, string id, CancellationToken cancellation = default)
        {
            var request = CreateContentRequest(project, modelKey, "{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> DuplicateAsync(string modelKey, string id, CancellationToken cancellation = default)
        {
            return await DuplicateAsync(_options.Project, modelKey, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> DuplicateAsync(string project, string modelKey, string id, CancellationToken cancellation = default)
        {
            var request = CreateContentRequest(project, modelKey, "{id}/duplicate")
                               .AddUrlSegment("id", id)
                               .AddJsonBody(
                                   new
                                   {
                                       Locales = false
                                   }
                               );

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> DuplicateAllAsync(string modelKey, string id, CancellationToken cancellation = default)
        {
            return await DuplicateAllAsync(_options.Project, modelKey, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> DuplicateAllAsync(string project, string modelKey, string id, CancellationToken cancellation = default)
        {
            var request = CreateContentRequest(project, modelKey, "{id}/duplicate")
                               .AddUrlSegment("id", id)
                               .AddJsonBody(
                                   new
                                   {
                                       Locales = true
                                   }
                               );

            return (await PostAsync<KeyResult<IEnumerable<string>>>(request, cancellation)).Result;
        }

        /// <inheritdoc/>
        public async Task<Content<T>> GetContentAsync<T>(string modelKey, string id, CancellationToken cancellation = default) where T : class
        {
            return await GetContentAsync<T>(_options.Project, modelKey, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<Content<T>> GetContentAsync<T>(string project, string modelKey, string id, CancellationToken cancellation = default) where T : class
        {
            var request = CreateContentRequest(project, modelKey, "{id}").AddUrlSegment("id", id);

            return await GetAsync<Content<T>>(request, cancellation);
        }


        /// <inheritdoc/>
        public async Task<Content<Dictionary<string, object>>> GetContentAsync(string modelKey, string id, CancellationToken cancellation = default)
        {
            return await GetContentAsync(_options.Project, modelKey, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<Content<Dictionary<string, object>>> GetContentAsync(string project, string modelKey, string id, CancellationToken cancellation = default)
        {
            return await GetContentAsync<Dictionary<string, object>>(project, modelKey, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<PagedList<Content<T>>> GetContentsAsync<T>(string modelKey, ContentsQuery query, CancellationToken cancellation = default) where T : class
        {
            return await GetContentsAsync<T>(_options.Project, modelKey, query, cancellation);
        }

        /// <inheritdoc/>
        public async Task<PagedList<Content<T>>> GetContentsAsync<T>(string project, string modelKey, ContentsQuery query, CancellationToken cancellation = default) where T : class
        {
            var request = CreateContentRequest(project, modelKey, "").AddQuery(query);

            return await GetAsync<PagedList<Content<T>>>(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task<PagedList<Content<Dictionary<string, object>>>> GetContentsAsync(string modelKey, ContentsQuery query, CancellationToken cancellation = default)
        {
            return await GetContentsAsync<Dictionary<string, object>>(_options.Project, modelKey, query, cancellation);
        }

        /// <inheritdoc/>
        public async Task<PagedList<Content<Dictionary<string, object>>>> GetContentsAsync(string project, string modelKey, ContentsQuery query, CancellationToken cancellation = default)
        {
            return await GetContentsAsync<Dictionary<string, object>>(project, modelKey, query, cancellation);
        }

        /// <inheritdoc/>
        public async Task<int> CountContentsAsync(string modelKey, ContentsQuery query, CancellationToken cancellation = default)
        {
            return await CountContentsAsync(_options.Project, modelKey, query, cancellation);
        }

        /// <inheritdoc/>
        public async Task<int> CountContentsAsync(string project, string modelKey, ContentsQuery query, CancellationToken cancellation = default)
        {
            var request = CreateContentRequest(project, modelKey, "count").AddQuery(query);

            return (await GetAsync<KeyResult<int>>(request, cancellation)).Result;
        }

        /// <inheritdoc/>
        public async Task UpdateContentAsync<T>(string modelKey, string id, T content, CancellationToken cancellation = default) where T : class
        {
            await UpdateContentAsync<T>(_options.Project, modelKey, id, content, cancellation);
        }

        /// <inheritdoc/>
        public async Task UpdateContentAsync<T>(string project, string modelKey, string id, T content, CancellationToken cancellation = default) where T : class
        {
            var request = CreateContentRequest(modelKey, "{id}", "")
                                .AddUrlSegment("id", id)
                                .AddJsonBody(
                                    new
                                    {
                                        Attributes = content
                                    }
                                );

            await PutAsync(request, cancellation);
        }


        /// <inheritdoc/>
        public async Task UpdateContentAsync(string modelKey, string id, Dictionary<string, object> content, CancellationToken cancellation = default)
        {
            await UpdateContentAsync<Dictionary<string, object>>(_options.Project, modelKey, id, content, cancellation);
        }

        /// <inheritdoc/>
        public async Task UpdateContentAsync(string project, string modelKey, string id, Dictionary<string, object> content, CancellationToken cancellation = default)
        {
            await UpdateContentAsync<Dictionary<string, object>>(modelKey, id, content, cancellation);
        }

        #endregion

        #region -- assets --

        /// <inheritdoc/>
        public async Task<Asset> GetAssetAsync(string id, CancellationToken cancellation = default)
        {
            return await GetAssetAsync(_options.Project, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<Asset> GetAssetAsync(string project, string id, CancellationToken cancellation = default)
        {
            var request = CreateMediaRequest(project, "{id}").AddUrlSegment("id", id);

            return await GetAsync<Asset>(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task<PagedList<Asset>> GetAssetsAsync(AssetsQuery query, CancellationToken cancellation = default)
        {
            return await GetAssetsAsync(_options.Project, query, cancellation);
        }

        /// <inheritdoc/>
        public async Task<PagedList<Asset>> GetAssetsAsync(string project, AssetsQuery query, CancellationToken cancellation = default)
        {
            var request = CreateMediaRequest(project, "").AddQuery(query);

            return await GetAsync<PagedList<Asset>>(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task SetAssetTagsAsync(string id, IEnumerable<string> tags, CancellationToken cancellation = default)
        {
            await SetAssetTagsAsync(_options.Project, id, tags, cancellation);
        }

        /// <inheritdoc/>
        public async Task SetAssetTagsAsync(string project, string id, IEnumerable<string> tags, CancellationToken cancellation = default)
        {
            var request = CreateMediaRequest(project, "{id}/tags")
                                .AddUrlSegment("id", id)
                                .AddJsonBody(new { tags = tags });

            await PostAsync(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> UploadAssetAsync(string fileName, byte[] bytes, string folder = "/", CancellationToken cancellation = default)
        {
            return await UploadAssetAsync(_options.Project, fileName, bytes, folder, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> UploadAssetAsync(string project, string fileName, byte[] bytes, string folder = "/", CancellationToken cancellation = default)
        {
            var request = CreateMediaRequest(project, "upload")
                                .AddHeader("Content-Type", "multipart/form-data")
                                .AddParameter("folder", folder, ParameterType.RequestBody)
                                .AddFile("file", bytes, fileName);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <inheritdoc/>
        public async Task<string> UploadAssetAsync(string filePath, string folder = "/", CancellationToken cancellation = default)
        {
            return await UploadAssetAsync(_options.Project, filePath, folder, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> UploadAssetAsync(string project, string filePath, string folder = "/", CancellationToken cancellation = default)
        {
            var request = CreateMediaRequest(project, "upload")
                                .AddHeader("Content-Type", "multipart/form-data")
                                .AddParameter("folder", folder, ParameterType.RequestBody)
                                .AddFile("file", filePath);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <inheritdoc/>
        public async Task<byte[]> DownloadAssetAsync(string id, CancellationToken cancellation = default)
        {
            return await DownloadAssetAsync(_options.Project, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<byte[]> DownloadAssetAsync(string project, string id, CancellationToken cancellation = default)
        {
            var asset = await GetAssetAsync(project, id, cancellation);

            if (asset != null)
            {
                return await new RestClient().DownloadDataAsync(new RestRequest(asset.Url), cancellation);
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task<Stream> DownloadAssetStreamAsync(string id, CancellationToken cancellation = default)
        {
            return await DownloadAssetStreamAsync(_options.Project, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<Stream> DownloadAssetStreamAsync(string project, string id, CancellationToken cancellation = default)
        {
            var asset = await GetAssetAsync(project, id, cancellation);

            if (asset != null)
            {
                return await new RestClient().DownloadStreamAsync(new RestRequest(asset.Url), cancellation);
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task DeleteAssetAsync(string id, CancellationToken cancellation = default)
        {
            await DeleteAssetAsync(_options.Project, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task DeleteAssetAsync(string project, string id, CancellationToken cancellation = default)
        {
            var request = CreateMediaRequest(project, "{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task<List<Folder>> GetAllFoldersAsync(CancellationToken cancellation = default)
        {
            return await GetAllFoldersAsync(_options.Project, cancellation);
        }

        /// <inheritdoc/>
        public async Task<List<Folder>> GetAllFoldersAsync(string project, CancellationToken cancellation = default)
        {
            var request = CreateMediaFoldersRequest(project, "");

            return await GetAsync<List<Folder>>(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> CreateFolderAsync(string folder, CancellationToken cancellation = default)
        {
            return await CreateFolderAsync(_options.Project, folder, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> CreateFolderAsync(string project, string folder, CancellationToken cancellation = default)
        {
            var request = CreateMediaFoldersRequest(project, "").AddJsonBody(new { folder = folder });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }


        /// <inheritdoc/>
        public async Task<string> RenameFolderAsync(string folder, string newName, CancellationToken cancellation = default)
        {
            return await RenameFolderAsync(_options.Project, folder, newName, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> RenameFolderAsync(string project, string folder, string newName, CancellationToken cancellation = default)
        {
            var request = CreateMediaFoldersRequest(project, "rename").AddJsonBody(new { folder = folder, newName = newName });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <inheritdoc/>
        public async Task<string> MoveFolderAsync(string folder, string moveTo, CancellationToken cancellation = default)
        {
            return await MoveFolderAsync(_options.Project, folder, moveTo, cancellation);
        }

        /// <inheritdoc/>
        public async Task<string> MoveFolderAsync(string project, string folder, string moveTo, CancellationToken cancellation = default)
        {
            var request = CreateMediaFoldersRequest(project, "move").AddJsonBody(new { folder = folder, moveTo = moveTo });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <inheritdoc/>
        public async Task DeleteFolderAsync(string folder, CancellationToken cancellation = default)
        {
            await DeleteFolderAsync(_options.Project, folder, cancellation);
        }

        /// <inheritdoc/>
        public async Task DeleteFolderAsync(string project, string folder, CancellationToken cancellation = default)
        {
            var request = CreateMediaFoldersRequest(project, "").AddQueryParameter("folder", folder);

            await DeleteAsync(request, cancellation);
        }

        #endregion

        #region -- models --

        /// <inheritdoc/>
        public async Task<Model> GetModelAsync(string id, CancellationToken cancellation = default)
        {
            return await GetModelAsync(_options.Project, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<Model> GetModelAsync(string project, string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/content-types/models/{id}").AddUrlSegment("id", id);

            return await GetAsync<Model>(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task<List<Model>> GetAllModelsAsync(CancellationToken cancellation = default)
        {
            return await GetAllModelsAsync(_options.Project, cancellation);
        }

        /// <inheritdoc/>
        public async Task<List<Model>> GetAllModelsAsync(string project, CancellationToken cancellation = default)
        {
            return (await GetModelsAsync(new ModelsQuery { Size = int.MaxValue }, cancellation)).Data.ToList();
        }

        /// <inheritdoc/>
        public async Task<PagedList<Model>> GetModelsAsync(ModelsQuery query, CancellationToken cancellation = default)
        {
            return await GetModelsAsync(_options.Project, query, cancellation);
        }

        /// <inheritdoc/>
        public async Task<PagedList<Model>> GetModelsAsync(string project, ModelsQuery query, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/content-types/models").AddQuery(query);

            return await GetAsync<PagedList<Model>>(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task<Component> GetComponentAsync(string id, CancellationToken cancellation = default)
        {
            return await GetComponentAsync(_options.Project, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task<Component> GetComponentAsync(string project, string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/content-types/components/{id}").AddUrlSegment("id", id);

            return await GetAsync<Component>(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task<List<Component>> GetAllComponentsAsync(CancellationToken cancellation = default)
        {
            return await GetAllComponentsAsync(_options.Project, cancellation);
        }

        /// <inheritdoc/>
        public async Task<List<Component>> GetAllComponentsAsync(string project, CancellationToken cancellation = default)
        {
            return (await GetComponentsAsync(project, new ComponentsQuery { Size = int.MaxValue }, cancellation)).Data.ToList();
        }

        /// <inheritdoc/>
        public async Task<PagedList<Component>> GetComponentsAsync(ComponentsQuery query, CancellationToken cancellation = default)
        {
            return await GetComponentsAsync(_options.Project, query, cancellation);
        }

        /// <inheritdoc/>
        public async Task<PagedList<Component>> GetComponentsAsync(string project, ComponentsQuery query, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/content-types/components").AddQuery(query);

            return await GetAsync<PagedList<Component>>(request, cancellation);
        }

        #endregion

        #region -- publishing --

        /// <inheritdoc/>
        public async Task PublishContentAsync(string modelKey, string id, CancellationToken cancellation = default)
        {
            await PublishContentAsync(_options.Project, modelKey, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task PublishContentAsync(string project, string modelKey, string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/contents/{model}/{id}/publish")
                                .AddUrlSegment("model", modelKey)
                                .AddUrlSegment("id", id);

            await PostAsync(request, cancellation);
        }

        /// <inheritdoc/>
        public async Task UnpublishContentAsync(string modelKey, string id, CancellationToken cancellation = default)
        {
            await UnpublishContentAsync(_options.Project, modelKey, id, cancellation);
        }

        /// <inheritdoc/>
        public async Task UnpublishContentAsync(string project, string modelKey, string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/contents/{model}/{id}/unpublish")
                                .AddUrlSegment("model", modelKey)
                                .AddUrlSegment("id", id);

            await PostAsync(request, cancellation);
        }

        #endregion

        #region --  helpers --

        protected RestRequest CreateContentRequest(string project, string model, string resource)
        {
            if (!string.IsNullOrEmpty(resource))
            {
                return CreateProjectRequest(project, $"contents/{model}/{resource.TrimStart('/')}");
            }

            return CreateProjectRequest(project, $"contents/{model}");
        }

        protected RestRequest CreateMediaRequest(string project, string resource)
        {
            if (!string.IsNullOrEmpty(resource))
            {
                return CreateProjectRequest(project, $"media-library/media/{resource.TrimStart('/')}");
            }

            return CreateProjectRequest(project, $"media-library/media");
        }

        protected RestRequest CreateMediaFoldersRequest(string project, string resource)
        {
            if (!string.IsNullOrEmpty(resource))
            {
                return CreateProjectRequest(project, $"media-library/folders/{resource.TrimStart('/')}");
            }

            return CreateProjectRequest(project, $"media-library/folders");
        }

        #endregion
    }
}
