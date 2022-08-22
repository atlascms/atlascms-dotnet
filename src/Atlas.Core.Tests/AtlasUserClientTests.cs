using RestSharp;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Atlas.Core.Tests
{
    public class AtlasUserClientTests
    {
        private const string _apikey = "favarava";

        public AtlasUserClientTests()
        {
            
        }

        [Fact]
        public async Task ChangePassword_Should_Call_CorrectEndpoint()
        {
            MockHttpMessageHandler httpMessageHandlerMock = new MockHttpMessageHandler();
            httpMessageHandlerMock.When(HttpMethod.Post, "http://localhost/api/users/*")
                                  .WithContent("{\"password\":\"fava\"}")
                                  .AddStandardHeaders(_apikey);

            var atlasUserClient = _getRestClient(httpMessageHandlerMock);

            Func<Task> res = async () => await atlasUserClient.ChangePassword("1", "fava", CancellationToken.None);

            await res();

            httpMessageHandlerMock.VerifyNoOutstandingExpectation();
        }

        private AtlasUserClient _getRestClient(MockHttpMessageHandler httpMessageHandlerMock) 
        {
            var client = new RestClient(new RestClientOptions()
            {
                BaseUrl = new Uri("http://localhost"),
                ConfigureMessageHandler = _ => httpMessageHandlerMock
            });

            return new AtlasUserClient(client, new Configuration.AtlasOptions()
            {
                BaseUrl = "http://localhost",
                ApiKey = _apikey,
                SerializerOptions = new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.None }
            });
        }
    }
}
