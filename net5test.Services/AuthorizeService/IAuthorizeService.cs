using net5test.Repositories.DataModels;
using net5test.Services.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Services.AuthorizeService
{
    public interface IAuthorizeService
    {
        bool OauthClientClassIsExistByClientId(string clientId);

        OauthClientsClass GetOauthClientClassByClientId(string clientId);

        bool CheckOauthClientClassByClientIdAndClientSecret(string clientId, string clientSecret);

        List<OauthClientsKind> GetOauthClientKinds(int oauthClientsClassId = 0, string redirect_uri = null);

        OauthRefreshToken GetOauthRefreshToken(string refresh_token, string clientId);

       OauthAuthorizationCode GetOauthAuthorizationCode(string authCode, string clientId);

        string CreateAuthorizationCode(string clientId, int memberId, string redirect_uri, string scope);

        (string, string, int) CreateToken(OauthClientsClass oauthClientsClass, int memberId, string redirect_uri);

        void DeleteRefreshToken(OauthRefreshToken refreshToken);

        void DeleteAuthorizationCode(OauthAuthorizationCode authorizationCode);

        (bool result, string message) VaildOauthparameterInput(string client_id, string response_type, string redirect_uri);
        //獲取資料前驗證授權
        VerifyModel ClientVerify(string token, string scop, string client_id = null, string client_secret = null);
        OauthAccessToken GetByAccessToken(string accessToken);
    }
}
