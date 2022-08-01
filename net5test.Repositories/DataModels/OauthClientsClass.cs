using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class OauthClientsClass
    {
        public int Id { get; set; }
        public int OauthAid { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantTypes { get; set; }
        public string Scope { get; set; }
        public long AccessTokenTime { get; set; }
        public long RefreshTokenTime { get; set; }
        public byte IsDel { get; set; }
        public long AddTime { get; set; }
    }
}
