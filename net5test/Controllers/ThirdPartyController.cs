using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI;
using net5test.Models.Auth;
using net5test.Repositories.DataModels;
using net5test.Services.AuthorizeService;
using net5test.Services.MemberService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace net5test.Controllers
{
    public class ThirdPartyController : Controller
    {
        private readonly string fb_client_id = "1371511800036900";
        private readonly string fb_client_secret = "4beacac06a6468d4876957b036b033e4";
        //private readonly string serviceurl = "https://localhost:5001";
        private readonly string serviceurl = "https://fhrtest.azurewebsites.net";
        private readonly IMemberService _memberService;
        private readonly IAuthorizeService _authorizeService;
        public ThirdPartyController(IMemberService memberService, IAuthorizeService authorizeService)
        {
            _memberService = memberService;
            _authorizeService = authorizeService;
        }

      
        public ActionResult FbLogin()
        {
            string state = Guid.NewGuid().ToString("N");
            HttpContext.Session.SetString("state", state);
            string fbUrl = $"https://www.facebook.com/v14.0/dialog/oauth?client_id={fb_client_id}&redirect_uri={serviceurl}/ThirdParty/fbrun&state={state}&scope=email";
            return Redirect(fbUrl);
        }
        public ActionResult FbRun(FbOAuthRedirectRuturnModel model)
        {
            string url = "/";

            if (model == null)
            {
                return Redirect(url);
            }
           string state= HttpContext.Session.GetString("state");
            if (state!= null)
            {
                if (model.state != state)
                {
                    return Content("Cross - site request forgery validation failed. Required param \"state\" is error.");
                }
            }
            else
            {
                return Content("Cross - site request forgery validation failed. Required param \"state\" missing from persistent data.");
            }
            if (model.error_code != null)
            {
               
                return Redirect(url);
            }

            (bool result, string id, string email, string name) = fbGraphAPI(fbGetAccesstoken(model.code));
            if (result)
                return (fbMemberLoginAsync(id, email, name));

            return RedirectToAction("index", "home");
        }
        private string fbGetAccesstoken(string code)
        {
            string accesstoken = string.Empty;
            string uri = $"https://graph.facebook.com/v14.0/oauth/access_token?client_id={fb_client_id}&redirect_uri={serviceurl}/ThirdParty/fbrun&client_secret={fb_client_secret}&code={code}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage response = client.GetAsync(uri).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var jobj = JsonConvert.DeserializeObject<JObject>(responseBody);
                    accesstoken = jobj["access_token"].ToString();
                }
                catch (HttpRequestException ex)
                {
                }
            }
            return accesstoken;
        }

        private (bool result, string id, string email, string name) fbGraphAPI(string accesstoken)
        {
            bool result = false;
            string email = string.Empty;
            string id = string.Empty;
            string name = string.Empty;
            string uri = $"https://graph.facebook.com/v14.0/me?fields=id,name,email&access_token={accesstoken}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage response = client.GetAsync(uri).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var jobj = JsonConvert.DeserializeObject<JObject>(responseBody);
                    id = jobj["id"].ToString();
                    name = jobj["name"].ToString();
                    email = jobj["email"]?.ToString();
                    result = true;
                }
                catch (HttpRequestException ex)
                {
                }
            }
            return (result, id, email, name);
        }

        private  ActionResult fbMemberLoginAsync(string id, string email, string name)
        {          
            var readCookie = HttpContext.Request.Cookies["info"];
            LoginDataModel info = new LoginDataModel();
            if (readCookie != null)
                info = JsonConvert.DeserializeObject<LoginDataModel>(WebUtility.UrlDecode(readCookie));
            if (string.IsNullOrWhiteSpace(email))
                email = $"fb_{id}@facebook.com";
            var member = _memberService.GetByEmail(email);
            if (member == null)
            {
                int regionId = Convert.ToInt32(_memberService.GetAllRegionIdByRegCode("UK").Value);
                if (readCookie != null)
                {
                    var region = _memberService.GetAllRegionIdByRegCode(info.region);
                    if (region.HasValue)
                    {
                        regionId = Convert.ToInt32(region.Value);
                    }
                }

                if (_memberService.CreateNewAccount(register_type: RegisterTypeEnum.ThirdpartyRegistration, name: name, email: email, region: regionId, idtoken: $"fb_{id}", birthday: DateTime.UtcNow))
                {
                    member = _memberService.GetByEmail(email);
                }
            }
            _memberService.UpdateAccountLastTime(member);
            SetClaims(member);
            if (readCookie != null)
            {
                var oauthClientClass = _authorizeService.GetOauthClientClassByClientId(info.client_id);
                var oauthClientKind = _authorizeService.GetOauthClientKinds(oauthClientClass.Id, info.redirect_uri).FirstOrDefault();
                //if (!info.redirect_uri.Contains("://"))
                //    info.redirect_uri += "://";

                string authCode = _authorizeService.CreateAuthorizationCode(oauthClientClass.ClientId, member.Id, oauthClientKind.RedirectUri, oauthClientClass.Scope);
                string urlparamterquery = $"?code={authCode}";
                if (!string.IsNullOrWhiteSpace(info.state))
                {
                    urlparamterquery += "&state=" + info.state;
                }
                TempData["fbsuccessredirecturl"] = $"{info.redirect_uri}{urlparamterquery}";
                
                return Redirect($"{info.redirect_uri}{urlparamterquery}");
            }
            return RedirectToAction("index", "home");
        }
        #region methods

        private  void SetClaims(Member user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "member"),
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//Scheme必填
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(principal,
                 new AuthenticationProperties()
                 {
                     IssuedUtc = DateTime.Now,
                     ExpiresUtc = DateTime.Now.AddHours(1),
                     IsPersistent = false
                     //IsPersistent = false：瀏覽器關閉立馬登出；IsPersistent = true 就變成常見的Remember Me功能                                    
                 });
        }
        #endregion
    }
}
