using Atlas.Core.Configuration;
using Atlas.Core.Models;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasUserClient"/> class.
        /// </summary>
        /// <param name="options">The configuration options <see cref="AtlasOptions"/>.</param>
        public AtlasManagementClient(AtlasOptions options)
        {
            InitClient(options);
        }

        /// <summary>
        /// Get all settings of the project
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Settings"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<Settings> GetSettings(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/settings");

            return await GetAsync<Settings>(request, cancellation);
        }
    }
}
