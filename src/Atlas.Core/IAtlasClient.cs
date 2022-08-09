using Atlas.Core.Models;
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
        Task<Content<T>> GetContent<T>(string modelKey, string id, CancellationToken cancellation = default) where T : class;
        Task<PagedList<Content<T>>> GetContents<T>(string modelKey, ContentsQuery query, CancellationToken cancellation = default) where T : class;
        Task<string> CreateContent<T>(string modelKey, T content, CancellationToken cancellation = default) where T : Content<T>;
        Task<string> UpdateContent<T>(string modelKey, T content, CancellationToken cancellation = default) where T : Content<T>;
        Task<string> DeleteContent<T>(string modelKey, string id, CancellationToken cancellation = default) where T : Content<T>;

        Task<Asset> GetAsset(string id, CancellationToken cancellation = default);
        Task<PagedList<Asset>> GetAssets(AssetsQuery query, CancellationToken cancellation = default);

        Task<List<Folder>> GetFolders(AssetsQuery query, CancellationToken cancellation = default);

        IAtlasClient UseToken(string token);
    }
}
