using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public abstract class UserBase
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; internal set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; internal set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ModifiedAt { get; internal set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ModifiedBy { get; internal set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MobilePhone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Roles { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsActive { get; set; }
    }

    public class User : UserBase {
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string,object> Attributes { get; set; }

        public static implicit operator User<Dictionary<string, object>>(User source)
        {
            return new User<Dictionary<string, object>>
            {
                Attributes = source.Attributes,
                Email = source.Email,
                FirstName = source.FirstName,
                IsActive = source.IsActive,
                LastName = source.LastName,
                MobilePhone = source.MobilePhone,
                Roles = source.Roles,
                Username = source.Username,
                CreatedAt = source.CreatedAt,
                CreatedBy = source.CreatedBy,
                Id = source.Id,
                ModifiedAt = source.ModifiedAt,
                ModifiedBy = source.ModifiedBy,
            };
        }
    }

    public class User<T> : UserBase 
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public new T Attributes { get; set; }
    }

    public class RegisterUser : UserBase
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Attributes { get; set; }

        public static implicit operator RegisterUser<Dictionary<string, object>>(RegisterUser source)
        {
            return new RegisterUser<Dictionary<string, object>>
            {
                Attributes = source.Attributes,
                Email = source.Email,
                FirstName = source.FirstName,
                IsActive = source.IsActive,
                LastName = source.LastName,
                MobilePhone = source.MobilePhone,
                Roles = source.Roles,
                Username = source.Username,
                Password = source.Password
            };
        }
    }

    public class RegisterUser<T> : UserBase
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public new T Attributes { get; set; }
    }

}
