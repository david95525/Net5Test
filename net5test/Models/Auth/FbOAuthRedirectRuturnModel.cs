using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5test.Models.Auth
{
    public class FbOAuthRedirectRuturnModel
    {
        public string code { get; set; }
        public int? error_code { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
        public string error_reason { get; set; }
        public string state { get; set; }
    }
}
