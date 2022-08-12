using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public abstract class UserBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public List<string> Roles { get; set; }
        public bool IsActive { get; set; }
    }

    public class User : UserBase {

        public string Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime ModifiedAt { get; private set; }
        public string ModifiedBy { get; private set; }

        public Dictionary<string, object> Attributes { get; set; }
    }

    public class User<T> : User where T : class
    {
        public new T Attributes { get; set; }
    }

    public class RegisterUser : UserBase
    {
        public Dictionary<string, object> Attributes { get; set; }
    }

    public class RegisterUser<T> : RegisterUser where T : class
    {
        public new T Attributes { get; set; }

    }

}
