using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class AuthToken
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_at")]
        public long Expires { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = "Bearer";

        [JsonProperty("auth_type")]
        public AuthTokenType AuthType { get; set; }
    }

    public enum AuthTokenType
    { 
        User,
        Account
    }
}
