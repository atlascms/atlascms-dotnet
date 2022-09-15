using Atlas.Core.Configuration;
using Atlas.Core.Exceptions;
using Atlas.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public abstract class ClientBase 
    {
        private RestClient _http;
        protected AtlasOptions _options;
        
        private string Os => IsWindows ? "Windows" : IsMacOS ? "macOS" : "Linux";

        private static bool IsWindows
        {
            get
            {
                try
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                }
                catch (PlatformNotSupportedException) { }
                return false;
            }
        }

        private static bool IsMacOS
        {
            get
            {
                try
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
                }
                catch (PlatformNotSupportedException) { }
                return false;
            }
        }

        private static bool IsLinux
        {
            get
            {
                try
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
                }
                catch (PlatformNotSupportedException) { }
                return false;
            }
        }

        private static string AppId => "atlas-dotnet";

        private static string Version => typeof(ClientBase).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        protected string _token = "";

        protected RestClient CreateClient(AtlasOptions options)
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri(options.BaseUrl)
            };

            var client = new RestClient(restClientOptions).UseNewtonsoftJson(options.SerializerOptions);

            SetClient(client, options);

            return client;
        }

        protected void SetClient(RestClient http, AtlasOptions options)
        {
            _http = http;
            _options = options; 
        }

        /// <summary>
        /// Add the Atlas SDK Client Heders
        /// </summary>
        /// <param name="request">The <see cref="RestRequest"/> to send</param>
        protected void EnrichRequestHeaders(RestRequest request)
        { 
            request.AddBearerToken(string.IsNullOrEmpty(_token) ? _options.ApiKey : _token);
            request.AddHeader("Content-Type", $"application/json");
            request.AddHeader("X-Atlas-SDK", $"{AppId}/{Version}; OS {Os};");
            request.AddHeader("User-Agent", "Atlas CMS .NET Client SDK");
        }

        /// <summary>
        /// Execute a Get Request Async
        /// </summary>
        /// <typeparam name="T">The type of response to deserialize.</typeparam>
        /// <param name="request">The <see cref="RestRequest"/> to send.</param>
        /// <param name="cancellation"></param>
        /// <returns>Returns the object <see cref="T"/></returns>
        /// <exception cref="AtlasException">The API exception</exception>
        protected async Task<T> GetAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequestAsync<T>(request, Method.Get, cancellation);
        }

        /// <summary>
        /// Execute a Post Request Async
        /// </summary>
        /// <typeparam name="T">The type of response to deserialize.</typeparam>
        /// <param name="request">The <see cref="RestRequest"/> to send.</param>
        /// <param name="cancellation"></param>
        /// <returns>Returns the object <see cref="T"/></returns>
        /// <exception cref="AtlasException">The API exception</exception>
        protected async Task<T> PostAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequestAsync<T>(request, Method.Post, cancellation);
        }

        /// <summary>
        /// Execute a Get Request Async
        /// </summary>
        /// <param name="request">The <see cref="RestRequest"/> to send.</param>
        /// <param name="cancellation"></param>
        /// <exception cref="AtlasException">The API exception</exception>
        protected async Task PostAsync(RestRequest request, CancellationToken cancellation = default)
        {
            await SendRequestAsync(request, Method.Post, cancellation);
        }

        /// <summary>
        /// Execute a Put Request Async
        /// </summary>
        /// <typeparam name="T">The type of response to deserialize.</typeparam>
        /// <param name="request">The <see cref="RestRequest"/> to send.</param>
        /// <param name="cancellation"></param>
        /// <returns>Returns the object <see cref="T"/></returns>
        /// <exception cref="AtlasException">The API exception</exception>
        protected async Task<T> PutAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequestAsync<T>(request, Method.Put, cancellation);
        }

        /// <summary>
        /// Execute a Put Request Async
        /// </summary>
        /// <param name="request">The <see cref="RestRequest"/> to send.</param>
        /// <param name="cancellation"></param>
        /// <exception cref="AtlasException">The API exception</exception>
        protected async Task PutAsync(RestRequest request, CancellationToken cancellation = default)
        {
            await SendRequestAsync(request, Method.Put, cancellation);
        }

        /// <summary>
        /// Execute a Delete Request Async
        /// </summary>
        /// <typeparam name="T">The type of response to deserialize.</typeparam>
        /// <param name="request">The <see cref="RestRequest"/> to send.</param>
        /// <param name="cancellation"></param>
        /// <returns>Returns the object <see cref="T"/></returns>
        /// <exception cref="AtlasException">The API exception</exception>
        protected async Task<T> DeleteAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequestAsync<T>(request, Method.Delete, cancellation);
        }

        /// <summary>
        /// Execute a Delete Request Async
        /// </summary>
        /// <param name="request">The <see cref="RestRequest"/> to send.</param>
        /// <param name="cancellation"></param>
        /// <exception cref="AtlasException">The API exception</exception>
        protected async Task DeleteAsync(RestRequest request, CancellationToken cancellation = default)
        {
            await SendRequestAsync(request, Method.Delete, cancellation);
        }

        /// <summary>
        /// Execute a Request Async
        /// </summary>
        /// <param name="request">The <see cref="RestRequest"/> to send.</param>
        /// <param name="method">The <see cref="Method"/> to use.</param>
        /// <param name="cancellation"></param>
        /// <exception cref="AtlasException">The API exception</exception>
        private async Task SendRequestAsync(RestRequest request, Method method, CancellationToken cancellation = default)
        {
            EnrichRequestHeaders(request);

            var response = await _http.ExecuteAsync(request, method, cancellation);

            SetToken("");

            ElaborateResponse(response);
        }

        /// <summary>
        /// Execute a Request Async
        /// </summary>
        /// <typeparam name="T">The type of response to deserialize.</typeparam>
        /// <param name="request">The <see cref="RestRequest"/> to send.</param>
        /// <param name="method">The <see cref="Method"/> to use.</param>
        /// <param name="cancellation"></param>
        /// <returns>Returns the object <see cref="T"/></returns>
        /// <exception cref="AtlasException">The API exception</exception>
        private async Task<T> SendRequestAsync<T>(RestRequest request, Method method, CancellationToken cancellation = default)
        {
            EnrichRequestHeaders(request);

            var response = await _http.ExecuteAsync<T>(request, method, cancellation);

            SetToken("");

            return ElaborateResponse(response);
        }

        /// <summary>
        /// Elaborate the Response
        /// </summary>
        /// <typeparam name="T">The type of response to deserialize.</typeparam>
        /// <param name="response">The <see cref="RestResponse{T}"/>.</param>
        /// <returns>Returns the object <see cref="T"/></returns>
        /// <exception cref="AtlasException">The API exception</exception>
        private T ElaborateResponse<T>(RestResponse<T> response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ComposeAndRaiseException(response);
            }

            return response.Data;
        }

        /// <summary>
        /// Elaborate the Response
        /// </summary>
        /// <param name="response">The <see cref="RestResponse"/>.</param>
        /// <exception cref="AtlasException">The API exception</exception>
        private void ElaborateResponse(RestResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ComposeAndRaiseException(response);
            }
        }

        /// <summary>
        /// Compose and Raise the Atlas API Exception
        /// </summary>
        private void ComposeAndRaiseException(RestResponse response)
        {
            var atlasException = string.IsNullOrEmpty(response.Content) ? JObject.Parse("{}") : JObject.Parse(response.Content);
            var message = atlasException?.SelectToken("message")?.ToString();

            if (message == null)
            {
                message = response.StatusCode switch
                {
                    System.Net.HttpStatusCode.NotFound => "Not found",
                    System.Net.HttpStatusCode.Unauthorized => "Unauthorized",
                    System.Net.HttpStatusCode.Forbidden => "Not enough privileges to access the requested resource",
                    System.Net.HttpStatusCode.UnprocessableEntity => "Validation errors",
                    System.Net.HttpStatusCode.BadRequest => "Bad request",
                    System.Net.HttpStatusCode.TooManyRequests => "Too many requests",
                    System.Net.HttpStatusCode.InternalServerError => "Internal server error",
                    System.Net.HttpStatusCode.BadGateway => "Bad gateway",
                    System.Net.HttpStatusCode.ServiceUnavailable => "Service unavailable",
                    System.Net.HttpStatusCode.GatewayTimeout => "Gateway timeout",
                    _ => "Generic Error"
                };
            }

            var exception = new AtlasException((int)response.StatusCode, message) {
                Errors = atlasException?.SelectToken("errors")
            };

            throw exception;
        }

        /// <summary>
        /// Set the Token to use as Authorization for the Request
        /// </summary>
        /// <param name="token">The token string</param>
        internal void SetToken(string token)
        { 
            _token = token;
        }

    }
}

