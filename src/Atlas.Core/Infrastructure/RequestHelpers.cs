using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Infrastructure
{
    public class RequestHelpers
    {
        public static RestRequest CreateProjectRequest(string project)
        { 
            var request = new RestRequest($"api/{project}");

            return request;
        }
    }
}
