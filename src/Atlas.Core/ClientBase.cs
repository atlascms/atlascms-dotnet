using Atlas.Core.Configuration;
using Atlas.Core.Exceptions;
using Atlas.Core.Extensions;
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

        protected async Task<T> PutAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequest<T>(request, Method.Put, cancellation);
        }

        protected async Task<T> DeleteAsync<T>(RestRequest request, CancellationToken cancellation = default)
        {
            return await SendRequest<T>(request, Method.Delete, cancellation);
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

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                throw new ForbiddenException();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new BadRequestException();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                throw new ValidationException();
            }
            else
            {
                throw new Exception();
            }
        }

        protected void SetToken(string token)
        { 
            _token = token;
        }
    }
}

