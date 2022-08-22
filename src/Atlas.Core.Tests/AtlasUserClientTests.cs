using Atlas.Core.Exceptions;
using Atlas.Core.Models;
using Atlas.Core.Models.Shared;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RichardSzalay.MockHttp;
using Xunit;

namespace Atlas.Core.Tests
{
    public class AtlasUserClientTests
    {
        private const string _apikey = "favarava";
        private const string _baseUrl = "http://localhost";
        private JsonSerializerSettings _defaultJSONSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.None,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public AtlasUserClientTests()
        {

        }

        [Fact]
        public async Task ChangePassword_Should_Call_CorrectEndpoint()
        {
            string password = "fava";

            MockHttpMessageHandler httpMessageHandlerMock = new MockHttpMessageHandler();

            var request = httpMessageHandlerMock.When(HttpMethod.Post, $"{_baseUrl}/api/users/*")
                                              .WithContent("{\"password\":\"" + password + "\"}")
                                              .AddStandardHeaders(_apikey)
                                              .Respond("application/json", "");

            var atlasUserClient = _getRestClient(httpMessageHandlerMock);

            Func<Task> res = async () => await atlasUserClient.ChangePassword("1", password, CancellationToken.None);

            await res();

            httpMessageHandlerMock.VerifyNoOutstandingExpectation();
            httpMessageHandlerMock.GetMatchCount(request).Should().Be(1);
        }

        [Fact]
        public async Task ChangePassword_Should_RaiseNotFoundException()
        {
            string password = "fava";

            MockHttpMessageHandler httpMessageHandlerMock = new MockHttpMessageHandler();

            var request = httpMessageHandlerMock.When(HttpMethod.Post, $"{_baseUrl}/api/usrs/*")
                                              .WithContent("{\"password\":\"" + password + "\"}")
                                              .AddStandardHeaders(_apikey)
                                              .Respond("application/json", "");

            var atlasUserClient = _getRestClient(httpMessageHandlerMock);

            Func<Task> res = async () => await atlasUserClient.ChangePassword("1", password, CancellationToken.None);

            await res.Should().ThrowAsync<AtlasException>().WithMessage("Not found");
        }

        [Fact]
        public async Task CreateRole_Should_Call_CorrectEndpoint()
        {
            MockHttpMessageHandler httpMessageHandlerMock = new MockHttpMessageHandler();

            Role role = new Role()
            {
                Name = "Role test",
                Permissions = new List<string>()
            };

            var request = httpMessageHandlerMock.When(HttpMethod.Post, $"{_baseUrl}/api/roles")
                                              .WithContent(JsonConvert.SerializeObject(role, _defaultJSONSerializerSettings))
                                              .AddStandardHeaders(_apikey)
                                              .Respond("application/json", JsonConvert.SerializeObject(new KeyResult<string>() { Result = "Ok" }));

            var atlasUserClient = _getRestClient(httpMessageHandlerMock);

            Func<Task> res = async () => await atlasUserClient.CreateRole(role);

            await res();

            httpMessageHandlerMock.VerifyNoOutstandingExpectation();
            httpMessageHandlerMock.GetMatchCount(request).Should().Be(1);
        }

        private AtlasUserClient _getRestClient(MockHttpMessageHandler httpMessageHandlerMock)
        {
            var client = new RestClient(new RestClientOptions()
            {
                BaseUrl = new Uri(_baseUrl),
                ConfigureMessageHandler = _ => httpMessageHandlerMock
            });

            return new AtlasUserClient(client, new Configuration.AtlasOptions()
            {
                BaseUrl = _baseUrl,
                ApiKey = _apikey,
                SerializerOptions = _defaultJSONSerializerSettings
            });
        }
    }
}
