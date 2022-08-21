using Atlas.Core.Configuration;
using Atlas.Core.Exceptions;
using Atlas.Core.Extensions;
using Atlas.Core.Models;
using Atlas.Core.Models.Collections;
using Atlas.Core.Models.Queries;
using Atlas.Core.Models.Shared;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public AtlasManagementClient(RestClient http, AtlasOptions options)
        {
            InitClient(http, options);
        }

        /// <summary>
        /// Set a new password for the account
        /// </summary>
        /// <param name="id">The account ID.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task ChangePassword(string id, string newPassword, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/accounts/{id}/change-password")
                                .AddUrlSegment("id", id)
                                .AddJsonBody(
                                    new
                                    {
                                        Password = newPassword
                                    }
                                );

            await PostAsync(request, cancellation);
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="account">The object to serialize as a <see cref="RegisterAccount"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the account created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateAccount(RegisterAccount account, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/accounts")
                                .AddJsonBody(account);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="role">The object to serialize as a <see cref="Role"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the role created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateAccountRole(AccountRole role, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/roles").AddJsonBody(
                new
                {
                    name = role.Name,
                    permissions = role.Permissions,
                });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Create a new api key
        /// </summary>
        /// <param name="apiKey">The object to serialize as a <see cref="ApiKey"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the webhook created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateApiKey(ApiKey apiKey, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/apikeys").AddJsonBody(
                new
                {
                    name = apiKey.Name,
                    isActive = apiKey.IsActive,
                    validFrom = apiKey.ValidFrom,
                    validTo = apiKey.ValidTo,
                    permissions = apiKey.Permissions,
                });

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Create a new webook
        /// </summary>
        /// <param name="webhook">The object to serialize as a <see cref="Webhook"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the webhook created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateWebhook(Webhook webhook, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/webhooks").AddJsonBody(webhook);

            return (await PostAsync<KeyResult<string>>(request, cancellation)).Result;
        }

        /// <summary>
        /// Delete the account with the ID provided
        /// </summary>
        /// <param name="id">The account ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteAccount(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/accounts/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Delete the role with the ID provided
        /// </summary>
        /// <param name="id">The role ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteAccountRole(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/roles/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Delete the api key with the ID provided
        /// </summary>
        /// <param name="id">The api key ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteApiKey(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/apikeys/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Delete the webhook with the ID provided
        /// </summary>
        /// <param name="id">The webhook ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteWebhook(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/webhooks/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Get the account with the ID provided
        /// </summary>
        /// <param name="id">The ID of the account to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Account"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<Account> GetAccount(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/accounts/{id}").AddUrlSegment("id", id);

            return await GetAsync<Account>(request, cancellation);
        }

        /// <summary>
        /// Get the list of roles
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="List{Role}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<List<AccountRole>> GetAllAccountRoles(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/roles");

            return await GetAsync<List<AccountRole>>(request, cancellation);
        }

        /// <summary>
        /// Get the paginated list of accounts 
        /// </summary>
        /// <param name="query">The optional <see cref="AccountsQuery"/> to filter the accounts.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="PagedList{Account}"/> with paging information and the list of <see cref="Account"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<PagedList<Account>> GetAccounts(AccountsQuery query, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/accounts").AddQuery(query);

            return await GetAsync<PagedList<Account>>(request, cancellation);
        }

        /// <summary>
        /// Get the api key with the ID provided
        /// </summary>
        /// <param name="id">The ID of the api key to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="ApiKey"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<ApiKey> GetApiKey(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/apikeys/{id}").AddUrlSegment("id", id);

            return await GetAsync<ApiKey>(request, cancellation);
        }

        /// <summary>
        /// Get the list of api keys
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="ApiKey"/> objects.</returns>
        public async Task<List<ApiKey>> GetAllApiKeys(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/apikeys");

            return await GetAsync<List<ApiKey>>(request, cancellation);
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

        /// <summary>
        /// Get the webook with the ID provided
        /// </summary>
        /// <param name="id">The ID of the webhook to fetch.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Webhook"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<Webhook> GetWebhook(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/webhooks/{id}").AddUrlSegment("id", id);

            return await GetAsync<Webhook>(request, cancellation);
        }

        /// <summary>
        /// Get the list of webhooks
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The list of <see cref="Webhook"/> objects.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<List<Webhook>> GetAllWebooks(CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/webhooks");

            return await GetAsync<List<Webhook>>(request, cancellation);
        }

        /// <summary>
        /// Authenticate the account
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="AuthToken"/> if authenticated, or null if it is not possible to authenticate the account.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<AuthToken> Login(string username, string password, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/login")
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

        /// <summary>
        /// Update an existing account
        /// </summary>
        /// <param name="account">The object to serialize as a <see cref="Account"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateAccount(Account account, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/accounts/{id}")
                                .AddUrlSegment("id", account.Id)
                                .AddJsonBody(account);

            await PutAsync(request, cancellation);
        }

        /// <summary>
        /// Get the list of roles
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="List{Role}"/>.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateAccountRole(AccountRole role, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/roles/{id}")
                                .AddUrlSegment("id", role.Id)
                                .AddJsonBody(role);

            await PutAsync(request, cancellation);
        }

        /// <summary>
        /// Update an existing webhook
        /// </summary>
        /// <param name="apiKey">The object to serialize as a <see cref="ApiKey"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateApiKey(ApiKey apiKey, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/apikeys/{id}")
                                .AddUrlSegment("id", apiKey.Id)
                                .AddJsonBody(apiKey);

            await PutAsync(request, cancellation);
        }

        /// <summary>
        /// Update an existing webhook
        /// </summary>
        /// <param name="webhook">The object to serialize as a <see cref="Webhook"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateWebhook(Webhook webhook, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/admin/webhooks/{id}")
                                .AddUrlSegment("id", webhook.Id)
                                .AddJsonBody(webhook);

            await PutAsync(request, cancellation);
        }

        /// <summary>
        /// Create a new model
        /// </summary>
        /// <param name="model">The object to serialize as a <see cref="Model"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the webhook created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateModel(Model model, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete the model with the ID provided
        /// </summary>
        /// <param name="id">The model ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteModel(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/content-types/models/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Update an existing webhook
        /// </summary>
        /// <param name="model">The object to serialize as a <see cref="Model"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateModel(Model model, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/content-types/models/{id}")
                                .AddUrlSegment("id", model.Id)
                                .AddJsonBody(model);

            await PutAsync(request, cancellation);
        }

        /// <summary>
        /// Create a new component
        /// </summary>
        /// <param name="component">The object to serialize as a <see cref="Component"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The ID of the webhook created.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task<string> CreateComponent(Component component, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete the component with the ID provided
        /// </summary>
        /// <param name="id">The model ID.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task DeleteComponent(string id, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/content-types/components/{id}").AddUrlSegment("id", id);

            await DeleteAsync(request, cancellation);
        }

        /// <summary>
        /// Update an existing component
        /// </summary>
        /// <param name="component">The object to serialize as a <see cref="Component"/>.</param>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        public async Task UpdateComponent(Component component, CancellationToken cancellation = default)
        {
            var request = new RestRequest("/api/content-types/components/{id}")
                                .AddUrlSegment("id", component.Id)
                                .AddJsonBody(component);

            await PutAsync(request, cancellation);
        }
    }
}
