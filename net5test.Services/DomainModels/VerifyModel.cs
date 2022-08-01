using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net5test.Repositories.DataModels;

namespace net5test.Services.DomainModels
{
    public class VerifyModel
    {
        public ResultModel Result { get; set; }
        public OauthAccessToken AccessToken { get; set; }

        public bool IsValid { get; set; }
    }
}
