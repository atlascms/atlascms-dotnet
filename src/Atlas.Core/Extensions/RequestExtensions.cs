using Atlas.Core.Models.Queries;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Extensions
{
    public static class RequestExtensions
    {
        private const string AuthParameter = "Authorization";

        public static RestRequest AddBearerToken(this RestRequest request, string token)
        {
            if (request.Parameters.Any(p => p.Name == AuthParameter))
            {
                request.Parameters.RemoveParameter(AuthParameter);
            }

            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader(AuthParameter, $"Bearer {token}");
            }

            return request;
        }

        public static RestRequest AddQuery(this RestRequest request, ContentsQuery query)
        {
            if (query == null)
            {
                return request;
            }

            if (!string.IsNullOrEmpty(query.Filters))
            {
                var filters = query.Filters.Split('&');

                foreach (var filter in filters)
                { 
                    var parts = filter.Split('=');

                    if (parts.Length == 2)
                    {
                        request.AddQueryParameter(parts[0], parts[1]);
                    }
                }
            }

            if (query.Resolvers != ContentResolver.None)
            {
                var resolvers = new StringBuilder();

                if (query.Resolvers.HasFlag(ContentResolver.Media))
                {
                    resolvers.Append("media,");
                }
                if (query.Resolvers.HasFlag(ContentResolver.MediaGallery))
                {
                    resolvers.Append("mediagallery,");
                }
                if (query.Resolvers.HasFlag(ContentResolver.References))
                {
                    resolvers.Append("references,");
                }

                request.AddQueryParameter("resolve", resolvers.ToString().TrimEnd(','));
            }

            if (!string.IsNullOrEmpty(query.Search))
            {
                request.AddQueryParameter("search", query.Search);
            }

            return request.AddPaging(query).AddSorting(query);
        }

        public static RestRequest AddQuery(this RestRequest request, AssetsQuery query)
        {
            if (query == null)
            {
                return request;
            }

            if (!string.IsNullOrEmpty(query.Filters))
            {
                var filters = query.Filters.Split('&');

                foreach (var filter in filters)
                {
                    var parts = filter.Split('=');

                    if (parts.Length == 2)
                    {
                        request.AddQueryParameter(parts[0], parts[1]);
                    }
                }
            }

            if (!string.IsNullOrEmpty(query.Folder))
            {
                request.AddQueryParameter("folder", query.Folder);
            }

            return request.AddPaging(query).AddSorting(query);
        }

        public static RestRequest AddPaging(this RestRequest request, ListQuery query)
        {
            if (query == null)
            {
                return request;
            }

            request.AddQueryParameter("page", query.Page);
            request.AddQueryParameter("size", query.Size);

            return request;
        }

        public static RestRequest AddSorting(this RestRequest request, ListQuery query)
        {
            if (query == null)
            {
                return request;
            }

            request.AddQueryParameter("sort", query.Sort);

            return request;
        }
    }
}
