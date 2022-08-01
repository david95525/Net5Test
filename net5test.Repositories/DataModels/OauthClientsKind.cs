using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class OauthClientsKind
    {
        public int Id { get; set; }
        public int OauthAid { get; set; }
        public int OauthCcid { get; set; }
        public byte ClientType { get; set; }
        public string RedirectUri { get; set; }
        public string AndroidBundleId { get; set; }
        public string AndroidBundleFun { get; set; }
        public string AndroidKey { get; set; }
        public string IosBundleId { get; set; }
        public string IosUrlScheme { get; set; }
        public long AddTime { get; set; }
    }
}
