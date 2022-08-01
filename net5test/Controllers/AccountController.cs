using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net5test.Services.MemberService;
using net5test.Services.AuthorizeService;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using net5test.Repositories.DataModels;
using net5test.Models.Auth;
using Newtonsoft.Json;
using System.Net;
using System.IdentityModel.Tokens.Jwt;

namespace net5test.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly IMemberService _memberService;
        private readonly IAuthorizeService _authorizeService;
        public AccountController(IMemberService service, IAuthorizeService authorizeService)
        {
            _memberService = service;
            _authorizeService = authorizeService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        { 
            return View();
        }
        public async Task< IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }

        [HttpPost]
   
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
         
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(",", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                TempData["message"] = errorMessage;
                return View();
            }
            if (!_memberService.AccountExist(model.Email))
            {
                TempData["message"] = "不存在";
                return View();
            }
            if (!_memberService.AccountIsActive(model.Email))
            {
                TempData["message"] = "未驗證";
                return View();
            }
            bool vaildResult = _memberService.VaildAccount(model.Email, model.Password);
            if (!vaildResult)
            {
                TempData["message"] = "密碼錯誤";
                return View();
            }
        
         
            Member user= _memberService.GetByEmail(model.Email);
            if (user.IsReg == 6)
            {
                TempData["message"] = "密碼錯誤";
                return View();
            }
            _memberService.UpdateAccountLastTime(user);
            await  SetClaims(user);

            LoginDataModel info = new LoginDataModel();
            var readCookie = HttpContext.Request.Cookies["info"];

            if (readCookie != null)
            {
                info = JsonConvert.DeserializeObject<LoginDataModel>( WebUtility.UrlDecode(readCookie));
                var oauthClientClass = _authorizeService.GetOauthClientClassByClientId(info.client_id);
                var oauthClientKind = _authorizeService.GetOauthClientKinds(oauthClientClass.Id, info.redirect_uri).FirstOrDefault();
                //if (!info.redirect_uri.Contains("://"))
                //    info.redirect_uri += "://";
                
                string authCode = _authorizeService.CreateAuthorizationCode(oauthClientClass.ClientId, user.Id, oauthClientKind.RedirectUri, oauthClientClass.Scope);
                string urlparamterquery = $"?code={authCode}";
                if (!string.IsNullOrWhiteSpace(info.state))
                {
                    urlparamterquery += "&state=" + info.state;
                }
                return Redirect($"{info.redirect_uri}{urlparamterquery}");
            }
            //加上 Url.IsLocalUrl 防止Open Redirect漏洞
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);//導到原始要求網址
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(",", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                TempData["message"] = errorMessage;
                return RedirectToAction("Login", new { returnUrl = returnUrl });
            }
            if (_memberService.AccountExist(model.Email_Reg))
            {
                TempData["message"] = "帳號存在";
                return RedirectToAction("Login", new { returnUrl = returnUrl });
            }
            int regionId = Convert.ToInt32(_memberService.GetAllRegionIdByRegCode("UK").Value);
            string[] birthdayArray = model.Birthday.Split('.');
            DateTime birthday = new DateTime(int.Parse(birthdayArray[1]), int.Parse(birthdayArray[0]), 1);
            bool registerResult = _memberService.CreateNewAccount(register_type: 0, email: model.Email_Reg, pwd: model.Password_Reg, name: model.Name,birthday: birthday, region: regionId);

            if (!registerResult)
            {
                TempData["message"] = "註冊錯誤";
                return RedirectToAction("Login", new { returnUrl = returnUrl });
            }
            string regCode = _memberService.GetAccountRegCodeByEmail(model.Email_Reg);
            //Task.Run(() => _mailService.SendVerifyEmail(email: model.Email_Reg, regCode: regCode));

            TempData["message"] = "註冊成功";
            return RedirectToAction("Login", new { returnUrl = returnUrl });
        }
        //第三方
        [HttpPost]
        public async Task<ActionResult> ThirdLogin(ThridPartyLoginModel model)
        {
            LoginDataModel info = new LoginDataModel();
            var readCookie = HttpContext.Request.Cookies["info"];
            if (readCookie != null)
                info = JsonConvert.DeserializeObject<LoginDataModel>(WebUtility.UrlDecode(readCookie));
            string url = "home/index";
            string resultmsg = string.Empty;
            string identityStr = string.Empty;
            model.Name = string.IsNullOrWhiteSpace(model.Name) ? "Name" : model.Name;
            switch (model.ThridpartyType)
            {
                case (int)ThridpartyType.Apple:
                    identityStr = "apple_";
                    var jwtToken = new JwtSecurityToken(model.IdToken);
                    var plyload = jwtToken.Payload;
                    model.Email = jwtToken.Claims.First(claim => claim.Type == "email").Value;
                    model.Id = jwtToken.Claims.First(claim => claim.Type == "sub").Value;
                    break;
                case (int)ThridpartyType.Facebook:
                    identityStr = "fb_";
                    break;
                case (int)ThridpartyType.Google:
                    identityStr = "google_";
                    break;
            }

            Member member = _memberService.GetByEmail(model.Email);
            //apple 用id判斷是否為同一帳號 apple 有隱私帳號模式
            if (member == null && model.ThridpartyType == (int)ThridpartyType.Apple)
                member = _memberService.GetAccountByReg_thr_data(identityStr + model.Id);
            if (member == null)
            {
                int regionId = Convert.ToInt32(_memberService.GetAllRegionIdByRegCode("UK").Value);
                if (readCookie != null)
                {
                    info = JsonConvert.DeserializeObject<LoginDataModel>(WebUtility.UrlDecode(readCookie));
                    var region = _memberService.GetAllRegionIdByRegCode(info.region);
                    if (region.HasValue)
                    {
                        regionId = Convert.ToInt32(region.Value);
                    }
                }
                if (_memberService.CreateNewAccount(register_type: RegisterTypeEnum.ThirdpartyRegistration, name: model.Name, email: model.Email, region: regionId, idtoken: identityStr + model.Id, birthday: DateTime.UtcNow))
                {
                    member = _memberService.GetByEmail(model.Email);
                }
            }

            _memberService.UpdateAccountLastTime(member);
             await SetClaims(member);
            if (readCookie != null)
            {
                info = JsonConvert.DeserializeObject<LoginDataModel>(WebUtility.UrlDecode(readCookie));

                var oauthClientClass = _authorizeService.GetOauthClientClassByClientId(info.client_id);
                var oauthClientKind = _authorizeService.GetOauthClientKinds(oauthClientClass.Id, info.redirect_uri).FirstOrDefault();
                //if (!info.redirect_uri.Contains("://"))
                //    info.redirect_uri += "://";

                string authCode = _authorizeService.CreateAuthorizationCode(oauthClientClass.ClientId, member.Id ,oauthClientKind.RedirectUri, oauthClientClass.Scope);
                string urlparamterquery = $"?code={authCode}";
                if (!string.IsNullOrWhiteSpace(info.state))
                {
                    urlparamterquery += "&state=" + info.state;
                }
                url = $"{info.redirect_uri}{urlparamterquery}";
            }
            return Json(new { result = true, message = resultmsg, redirecturl = url });
        }
        #region methods

        private async Task SetClaims(Member user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "member"),
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//Scheme必填
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(principal,
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
