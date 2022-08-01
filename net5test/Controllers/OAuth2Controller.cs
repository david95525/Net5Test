using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using net5test.Models.Auth;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Threading;

using log4net;
using log4net.Repository.Hierarchy;
using System.Text;
using System.Collections.Generic;
using net5test.Services.AuthorizeService;
using System.Linq;

namespace net5test.Controllers
{
    public class OAuth2Controller : Controller
    {
        static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IAuthorizeService _authorizeService;
        public OAuth2Controller( IAuthorizeService _authorizeService)
        {
            this._authorizeService = _authorizeService;
        }
        [HttpGet]
        public ActionResult RequestAuthorization(LoginDataModel model)
        {
            model.client_id = "499268d0db1f4214aeef7f59e7fe4649";
            model.response_type ="code";
            model.redirect_uri = Url.Action("Test1", "APPtest");
            model.state = "0";
            //model.lang = "tw";
            model.region = "250";
            //if (!string.IsNullOrWhiteSpace(model.lang))
            //{
            //    Thread.CurrentThread.CurrentCulture = new CultureInfo(model.lang);
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(model.lang);
            //    setcookies("lang", model.lang);
            //}

            if (model != null)
                setcookies("info", WebUtility.UrlEncode(JsonConvert.SerializeObject(model)));
            return RedirectToAction("login", "account");
        }

        public ActionResult Token()
        {
            TokenModel model = JsonConvert.DeserializeObject<TokenModel>((string)TempData["model"]);
            int memberId = 0, expire_in = 0;
            string clientId = string.Empty, clientSecret = string.Empty, refreshtoken = string.Empty, accesstoken = string.Empty;

            try
            {
               string authHeader = Request.Headers["Authorization"];
                logger.Info($"Authorization:{authHeader} tokenmodel:{JsonConvert.SerializeObject(model)}");

                if ((authHeader == null || !authHeader.StartsWith("Basic", StringComparison.InvariantCultureIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(model.client_id) || string.IsNullOrWhiteSpace(model.client_secret)))
                {
                    return BadRequest("invalid_client");
                }
                if (authHeader != null && authHeader.StartsWith("Basic", StringComparison.InvariantCultureIgnoreCase))
                {
                    string encodedClientIdSecret = authHeader.Substring("Basic ".Length).Trim();
                    string clientIdSecret = Encoding.UTF8.GetString(Convert.FromBase64String(encodedClientIdSecret));
                    int seperatorIndex = clientIdSecret.IndexOf(':');
                    clientId = clientIdSecret.Substring(0, seperatorIndex);
                    clientSecret = clientIdSecret.Substring(seperatorIndex + 1);
                }
                else
                {
                    clientId = model.client_id;
                    clientSecret = model.client_secret;
                }
                if (!_authorizeService.CheckOauthClientClassByClientIdAndClientSecret(clientId, clientSecret))
                {
                    return BadRequest("invalid_client");
                }
                var oauthClientClass = _authorizeService.GetOauthClientClassByClientId(clientId);
                List<string> grant_typeList = oauthClientClass.GrantTypes.Split(',').ToList();
                if (!grant_typeList.Any(x => x == model.grant_type))
                {
                    return BadRequest("unsupported_grant_type");
                }
                var oauthClientKinds = _authorizeService.GetOauthClientKinds(oauthClientClass.Id, model.redirect_uri);
                var oauthClientKind = oauthClientKinds.Where(x => x.RedirectUri == model.redirect_uri).FirstOrDefault();
                if (oauthClientKind == null)
                {
                    //Redirect_uri error
                    return BadRequest("invalid_grant");
                }
                switch (model.grant_type)
                {
                    case "authorization_code":
                        if (string.IsNullOrWhiteSpace(model.code))
                        {
                            return BadRequest("invalid_request");
                        }
                        var authorizationCode = _authorizeService.GetOauthAuthorizationCode(model.code, clientId);
                        if (authorizationCode == null)
                        {
                            return BadRequest("invalid_grant");
                        }
                        if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > (authorizationCode.AddTime + authorizationCode.Expires))
                        {
                            return BadRequest("invalid_grant");
                        }
                        memberId = authorizationCode.MemberId;
                        (accesstoken, refreshtoken, expire_in) = _authorizeService.CreateToken(oauthClientClass, memberId, oauthClientKind.RedirectUri);
                        _authorizeService.DeleteAuthorizationCode(authorizationCode);
                        break;
                    case "refresh_token":
                        if (string.IsNullOrWhiteSpace(model.refresh_token))
                        {
                            return BadRequest("invalid_request");
                        }
                        var refreshToken = _authorizeService.GetOauthRefreshToken(model.refresh_token, clientId);
                        if (refreshToken == null)
                        {
                            return BadRequest("invalid_grant");
                        }
                        if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > (refreshToken.AddTime + refreshToken.Expires))
                        {
                            return BadRequest("invalid_grant");
                        }
                        memberId = refreshToken.MemberId;
                        (accesstoken, refreshtoken, expire_in) = _authorizeService.CreateToken(oauthClientClass, memberId, oauthClientKind.RedirectUri);
                        _authorizeService.DeleteRefreshToken(refreshToken);
                        break;
                    default:
                        return BadRequest("unsupported_grant_type");
                }
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                return BadRequest("server_error");
            }
            TokenResponseModel tokenrspmodel = new TokenResponseModel()
            {
                access_token = accesstoken,
                refresh_token = refreshtoken,
                token_type = "Bearer",
                //scope = string.Empty,
                expires_in = expire_in
            };
            logger.Info(JsonConvert.SerializeObject(tokenrspmodel));
            setcookies("TK", WebUtility.UrlEncode(JsonConvert.SerializeObject(tokenrspmodel)));
            return RedirectToAction("index", "home");
        }
        private void setcookies(string key, string value)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions() 
            { 
                HttpOnly = true ,
                Secure=true,
                Expires=DateTime.Now.AddHours(2)
            });
        }
    }
}
