using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models
{
    public class Settings
    {
        public string ProjectName { get; set; }
        public UserSettings UserSettings { get; set; } = new UserSettings();
        public List<LocaleSettings> Locales { get; set; } = new List<LocaleSettings>();
    }

    public class UserSettings
    {
        public bool UsersEnabled { get; set; }
    }

    public class LocaleSettings
    {
        public string Locale { get; set; }
        public bool IsDefault { get; set; }
    }
}
