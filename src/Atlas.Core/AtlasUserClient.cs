using Atlas.Core.Configuration;
using Atlas.Core.Exceptions;
using Atlas.Core.Extensions;
using Atlas.Core.Models;
using Atlas.Core.Models.Api;
using Atlas.Core.Models.Collections;
using Atlas.Core.Models.Queries;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        /// <summary>
        /// Set a new password for the user
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task ChangePassword(string id, string newPassword, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/users/{id}")
                                    .AddJsonBody(
                                        new
                                        {
                                            password = newPassword
                                        }
                                    );

            await PostAsync(request, cancellation);
        }

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="user">The object to serialize as a <see cref="Role"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the role created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateRole(Role role, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles").AddJsonBody(role);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Delete the role with the ID provided
        /// </summary>
        /// <param name="id">The role ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteRole(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Delete the user with the ID provided
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteUser(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/users/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Get the list of roles
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="List{Role}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<List<Role>> GetRoles(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles");

            return await GetAsync<List<Role>>(request, cancellation);
        }

        /// <summary>
        /// Get the user with the ID provided
        /// </summary>
        /// <param name="id">The ID of the user to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="User{T}"/> with the Attribute as <see cref="Dictionary{TKey, TValue}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<User<Dictionary<string, object>>> GetUser(string id, CancellationToken cancellation = default)
        {
            return await GetUser<Dictionary<string, object>>(id, cancellation);
        }

        /// <summary>
        /// Get the user with the ID provided
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the user.</typeparam>
        /// <param name="id">The ID of the user to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="User{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<User<T>> GetUser<T>(string id, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/users/{id}").AddUrlSegment("id", id);

            return await GetAsync<User<T>>(request, cancellation);
        }

        /// <summary>
        /// Get the paginated list of users 
        /// </summary>
        /// <param name="query">The optional <see cref="UserQuery"/> to filter the users.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{User{Dictionary{TKey,TValue}}}"/> with paging information and the list of <see cref="User{Dictionary{TKey,TValue}}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<PagedList<User<Dictionary<string, object>>>> GetUsers(UserQuery query, CancellationToken cancellation = default)
        {
            return await GetUsers<Dictionary<string, object>>(query, cancellation);
        }

        /// <summary>
        /// Get the paginated list of users 
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the users.</typeparam>
        /// <param name="query">The optional <see cref="UserQuery"/> to filter the users.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{User{T}}"/> with paging information and the list of <see cref="User{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<PagedList<User<T>>> GetUsers<T>(UserQuery query, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/users").AddQuery(query);

            return await GetAsync<PagedList<User<T>>>(request, cancellation);
        }

        /// <summary>
        /// Authenticate a user
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="AuthToken"/> if authenticated, or null if it is not possible to authenticate the user.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<AuthToken> Login(string username, string password, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/users/login")
                                    .AddJsonBody(
                                        new { 
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

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">The object to serialize as a <see cref="RegisterUser"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the user created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> RegisterUser(RegisterUser user, CancellationToken cancellation = default)
        {
            return await RegisterUser<Dictionary<string, object>>(user, cancellation);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the user</typeparam>
        /// <param name="user">The object to serialize as a <see cref="RegisterUser{T}"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the user created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> RegisterUser<T>(RegisterUser<T> user, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/users/register").AddJsonBody(user);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="user">The object to serialize as a <see cref="Role"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateRole(Role role, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/roles").AddJsonBody(role);

            await PutAsync(request, cancellation);
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="user">The object to serialize as a <see cref="User"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateUser(User user, CancellationToken cancellation = default)
        {
            await UpdateUser<Dictionary<string, object>>(user, cancellation);
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the user.</typeparam>
        /// <param name="user">The object to serialize as a <see cref="User{T}"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateUser<T>(User<T> user, CancellationToken cancellation = default) where T : class
        {
            var request = new RestRequest("/api/users/{id}").AddUrlSegment("id", user.Id).AddJsonBody(user);

            await PutAsync(request, cancellation);
        }
    }
}
