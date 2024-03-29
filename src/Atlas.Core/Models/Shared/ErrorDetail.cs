﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Shared
{
    public class ErrorDetail
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; } = null;

        [JsonExtensionData]
        public IDictionary<string, object> Data { get; set; } = new Dictionary<string, object>(StringComparer.Ordinal);
    }
}
