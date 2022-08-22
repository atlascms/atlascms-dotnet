using RichardSzalay.MockHttp;
using System.Reflection;

namespace Atlas.Core.Tests
{
    public static class Extensions
    {
        public static MockedRequest AddStandardHeaders(this MockedRequest mockedRequest, string token) 
        {
            return mockedRequest.WithHeaders("Authorization", $"Bearer {token}")
                    .WithHeaders("Content-Type", $"application/json")
                    .WithHeaders("X-Atlas-SDK", $"atlas-dotnet/{typeof(ClientBase).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}; OS Windows;")
                    // .WithHeaders("User-Agent", "Atlas CMS .NET Client SDK")
                    ;
        }
    }
}
