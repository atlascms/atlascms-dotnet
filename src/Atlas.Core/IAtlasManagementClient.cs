using Atlas.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public interface IAtlasManagementClient
    {
        /// <summary>
        /// Get all settings of the project
        /// </summary>
        /// <param name="cancellation">The optional cancellation token to cancel the operation.</param>
        /// <returns>The <see cref="Settings"/> object.</returns>
        /// <exception cref="AtlasException">The API Exception returned.</exception>
        Task<Settings> GetSettings(CancellationToken cancellation = default);

    }
}
