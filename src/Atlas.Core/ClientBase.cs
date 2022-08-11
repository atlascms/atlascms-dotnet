using Atlas.Core.Configuration;
using Atlas.Core.Exceptions;
using Atlas.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public abstract class ClientBase
    {
        protected RestClient _http;
        protected AtlasOptions _options;

        private string _token = "";
        
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

        protected void EnrichRequestHeaders(RestRequest request)
        { 
            request.AddBearerToken(string.IsNullOrEmpty(_token) ? _options.ApiKey : _token);
        }

        protected async Task<T> GetAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequest<T>(request, Method.Get, cancellation);
        }

        protected async Task<T> PostAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequest<T>(request, Method.Post, cancellation);
        }

        protected async Task PostAsync(RestRequest request, CancellationToken cancellation = default)
        {
            await SendRequest(request, Method.Post, cancellation);
        }

        protected async Task<T> PutAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequest<T>(request, Method.Put, cancellation);
        }

        protected async Task PutAsync(RestRequest request, CancellationToken cancellation = default)
        {
            await SendRequest(request, Method.Put, cancellation);
        }

        protected async Task<T> DeleteAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequest<T>(request, Method.Delete, cancellation);
        }

        protected async Task DeleteAsync(RestRequest request, CancellationToken cancellation = default)
        {
            await SendRequest(request, Method.Delete, cancellation);
        }

        private async Task SendRequest(RestRequest request, Method method, CancellationToken cancellation = default)
        {
            EnrichRequestHeaders(request);

            var response = await _http.ExecuteAsync(request, method, cancellation);

            SetToken("");

            ElaborateResponse(response);
        }

        private async Task<T> SendRequest<T>(RestRequest request, Method method, CancellationToken cancellation = default)
        {
            EnrichRequestHeaders(request);

            var response = await _http.ExecuteAsync<T>(request, method, cancellation);

            SetToken("");

            return ElaborateResponse(response);
        }

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

        protected void SetToken(string token)
        { 
            _token = token;
        }
    }
}

