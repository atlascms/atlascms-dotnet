﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Models.Queries
{
    public class AssetsQuery : ListQuery
    {
        public string Folder { get; set; }

        public string Filters { get; set; }
    }
}
