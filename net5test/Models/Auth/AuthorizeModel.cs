using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5test.Models.Auth
{
    public class AuthorizeModel
    {
        /// <summary>
        /// required
        /// </summary>
        public string client_id { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string response_type { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string redirect_uri { get; set; }
        /// <summary>
        /// optional 
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// optional 
        /// </summary>
        public string scope { get; set; }
    }
}
