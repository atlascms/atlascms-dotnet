using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public static class ClientExtensions
    {
        /// <summary>
        /// Set the token to use as Authorization on the next API call. 
        /// </summary>
        /// <param name="token">The token to use.</param>
        /// <returns>The instance of the client.</returns>
        public static T UseToken<T>(this T client, string token) where T : ISecuredClient
        {
            var baseClient = client as ClientBase;

            baseClient.SetToken(token);

            return client;
        }
    }
}
