using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5test.Models.Auth
{
    public class ThridPartyLoginModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        //for apple sign in
        public string IdToken { get; set; }
        public string AccessToken { get; set; }
        public int ThridpartyType { get; set; }
    }
    public enum ThridpartyType
    {
        Google = 1,
        Facebook = 2,
        Apple = 3
    }

}
