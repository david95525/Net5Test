using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net5test.Models.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5test.Controllers
{
    public class APPtestController : Controller
    {
        private readonly string Client_id = "499268d0db1f4214aeef7f59e7fe4649";
        private readonly string Client_secret = "9e0e028a8284428da2eb5cb208c542a8";
        private readonly string Grant_type = "authorization_code";
        public IActionResult Test1()
        {
           string code= HttpContext.Request.Query["code"];
            ViewBag.code = code;
            TokenModel model = new TokenModel {
                redirect_uri = Url.Action("Test1", "APPtest"),
                client_id= Client_id,
                client_secret=Client_secret,
                grant_type=Grant_type,
                code=code
            };
            TempData["model"] = JsonConvert.SerializeObject (model);
            return RedirectToAction("Token","OAuth2");
        }
        //public IActionResult Test1()
        //{
        //    string code = HttpContext.Request.Query["code"];
        //    ViewBag.code = code;
        //    return View();
        //}

    }
}
