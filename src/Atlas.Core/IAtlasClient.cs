using Atlas.Core.Models;
using Atlas.Core.Models.Api;
using Atlas.Core.Models.Collections;
using Atlas.Core.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public interface IAtlasClient 
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
        Task<string> CreateContent<T>(string modelKey, T content, string locale = "", CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Create a content for a specific Model
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="content">The <see cref="Dictionary{TKey, TValue}"/> to serialize in the Attributes prop of a Content.</param>
        /// <param name="locale">The optional locale value. If empty it will create the object under the default locale.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateContent(string modelKey, Dictionary<string, object> content, string locale = "", CancellationToken cancellation = default);

        /// <summary>
        /// Create a localized versione of the content with the ID provided
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to localize.</param>
        /// <param name="locale">The locale to create.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateTranslation(string modelKey, string id, string locale, CancellationToken cancellation = default);

        /// <summary>
        /// Duplicate a content
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to duplicate.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the content duplicated.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> Duplicate(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Duplicate a content and all its localized version
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to duplicate.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of ID of the contents duplicated.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<IEnumerable<string>> DuplicateAll(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the content with the ID provided
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteContent(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the content with the ID provided
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Content{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Content<T>> GetContent<T>(string modelKey, string id, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Get the content with the ID provided
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of the content to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Content{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Content<Dictionary<string,object>>> GetContent(string modelKey, string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paginated list of contents 
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the content</typeparam>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{T}"/> with paging information and the list of <see cref="Content{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Content<T>>> GetContents<T>(string modelKey, ContentsQuery query, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Get the paginated list of contents 
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="query">The optional <see cref="ContentsQuery"/> to filter the contents.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{T}"/> with paging information and the list of <see cref="Content{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Content<Dictionary<string, object>>>> GetContents(string modelKey, ContentsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Update a content for a specific Model and ID
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <param name="content">The object to serialize in the Attributes prop of a Content.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateContent<T>(string modelKey, string id, T content, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Update a content for a specific Model and ID
        /// </summary>
        /// <param name="modelKey">The Key of the Model.</param>
        /// <param name="id">The ID of content to update.</param>
        /// <param name="content">The <see cref="Dictionary{TKey, TValue}"/> to serialize in the Attributes prop of a Content.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateContent(string modelKey, string id, Dictionary<string, object> content, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteAsset(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Download the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The bytes array of the media of null if the ID has not been found.</returns>
        Task<byte[]> DownloadAsset(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Download the stream of the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The Stream of the media of null if the ID has not been found.</returns>
        Task<Stream> DownloadAssetStream(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the media with the ID provided
        /// </summary>
        /// <param name="id">The ID of the media to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Asset"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Asset> GetAsset(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paginated list of media 
        /// </summary>
        /// <param name="query">The optional <see cref="AssetsQuery"/> to filter the media.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Asset}"/> with paging information and the list of <see cref="Asset"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Asset>> GetAssets(AssetsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Upload a media to the Media Library
        /// </summary>
        /// <param name="fileName">The name of the file to store in the Media Library.</param>
        /// <param name="bytes">The bytes array of the file to send.</param>
        /// <param name="folder">The full path of the folder where to store the file.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> UploadAsset(string fileName, byte[] bytes, string folder = "/", CancellationToken cancellation = default);

        /// <summary>
        /// Upload a media to the Media Library
        /// </summary>
        /// <param name="filePath">The full path of the local file to upload.</param>
        /// <param name="folder">The full path of the folder where to store the file.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the media created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> UploadAsset(string filePath, string folder = "/", CancellationToken cancellation = default);

        /// <summary>
        /// Create a folder in the Media Library
        /// </summary>
        /// <param name="folder">The full path of the folder to create.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the folder created</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateFolder(string folder, CancellationToken cancellation = default);

        /// <summary>
        /// Delete a folder in the Media Library
        /// </summary>
        /// <param name="folder">The full path of the folder to delete.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteFolder(string folder, CancellationToken cancellation = default);

        /// <summary>
        /// Get the full structure of folders
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="List{Folder}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<Folder>> GetFolders(CancellationToken cancellation = default);

        /// <summary>
        /// Move a folder under another path
        /// </summary>
        /// <param name="folder">The full path of the folder to move.</param>
        /// <param name="moveTo">The full path of the destination parent folder.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the moved folder.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> MoveFolder(string folder, string moveTo, CancellationToken cancellation = default);

        /// <summary>
        /// Rename a folder
        /// </summary>
        /// <param name="folder">The full path of the folder to move.</param>
        /// <param name="newName">The new name of the folder.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The full path of the renamed.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> RenameFolder(string folder, string newName, CancellationToken cancellation = default);

        /// <summary>
        /// Set the token to use as Authorization on the next API call. 
        /// </summary>
        /// <param name="token">the token to use.</param>
        /// <returns>The instance of the client.</returns>
        IAtlasClient UseToken(string token);

        /// <summary>
        /// The Users & Roles API Client
        /// </summary>
        IAtlasUserClient Users { get; }

        /// <summary>
        /// The Admin & Management API Client
        /// </summary>
        IAtlasManagementClient Management { get; }
    }
}