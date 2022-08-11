using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core.Exceptions
{
    public class AtlasException : Exception
    {
        /// <summary>
        /// The Http Status Code of the Error
        /// </summary>
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// Error Details
        /// </summary>
        public dynamic Errors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasException"/> class.
        /// </summary>
        /// <param name="httpStatusCode">The http status code of the exception.</param>
        /// <param name="message">The message of the exception.</param>
        public AtlasException(int httpStatusCode, string message): base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
