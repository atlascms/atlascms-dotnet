using Atlas.Core.Configuration;
using Atlas.Core.Extensions;
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

namespace Atlas.Core
{
    public class AtlasClient : ClientBase, IAtlasClient
    {
        /// <summary>
        /// The Users & Roles API Client
        /// </summary>
        public IAtlasUserClient Users { get; private set; }

        /// <summary>
        /// The Admin & Management API Client
        /// </summary>
        public IAtlasManagementClient Management { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasClient"/> class.
        /// </summary>
        /// <param name="options">The configuration options <see cref="AtlasOptions"/>.</param>
        public AtlasClient(RestClient http, AtlasOptions options, IAtlasUserClient userClient, IAtlasManagementClient managementClient)
        {
            InitClient(http, options);

            Users = userClient;
            Management = managementClient;  
        }

        #region -- contents --

        /// <summary>
        /// Create a content for a specific Model
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="content">The object to serialize in the Attributes prop of a Content.</param>
        /// <param name="locale">The optional locale value. If empty it will create the object under the default locale.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
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

        /// <summary>
        /// Create a content for a specific Model
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="content">The <see cref="Dictionary{TKey, TValue}"/> to serialize in the Attributes prop of a Content.</param>
        /// <param name="locale">The optional locale value. If empty it will create the object under the default locale.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateContent(string modelKey, Dictionary<string, object> content, string locale = "", CancellationToken cancellation = default)
        {
            return await CreateContent<Dictionary<string, object>>(modelKey, content, locale, cancellation);
        }

        /// <summary>
        /// Create a localized versione of the content with the ID provided
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to localize.</param>
        /// <param name="locale">The locale to create.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateTranslation(string modelKey, string id, string locale, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/contents/{model}/{id}/create-translation")
                               .AddUrlSegment("model", modelKey)
                               .AddJsonBody(
                                   new
                                   {
                                       Locale = locale
                                   }
                               );

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Delete the content with the ID provided
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteContent(string modelKey, string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/contents/{model}/{id}")
                                .AddUrlSegment("model", modelKey)
                                .AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Duplicate a content
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to duplicate.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content duplicated.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> Duplicate(string modelKey, string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/contents/{model}/{id}/duplicate")
                               .AddUrlSegment("model", modelKey)
                               .AddJsonBody(
                                   new
                                   {
                                       Locales = false
                                   }
                               );

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Duplicate a content and all its localized version
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to duplicate.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of ID of the contents duplicated.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<IEnumerable<string>> DuplicateAll(string modelKey, string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/contents/{model}/{id}/duplicate")
                               .AddUrlSegment("model", modelKey)
                               .AddJsonBody(
                                   new
                                   {
                                       Locales = true
                                   }
                               );

            return (await PostAsync<KeyResult<IEnumerable<string>>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Get the content with the ID provided
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Content{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<Content<T>> GetContent<T>(string modelKey, string id, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/contents/{model}/{id}").AddUrlSegment("model", modelKey).AddUrlSegment("id", id);

            return await GetAsync<Content<T>>(request, cancellation);
        }

        /// <summary>
        /// Get the content with the ID provided
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Content{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<Content<Dictionary<string, object>>> GetContent(string modelKey, string id, CancellationToken cancellation = default)
        {
            return await GetContent<Dictionary<string, object>>(modelKey, id, cancellation);
        }

        /// <summary>
        /// Get the paginated list of contents 
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{T}"/> with paging information and the list of <see cref="Content{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<PagedList<Content<T>>> GetContents<T>(string modelKey, ContentsQuery query, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/contents/{model}").AddUrlSegment("model", modelKey).AddQuery(query);

            return await GetAsync<PagedList<Content<T>>>(request, cancellation);
        }

        /// <summary>
        /// Get the paginated list of contents 
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{T}"/> with paging information and the list of <see cref="Content{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<PagedList<Content<Dictionary<string, object>>>> GetContents(string modelKey, ContentsQuery query, CancellationToken cancellation = default)
        {
            return await GetContents<Dictionary<string, object>>(modelKey, query, cancellation);
        }

        /// <summary>
        /// Update a content for a specific Model and ID
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="modelKey">The ID of content to update.</param>
        /// <param name="content">The object to serialize in the Attributes prop of a Content.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
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

        /// <summary>
        /// Update a content for a specific Model and ID
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <param name="content">The <see cref="Dictionary{TKey, TValue}"/> to serialize in the Attributes prop of a Content.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateContent(string modelKey, string id, Dictionary<string, object> content, CancellationToken cancellation = default)
        {
            await UpdateContent<Dictionary<string, object>>(modelKey, id, content, cancellation);
        }

        #endregion

        #region -- assets --

        /// <summary>
        /// Get the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Asset"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<Asset> GetAsset(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media/{id}").AddUrlSegment("id", id);

            return await GetAsync<Asset>(request, cancellation);
        }

        /// <summary>
        /// Get the paginated list of media 
        /// </summary>
        /// <param name="query">The optional <see cref="AssetsQuery"/> to filter the media.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Asset}"/> with paging information and the list of <see cref="Asset"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<PagedList<Asset>> GetAssets(AssetsQuery query, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media").AddQuery(query);

            return await GetAsync<PagedList<Asset>>(request, cancellation);
        }

        /// <summary>
        /// Upload a media to the Media Library
        /// </summary>
        /// <param name="fileName">The name of the file to store in the Media Library.</param>
        /// <param name="bytes">The bytes array of the file to send.</param>
        /// <param name="folder">The full path of the folder where to store the file.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> UploadAsset(string fileName, byte[] bytes, string folder = "/", CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media/upload")
                                .AddHeader("Content-Type", "multipart/form-data")
                                .AddParameter("folder", folder, ParameterType.RequestBody)
                                .AddFile("file", bytes, fileName);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Upload a media to the Media Library
        /// </summary>
        /// <param name="filePath">The full path of the local file to upload.</param>
        /// <param name="folder">The full path of the folder where to store the file.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> UploadAsset(string filePath, string folder = "/", CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media/upload")
                                .AddHeader("Content-Type", "multipart/form-data")
                                .AddParameter("folder", folder, ParameterType.RequestBody)
                                .AddFile("file", filePath);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Download the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The bytes array of the media of null if the ID has not been found.</returns>
        public async Task<byte[]> DownloadAsset(string id, CancellationToken cancellation = default)
        {
            var asset = await GetAsset(id, cancellation);

            if (asset != null)
            {
                return await new RestClient().DownloadDataAsync(new RestRequest(asset.Url), cancellation);
            }

            return null;
        }

        /// <summary>
        /// Download the stream of the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The Stream of the media of null if the ID has not been found.</returns>
        public async Task<Stream> DownloadAssetStream(string id, CancellationToken cancellation = default)
        {
            var asset = await GetAsset(id, cancellation);

            if (asset != null)
            {
                return await new RestClient().DownloadStreamAsync(new RestRequest(asset.Url), cancellation);
            }

            return null;
        }

        /// <summary>
        /// Delete the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteAsset(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/media/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Get the full structure of folders
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="List{Folder}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<List<Folder>> GetAllFolders(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders");

            return await GetAsync<List<Folder>>(request, cancellation);
        }

        /// <summary>
        /// Create a folder in the Media Library
        /// </summary>
        /// <param name="folder">The full path of the folder to create.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the folder created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateFolder(string folder, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders").AddJsonBody(new { folder = folder });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Rename a folder
        /// </summary>
        /// <param name="folder">The full path of the folder to move.</param>
        /// <param name="newName">The new name of the folder.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the renamed.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> RenameFolder(string folder, string newName, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders/rename").AddJsonBody(new { folder = folder, newName = newName });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Move a folder under another path
        /// </summary>
        /// <param name="folder">The full path of the folder to move.</param>
        /// <param name="moveTo">The full path of the destination parent folder.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the moved folder.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> MoveFolder(string folder, string moveTo, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders/move").AddJsonBody(new { folder = folder, moveTo = moveTo });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Delete a folder in the Media Library
        /// </summary>
        /// <param name="folder">The full path of the folder to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteFolder(string folder, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/media-library/folders").AddQueryParameter("folder", folder);

            await DeleteAsync(request, cancellation);
        }

        #endregion

        /// <summary>
        /// Set the token to use as Authorization on the next API call. 
        /// </summary>
        /// <param name="token">the token to use.</param>
        /// <returns>The instance of the client.</returns>
        public IAtlasClient UseToken(string token)
        {
            base.SetToken(token);

            return this;
        }

        /// <summary>
        /// Get the model with the ID provided
        /// </summary>
        /// <param name="id">The ID of the model to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Model"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<Model> GetModel(string id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the list of models
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="Model"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<List<Model>> GetAllModels(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the component with the ID provided
        /// </summary>
        /// <param name="id">The ID of the component to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Component"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<Model> GetComponent(string id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the list of components
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="Component"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<List<Model>> GetAllComponents(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
