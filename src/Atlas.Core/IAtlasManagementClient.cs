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
    public interface IAtlasManagementClient : ISecuredClient
    {
        /// <summary>
        /// Get all settings of the project
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Settings"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Settings> GetSettingsAsync(CancellationToken cancellation = default);

        /// <summary>
        /// Set a new password for the account
        /// </summary>
        /// <param name="id">The account ID.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task ChangePasswordAsync(string id, string newPassword, CancellationToken cancellation = default);

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="account">The object to serialize as a <see cref="RegisterAccount"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the account created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateAccountAsync(RegisterAccount account, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the account with the ID provided
        /// </summary>
        /// <param name="id">The account ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteAccountAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the account with the ID provided
        /// </summary>
        /// <param name="id">The ID of the account to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Account"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Account> GetAccountAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the paginated list of accounts 
        /// </summary>
        /// <param name="query">The optional <see cref="AccountsQuery"/> to filter the accounts.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Account}"/> with paging information and the list of <see cref="Account"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<PagedList<Account>> GetAccountsAsync(AccountsQuery query, CancellationToken cancellation = default);

        /// <summary>
        /// Authenticate the account
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="AuthToken"/> if authenticated, or null if it is not possible to authenticate the account.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<AuthToken> LoginAsync(string username, string password, CancellationToken cancellation = default);

        /// <summary>
        /// Update an existing account
        /// </summary>
        /// <param name="account">The object to serialize as a <see cref="Account"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateAccountAsync(Account account, CancellationToken cancellation = default);

        /// <summary>
        /// Get the list of roles
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="List{Role}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<AccountRole>> GetAllAccountRolesAsync(CancellationToken cancellation = default);

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="role">The object to serialize as a <see cref="Role"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the role created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateAccountRoleAsync(AccountRole role, CancellationToken cancellation = default);

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="role">The object to serialize as a <see cref="Role"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateAccountRoleAsync(AccountRole role, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the role with the ID provided
        /// </summary>
        /// <param name="id">The role ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteAccountRoleAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Create a new webook
        /// </summary>
        /// <param name="webhook">The object to serialize as a <see cref="Webhook"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the webhook created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateWebhookAsync(Webhook webhook, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the webhook with the ID provided
        /// </summary>
        /// <param name="id">The webhook ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteWebhookAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the webook with the ID provided
        /// </summary>
        /// <param name="id">The ID of the webhook to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Webhook"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Webhook> GetWebhookAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the list of webhooks
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="Webhook"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<Webhook>> GetAllWebooksAsync(CancellationToken cancellation = default);

        /// <summary>
        /// Update an existing webhook
        /// </summary>
        /// <param name="webhook">The object to serialize as a <see cref="Webhook"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateWebhookAsync(Webhook webhook, CancellationToken cancellation = default);

        /// <summary>
        /// Create a new api key
        /// </summary>
        /// <param name="apiKey">The object to serialize as a <see cref="ApiKey"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the webhook created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateApiKeyAsync(ApiKey apiKey, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the api key with the ID provided
        /// </summary>
        /// <param name="id">The api key ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteApiKeyAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the api key with the ID provided
        /// </summary>
        /// <param name="id">The ID of the api key to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="ApiKey"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<ApiKey> GetApiKeyAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Get the list of api keys
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="ApiKey"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<List<ApiKey>> GetAllApiKeysAsync(CancellationToken cancellation = default);

        /// <summary>
        /// Update an existing webhook
        /// </summary>
        /// <param name="apiKey">The object to serialize as a <see cref="ApiKey"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateApiKeyAsync(ApiKey apiKey, CancellationToken cancellation = default);

        /// <summary>
        /// Create a new model
        /// </summary>
        /// <param name="model">The object to serialize as a <see cref="Model"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the webhook created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateModelAsync(Model model, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the model with the ID provided
        /// </summary>
        /// <param name="id">The model ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteModelAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Update an existing webhook
        /// </summary>
        /// <param name="model">The object to serialize as a <see cref="Model"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateModelAsync(Model model, CancellationToken cancellation = default);

        /// <summary>
        /// Create a new component
        /// </summary>
        /// <param name="component">The object to serialize as a <see cref="Component"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the webhook created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<string> CreateComponentAsync(Component component, CancellationToken cancellation = default);

        /// <summary>
        /// Delete the component with the ID provided
        /// </summary>
        /// <param name="id">The model ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task DeleteComponentAsync(string id, CancellationToken cancellation = default);

        /// <summary>
        /// Update an existing component
        /// </summary>
        /// <param name="component">The object to serialize as a <see cref="Component"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task UpdateComponentAsync(Component component, CancellationToken cancellation = default);

    }
}
