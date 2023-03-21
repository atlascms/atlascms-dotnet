using Atlas.Core.Configuration;
using Atlas.Core.Exceptions;
using Atlas.Core.Models;
using Atlas.Core.Models.Shared;
using Atlas.Core.Models.Collections;
using Atlas.Core.Models.Queries;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Atlas.Core.Infrastructure;

namespace Atlas.Core
{
    public class AtlasUserClient : ClientBase, IAtlasUserClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasUserClient"/> class.
        /// </summary>
        /// <param name="options">The configuration options <see cref="AtlasOptions"/>.</param>
        public AtlasUserClient(RestClient http, AtlasOptions options)
        {
            SetClient(http, options);
        }

        /// <inheritdoc cref="IAtlasUserClient.ChangePasswordAsync(string, string, CancellationToken)"/>
        public async Task ChangePasswordAsync(string id, string newPassword, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/users/{id}/change-password")
                                    .AddUrlSegment("id", id)
                                    .AddJsonBody(
                                        new
                                        {
                                            password = newPassword
                                        }
                                    );
        
            await PostAsync(request, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.CreateRoleAsync(Role, CancellationToken)"/>
        public async Task<string> CreateRoleAsync(Role role, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles").AddJsonBody(new
            {
                name = role.Name,
                permissions = role.Permissions,
            });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <inheritdoc cref="IAtlasUserClient.DeleteRoleAsync(string, CancellationToken)"/>
        public async Task DeleteRoleAsync(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.DeleteUserAsync(string, CancellationToken)"/>
        public async Task DeleteUserAsync(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/users/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.GetAllRolesAsync(CancellationToken)"/>
        public async Task<List<Role>> GetAllRolesAsync(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles");

            return await GetAsync<List<Role>>(request, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.GetUserAsync(string, CancellationToken)"/>
        public async Task<User<Dictionary<string, object>>> GetUserAsync(string id, CancellationToken cancellation = default)
        {
            return await GetUserAsync<Dictionary<string, object>>(id, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.GetUserAsync{T}(string, CancellationToken)"/>
        public async Task<User<T>> GetUserAsync<T>(string id, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/users/{id}").AddUrlSegment("id", id);

            return await GetAsync<User<T>>(request, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.GetUsersAsync(UsersQuery, CancellationToken)"/>
        public async Task<PagedList<User<Dictionary<string, object>>>> GetUsersAsync(UsersQuery query, CancellationToken cancellation = default)
        {
            return await GetUsersAsync<Dictionary<string, object>>(query, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.GetUsersAsync{T}(UsersQuery, CancellationToken)"/>
        public async Task<PagedList<User<T>>> GetUsersAsync<T>(UsersQuery query, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/users").AddQuery(query);

            return await GetAsync<PagedList<User<T>>>(request, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.CountUsersAsync(UsersQuery, CancellationToken)"/>
        public async Task<int> CountUsersAsync(UsersQuery query, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/users/count").AddQuery(query);

            return (await GetAsync<KeyResult<int>>(request, cancellation)).Result;
        }

        /// <inheritdoc cref="IAtlasUserClient.LoginAsync(string, string, CancellationToken)"/>
        public async Task<AuthToken> LoginAsync(string username, string password, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/users/login")
                                    .AddJsonBody(
                                        new
                                        {
                                            username = username,
                                            password = password
                                        }
                                    );

            try
            {
                return await PostAsync<AuthToken>(request, cancellation);
            }
            catch (AtlasException atlasException)
            {
                if (atlasException.HttpStatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    return null;
                }
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <inheritdoc cref="IAtlasUserClient.RegisterUserAsync(RegisterUser, CancellationToken)"/>
        public async Task<string> RegisterUserAsync(RegisterUser user, CancellationToken cancellation = default)
        {
            return await RegisterUserAsync<Dictionary<string, object>>(user, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.RegisterUserAsync{T}(RegisterUser{T}, CancellationToken)"/>
        public async Task<string> RegisterUserAsync<T>(RegisterUser<T> user, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/users/register").AddJsonBody(user);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <inheritdoc cref="IAtlasUserClient.UpdateRoleAsync(Role, CancellationToken)"/>
        public async Task UpdateRoleAsync(Role role, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles").AddJsonBody(role);

            await PutAsync(request, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.UpdateUserAsync(User, CancellationToken)"/>
        public async Task UpdateUserAsync(User user, CancellationToken cancellation = default)
        {
            await UpdateUserAsync<Dictionary<string, object>>(user, cancellation);
        }

        /// <inheritdoc cref="IAtlasUserClient.UpdateUserAsync{T}(User{T}, CancellationToken)"/>
        public async Task UpdateUserAsync<T>(User<T> user, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/users/{id}").AddUrlSegment("id", user.Id).AddJsonBody(user);

            await PutAsync(request, cancellation);
        }
    }
}
