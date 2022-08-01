using net5test.Common;
using net5test.Repositories.DataModels;
using net5test.Repositories.Interface;
using net5test.Services.DomainModels;
using net5test.Services.MemberService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Services.AuthorizeService
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IGenericRepository<OauthClientsClass> _oauthClientsClassRepo;
        private readonly IGenericRepository<OauthClientsKind> _oauthClientsKindRepo;
        private readonly IGenericRepository<OauthRefreshToken> _oauthRefreshTokensRepo;
        private readonly IGenericRepository<OauthAuthorizationCode> _oauthAuthorizationCodesRepo;
        private readonly IGenericRepository<OauthAccessToken> _oauthAccessTokensRepo;
        private readonly IMemberService _memberService;

        public AuthorizeService(IGenericRepository<OauthClientsClass> oauthClientsClassRepo, IGenericRepository<OauthClientsKind> oauthClientsKindRepo,
       IGenericRepository<OauthRefreshToken> oauthRefreshTokensRepo, IGenericRepository<OauthAuthorizationCode> oauthAuthorizationCodesRepo,
       IGenericRepository<OauthAccessToken> oauthAccessTokensRepo, IMemberService memberService)
        {
            this._oauthClientsClassRepo = oauthClientsClassRepo;
            this._oauthClientsKindRepo = oauthClientsKindRepo;
            this._oauthRefreshTokensRepo = oauthRefreshTokensRepo;
            this._oauthAuthorizationCodesRepo = oauthAuthorizationCodesRepo;
            this._oauthAccessTokensRepo = oauthAccessTokensRepo;
            this._memberService = memberService;
        }
        public bool OauthClientClassIsExistByClientId(string clientId)
        {
            return _oauthClientsClassRepo.GetAll().Any(x => x.ClientId.Equals(clientId));
        }
        public OauthClientsClass GetOauthClientClassByClientId(string clientId)
        {
            return _oauthClientsClassRepo.GetAll().FirstOrDefault(x => x.ClientId.Equals(clientId));
        }

        public bool CheckOauthClientClassByClientIdAndClientSecret(string clientId, string clientSecret)
        {
            return _oauthClientsClassRepo.GetAll().Any(x => x.ClientId.Equals(clientId) && x.ClientSecret.Equals(clientSecret));
        }
        public List<OauthClientsKind> GetOauthClientKinds(int oauthClientsClassId = 0, string redirect_uri = null)
        {
            var query = _oauthClientsKindRepo.GetAll();
            if (oauthClientsClassId != 0)
                query = query.Where(x => x.OauthCcid == oauthClientsClassId);
            if (!string.IsNullOrWhiteSpace(redirect_uri))
                query = query.Where(x => x.RedirectUri == redirect_uri);
            return query.ToList();
        }
        public OauthRefreshToken GetOauthRefreshToken(string refresh_token, string clientId)
        {
            return _oauthRefreshTokensRepo.GetAll().FirstOrDefault(x => x.RefreshToken.Equals(refresh_token) && x.ClientId.Equals(clientId));
        }

        public OauthAuthorizationCode GetOauthAuthorizationCode(string authCode, string clientId)
        {
            return _oauthAuthorizationCodesRepo.GetAll().FirstOrDefault(x => x.AuthorizationCode.Equals(authCode) && x.ClientId.Equals(clientId));
        }
        public OauthAccessToken GetByAccessToken(string accessToken)
        {
            return _oauthAccessTokensRepo.Get(x => x.AccessToken == accessToken);
        }

        public string CreateAuthorizationCode(string clientId, int memberId, string redirect_uri, string scope)
        {
            OauthAuthorizationCode authcode = new OauthAuthorizationCode();
            authcode.AuthorizationCode = Guid.NewGuid().ToString("N");
            authcode.ClientId = clientId;
            authcode.MemberId = memberId;
            authcode.RedirectUri = redirect_uri;
            authcode.Scope = scope;
            authcode.AddTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            authcode.Expires = 600;

            _oauthAuthorizationCodesRepo.Create(authcode);

            return authcode.AuthorizationCode;
        }

        public (string, string, int) CreateToken(OauthClientsClass oauthClientsClass, int memberId, string redirect_uri)
        {
            long dtTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            OauthAccessToken accessToken = new OauthAccessToken();
            accessToken.AccessToken = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
            accessToken.ClientId = oauthClientsClass.ClientId;
            accessToken.MemberId = memberId;
            accessToken.Scope = oauthClientsClass.Scope;
            accessToken.AddTime = dtTimestamp;
            accessToken.Expires = oauthClientsClass.AccessTokenTime;

            OauthRefreshToken refreshToken = new OauthRefreshToken();
            refreshToken.RefreshToken = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
            refreshToken.ClientId = oauthClientsClass.ClientId;
            refreshToken.MemberId = memberId;
            refreshToken.RedirectUri = redirect_uri;
            refreshToken.Scope = oauthClientsClass.Scope;
            refreshToken.AddTime = dtTimestamp;
            refreshToken.Expires = oauthClientsClass.RefreshTokenTime;


            _oauthAccessTokensRepo.Create(accessToken);
            _oauthRefreshTokensRepo.Create(refreshToken);

            return (accessToken.AccessToken, refreshToken.RefreshToken, Convert.ToInt32(oauthClientsClass.AccessTokenTime));
        }

        public void DeleteRefreshToken(OauthRefreshToken refreshToken)
        {
            _oauthRefreshTokensRepo.Remove(refreshToken);
        }

        public void DeleteAuthorizationCode(OauthAuthorizationCode authorizationCode)
        {
            _oauthAuthorizationCodesRepo.Remove(authorizationCode);
        }

        public (bool result, string message) VaildOauthparameterInput(string client_id, string response_type, string redirect_uri)
        {
            if (string.IsNullOrWhiteSpace(response_type) && response_type != "code")
                return (false, "invalid_request");
            if (string.IsNullOrWhiteSpace(client_id))
                return (false, "invalid_request");
            if (string.IsNullOrWhiteSpace(redirect_uri))
                return (false, "invalid_request");
            var oauthClientClass = GetOauthClientClassByClientId(client_id);
            if (oauthClientClass == null)
                return (false, "unauthorized_client");
            var oauthClientKind = GetOauthClientKinds(oauthClientClass.Id, redirect_uri).FirstOrDefault();
            if (oauthClientKind == null)
                return (false, "invalid_client");
            return (true, null);
        }

        public VerifyModel ClientVerify(string token, string scop, string client_id = null, string client_secret = null)
        {
            ResultModel Result = new ResultModel();

            //驗證scop、client_id、client_secret
            int statusCode = CheckClient(token, scop, client_id, client_secret);

            if (statusCode != (int)Status.Success)
            {
                Result = new ResultModel()
                {
                    code = statusCode,
                    info = ((Status)statusCode).ToDescription() + "(access_token)"
                };
                return new VerifyModel()
                {
                    Result = Result,
                    IsValid = false
                };
            }

            var accessToken = GetByAccessToken(token);

            int memberCheckstatuscode = _memberService.CheckMemberAccount(accessToken.MemberId);

            if (memberCheckstatuscode != (int)Status.Success)
            {
                Result = new ResultModel()
                {
                    code = memberCheckstatuscode,
                    info = ((Status)memberCheckstatuscode).ToDescription()
                };
                return new VerifyModel()
                {
                    Result = Result,
                    IsValid = false
                };
            }

            return new VerifyModel()
            {
                AccessToken = accessToken,
                IsValid = true
            };
        }

        #region method
        private int CheckClient(string accessTokenStr, string apiScope = "", string client_id = null, string client_secret = null)
        {
            if (client_id != null && client_secret != null)
            {
                OauthClientsClass oauthClientsClass = _oauthClientsClassRepo.Get(x => x.ClientId == client_id);

                if (oauthClientsClass != null)
                {
                    //'狀態 0:啟用 1:停用',
                    if (oauthClientsClass.IsDel == 1)
                        return 5202;

                    if (oauthClientsClass.ClientSecret != client_secret)
                        return 5005;
                }
                else
                {
                    return 5004;
                }
            }
            OauthAccessToken access_Token =_oauthAccessTokensRepo.Get(x => x.AccessToken == accessTokenStr);

            if (!string.IsNullOrWhiteSpace(apiScope))
            {
                var scopes = access_Token.Scope.Split(',');
                if (!scopes.Where(x => x == apiScope).Any())
                    return 5102;
            }

            return 10000;
        }
        #endregion
    }
}