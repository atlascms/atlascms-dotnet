using Atlas.Core.Models;
using Atlas.Core.Models.Shared;
using Atlas.Core.Models.Collections;
using Atlas.Core.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public interface IAtlasClient : ISecuredClient
    {
        /// <summary>
        /// Create a content for a specific Model
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="content">The object to serialize in the Attributes prop of a Content.</param>
        /// <param name="locale">The optional locale value. If empty it will create the object under the default locale.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateContentAsync<T>(string modelKey, T content, string locale = "", CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Create a content for a specific Model
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="content">The object to serialize in the Attributes prop of a Content.</param>
        /// <param name="locale">The optional locale value. If empty it will create the object under the default locale.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateContentAsync<T>(string project, string modelKey, T content, string locale = "", CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Create a content for a specific Model
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="content">The <see cref="Dictionary{TKey, TValue}"/> to serialize in the Attributes prop of a Content.</param>
        /// <param name="locale">The optional locale value. If empty it will create the object under the default locale.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateContentAsync(string modelKey, Dictionary<string, object> content, string locale = "", CancellationToken cancellation = default);

        /// <summary>
        /// Create a content for a specific Model
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="content">The <see cref="Dictionary{TKey, TValue}"/> to serialize in the Attributes prop of a Content.</param>
        /// <param name="locale">The optional locale value. If empty it will create the object under the default locale.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateContentAsync(string project, string modelKey, Dictionary<string, object> content, string locale = "", CancellationToken cancellation = default);

        /// <summary>
        /// Create a localized versione of the content with the ID provided
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to localize.</param>
        /// <param name="locale">The locale to create.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateTranslationAsync(string modelKey, string id, string locale, CancellationToken cancellation = default);

        /// <summary>
        /// Create a localized versione of the content with the ID provided
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to localize.</param>
        /// <param name="locale">The locale to create.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateTranslationAsync(string project, string modelKey, string id, string locale, CancellationToken cancellation = default);

        /// <summary>
        /// Duplicate a content
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to duplicate.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content duplicated.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> DuplicateAsync(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Duplicate a content
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to duplicate.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content duplicated.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> DuplicateAsync(string project, string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Duplicate a content and all its localized version
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to duplicate.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of ID of the contents duplicated.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<IEnumerable<string>> DuplicateAllAsync(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Duplicate a content and all its localized version
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to duplicate.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of ID of the contents duplicated.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<IEnumerable<string>> DuplicateAllAsync(string project, string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the content with the ID provided
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteContentAsync(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the content with the ID provided
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteContentAsync(string project, string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the content with the ID provided
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Content{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Content<T>> GetContentAsync<T>(string modelKey, string id, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Get the content with the ID provided
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Content{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Content<T>> GetContentAsync<T>(string project, string modelKey, string id, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Get the content with the ID provided
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Content{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Content<Dictionary<string,object>>> GetContentAsync(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the content with the ID provided
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Content{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Content<Dictionary<string, object>>> GetContentAsync(string project, string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paginated list of contents 
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{T}"/> with paging information and the list of <see cref="Content{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Content<T>>> GetContentsAsync<T>(string modelKey, ContentsQuery query, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Get the paginated list of contents 
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{T}"/> with paging information and the list of <see cref="Content{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Content<T>>> GetContentsAsync<T>(string project, string modelKey, ContentsQuery query, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Get the paginated list of contents 
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{T}"/> with paging information and the list of <see cref="Content{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Content<Dictionary<string, object>>>> GetContentsAsync(string modelKey, ContentsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paginated list of contents 
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{T}"/> with paging information and the list of <see cref="Content{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Content<Dictionary<string, object>>>> GetContentsAsync(string project, string modelKey, ContentsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Get the total count of contents
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The number of contents.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<int> CountContentsAsync(string modelKey, ContentsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Get the total count of contents
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The number of contents.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<int> CountContentsAsync(string project, string modelKey, ContentsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Update a content for a specific Model and ID
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <param name="content">The object to serialize in the Attributes prop of a Content.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateContentAsync<T>(string modelKey, string id, T content, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Update a content for a specific Model and ID
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <param name="content">The object to serialize in the Attributes prop of a Content.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateContentAsync<T>(string project, string modelKey, string id, T content, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Update a content for a specific Model and ID
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <param name="content">The <see cref="Dictionary{TKey, TValue}"/> to serialize in the Attributes prop of a Content.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateContentAsync(string modelKey, string id, Dictionary<string, object> content, CancellationToken cancellation = default);

        /// <summary>
        /// Update a content for a specific Model and ID
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <param name="content">The <see cref="Dictionary{TKey, TValue}"/> to serialize in the Attributes prop of a Content.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateContentAsync(string project, string modelKey, string id, Dictionary<string, object> content, CancellationToken cancellation = default);

        /// <summary>
        /// Publish a content
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task PublishContentAsync(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Publish a content
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task PublishContentAsync(string project, string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Unpublish a content
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UnpublishContentAsync(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Unpublish a content
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UnpublishContentAsync(string project, string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteAssetAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the media with the ID provided
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteAssetAsync(string project, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Download the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The bytes array of the media of null if the ID has not been found.</returns>
        Task<byte[]> DownloadAssetAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Download the media with the ID provided
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The bytes array of the media of null if the ID has not been found.</returns>
        Task<byte[]> DownloadAssetAsync(string project, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Download the stream of the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The Stream of the media of null if the ID has not been found.</returns>
        Task<Stream> DownloadAssetStreamAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Download the stream of the media with the ID provided
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The Stream of the media of null if the ID has not been found.</returns>
        Task<Stream> DownloadAssetStreamAsync(string project, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Asset"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Asset> GetAssetAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the media with the ID provided
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="id">The ID of the media to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Asset"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Asset> GetAssetAsync(string project, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paginated list of media 
        /// </summary>
        /// <param name="query">The optional <see cref="AssetsQuery"/> to filter the media.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Asset}"/> with paging information and the list of <see cref="Asset"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Asset>> GetAssetsAsync(AssetsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paginated list of media 
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="query">The optional <see cref="AssetsQuery"/> to filter the media.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Asset}"/> with paging information and the list of <see cref="Asset"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Asset>> GetAssetsAsync(string project, AssetsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Set the tags to an Asset
        /// </summary>
        /// <param name="id">The ID of the media to fetch.</param>
        /// <param name="tags">The list of tags to assign.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task SetAssetTagsAsync(string id, IEnumerable<string> tags, CancellationToken cancellation = default);

        /// <summary>
        /// Set the tags to an Asset
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="id">The ID of the media to fetch.</param>
        /// <param name="tags">The list of tags to assign.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task SetAssetTagsAsync(string project, string id, IEnumerable<string> tags, CancellationToken cancellation = default);

        /// <summary>
        /// Upload a media to the Media Library
        /// </summary>
        /// <param name="fileName">The name of the file to store in the Media Library.</param>
        /// <param name="bytes">The bytes array of the file to send.</param>
        /// <param name="folder">The full path of the folder where to store the file.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> UploadAssetAsync(string fileName, byte[] bytes, string folder = "/", CancellationToken cancellation = default);

        /// <summary>
        /// Upload a media to the Media Library
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="fileName">The name of the file to store in the Media Library.</param>
        /// <param name="bytes">The bytes array of the file to send.</param>
        /// <param name="folder">The full path of the folder where to store the file.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> UploadAssetAsync(string project, string fileName, byte[] bytes, string folder = "/", CancellationToken cancellation = default);

        /// <summary>
        /// Upload a media to the Media Library
        /// </summary>
        /// <param name="filePath">The full path of the local file to upload.</param>
        /// <param name="folder">The full path of the folder where to store the file.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> UploadAssetAsync(string filePath, string folder = "/", CancellationToken cancellation = default);

        /// <summary>
        /// Upload a media to the Media Library
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="filePath">The full path of the local file to upload.</param>
        /// <param name="folder">The full path of the folder where to store the file.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> UploadAssetAsync(string project, string filePath, string folder = "/", CancellationToken cancellation = default);

        /// <summary>
        /// Create a folder in the Media Library
        /// </summary>
        /// <param name="folder">The full path of the folder to create.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the folder created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateFolderAsync(string folder, CancellationToken cancellation = default);

        /// <summary>
        /// Create a folder in the Media Library
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="folder">The full path of the folder to create.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the folder created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateFolderAsync(string project, string folder, CancellationToken cancellation = default);

        /// <summary>
        /// Delete a folder in the Media Library
        /// </summary>
        /// <param name="folder">The full path of the folder to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteFolderAsync(string folder, CancellationToken cancellation = default);

        /// <summary>
        /// Delete a folder in the Media Library
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="folder">The full path of the folder to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteFolderAsync(string project, string folder, CancellationToken cancellation = default);

        /// <summary>
        /// Get the full structure of folders
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="List{Folder}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<Folder>> GetAllFoldersAsync(CancellationToken cancellation = default);

        /// <summary>
        /// Get the full structure of folders
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="List{Folder}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<Folder>> GetAllFoldersAsync(string project, CancellationToken cancellation = default);

        /// <summary>
        /// Move a folder under another path
        /// </summary>
        /// <param name="folder">The full path of the folder to move.</param>
        /// <param name="moveTo">The full path of the destination parent folder.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the moved folder.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> MoveFolderAsync(string folder, string moveTo, CancellationToken cancellation = default);

        /// <summary>
        /// Move a folder under another path
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="folder">The full path of the folder to move.</param>
        /// <param name="moveTo">The full path of the destination parent folder.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the moved folder.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> MoveFolderAsync(string project, string folder, string moveTo, CancellationToken cancellation = default);

        /// <summary>
        /// Rename a folder
        /// </summary>
        /// <param name="folder">The full path of the folder to move.</param>
        /// <param name="newName">The new name of the folder.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the renamed.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> RenameFolderAsync(string folder, string newName, CancellationToken cancellation = default);

        /// <summary>
        /// Rename a folder
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="folder">The full path of the folder to move.</param>
        /// <param name="newName">The new name of the folder.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the renamed.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> RenameFolderAsync(string project, string folder, string newName, CancellationToken cancellation = default);

        /// <summary>
        /// Get the model with the ID provided
        /// </summary>
        /// <param name="id">The ID of the model to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Model"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Model> GetModelAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the model with the ID provided
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="id">The ID of the model to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Model"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Model> GetModelAsync(string project, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paged list of models
        /// </summary>
        /// <param name="query">The optional <see cref="ModelsQuery"/> to filter the media.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Model}"/> with paging information and the list of <see cref="Model"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Model>> GetModelsAsync(ModelsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paged list of models
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="query">The optional <see cref="ModelsQuery"/> to filter the media.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Model}"/> with paging information and the list of <see cref="Model"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Model>> GetModelsAsync(string project, ModelsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Get the list of models
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="Model"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<Model>> GetAllModelsAsync(CancellationToken cancellation = default);

        /// <summary>
        /// Get the list of models
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="Model"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<Model>> GetAllModelsAsync(string project, CancellationToken cancellation = default);

        /// <summary>
        /// Get the component with the ID provided
        /// </summary>
        /// <param name="id">The ID of the component to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Component"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Component> GetComponentAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the component with the ID provided
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="id">The ID of the component to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Component"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Component> GetComponentAsync(string project, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the list of components
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="Component"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<Component>> GetAllComponentsAsync(CancellationToken cancellation = default);

        /// <summary>
        /// Get the list of components
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="Component"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<Component>> GetAllComponentsAsync(string project, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paged list of components
        /// </summary>
        /// <param name="query">The optional <see cref="ComponentsQuery"/> to filter the media.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Component}"/> with paging information and the list of <see cref="Component"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Component>> GetComponentsAsync(ComponentsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paged list of components
        /// </summary>
        /// <param name="project">The Project Key.</param>
        /// <param name="query">The optional <see cref="ComponentsQuery"/> to filter the media.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Component}"/> with paging information and the list of <see cref="Component"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Component>> GetComponentsAsync(string project, ComponentsQuery query, CancellationToken cancellation = default);
    }
}