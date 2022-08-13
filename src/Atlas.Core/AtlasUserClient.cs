using Atlas.Core.Configuration;
using Atlas.Core.Models;
using Atlas.Core.Models.Api;
using Atlas.Core.Models.Collections;
using Atlas.Core.Models.Queries;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public class AtlasUserClient : ClientBase, IAtlasUserClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasUserClient"/> class.
        /// </summary>
        /// <param name="options">The configuration options <see cref="AtlasOptions"/>.</param>
        public AtlasUserClient(AtlasOptions options)
        {
            InitClient(options);
        }

        public async Task ChangeUserPassword(string id, string newPassword, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateRole(Role role, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles").AddJsonBody(role);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;  
        }

        public async Task DeleteRole(string id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUser(string id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Role>> GetRoles(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles");

            return await GetAsync<List<Role>>(request, cancellation);
        }

        public async Task<User> GetUser(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/users/{id}").AddUrlSegment("id", id);

            return await GetAsync<User>(request, cancellation);
        }

        public async Task<User<T>> GetUser<T>(string id, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/users/{id}").AddUrlSegment("id", id);

            return await GetAsync<User<T>>(request, cancellation);
        }

        public async Task<PagedList<User>> GetUsers(UserQuery query, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<User<T>>> GetUsers<T>(UserQuery query, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterUser(RegisterUser user, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterUser<T>(RegisterUser<T> user, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRole(Role role, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser(User user, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser<T>(User<T> user, CancellationToken cancellation = default) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
