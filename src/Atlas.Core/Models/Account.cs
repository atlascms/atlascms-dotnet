using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class AccountBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
    }

    public class Account : AccountBase
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime ModifiedAt { get; private set; }
        public string ModifiedBy { get; private set; }
    }

    public class RegisterAccount : AccountBase
    {
        public string Password { get; set; }
    }
}
