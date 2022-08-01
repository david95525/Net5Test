using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class OauthAccessToken
    {
        public string AccessToken { get; set; }
        public string ClientId { get; set; }
        public int MemberId { get; set; }
        public string Scope { get; set; }
        public long AddTime { get; set; }
        public long Expires { get; set; }
        public string Providerticket { get; set; }
    }
}
