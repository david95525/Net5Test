using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5test.Models.Auth
{
    public class LoginDataModel
    {
        //開發者AppId
        public string client_id { get; set; }
        public string response_type { get; set; }
        public string redirect_uri { get; set; }
        public string state { get; set; }
        public string region { get; set; }
        public string lang { get; set; }

    }
}
