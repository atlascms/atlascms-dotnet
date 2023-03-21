﻿using Atlas.Core.Models;
using Atlas.Core.Models.Collections;
using Atlas.Core.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public interface IAtlasUserClient : ISecuredClient
    {
        /// <summary>
        /// Set a new password for the user
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task ChangePasswordAsync(string id, string newPassword, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the user with the ID provided
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteUserAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the user with the ID provided
        /// </summary>
        /// <param name="id">The ID of the user to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="User{T}"/> with the Attribute as <see cref="Dictionary{TKey, TValue}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<User<Dictionary<string, object>>> GetUserAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the user with the ID provided
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the user.</typeparam>
        /// <param name="id">The ID of the user to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="User{T}"/> with the Attribute as <see cref="T"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<User<T>> GetUserAsync<T>(string id, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Get the paginated list of users 
        /// </summary>
        /// <param name="query">The optional <see cref="UsersQuery"/> to filter the users.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{User{Dictionary{TKey,TValue}}}"/> with paging information and the list of <see cref="User{Dictionary{TKey,TValue}}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<User<Dictionary<string, object>>>> GetUsersAsync(UsersQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paginated list of users 
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the users.</typeparam>
        /// <param name="query">The optional <see cref="UsersQuery"/> to filter the users.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{User{T}}"/> with paging information and the list of <see cref="User{T}"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<User<T>>> GetUsersAsync<T>(UsersQuery query, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Get the total count of users
        /// </summary>
        /// <param name="query">The optional <see cref="UsersQuery"/> to filter the users.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The number of users.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<int> CountUsersAsync(UsersQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="AuthToken"/> if authenticated, or null if it is not possible to authenticate the user.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<AuthToken> LoginAsync(string username, string password, CancellationToken cancellation = default);

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">The object to serialize as a <see cref="RegisterUserAsync"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the user created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> RegisterUserAsync(RegisterUser user, CancellationToken cancellation = default);

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the user</typeparam>
        /// <param name="user">The object to serialize as a <see cref="RegisterUserAsync{T}"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the user created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> RegisterUserAsync<T>(RegisterUser<T> user, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="user">The object to serialize as a <see cref="User"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateUserAsync(User user, CancellationToken cancellation = default);

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <typeparam name="T">The type of Attributes of the user.</typeparam>
        /// <param name="user">The object to serialize as a <see cref="User{T}"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateUserAsync<T>(User<T> user, CancellationToken cancellation = default) where T : class;

        /// <summary>
        /// Get the list of roles
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="List{Role}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<Role>> GetAllRolesAsync(CancellationToken cancellation = default);

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="user">The object to serialize as a <see cref="Role"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the role created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateRoleAsync(Role role, CancellationToken cancellation = default);

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="user">The object to serialize as a <see cref="Role"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateRoleAsync(Role role, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the role with the ID provided
        /// </summary>
        /// <param name="id">The role ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteRoleAsync(string id, CancellationToken cancellation = default);
    }
}
