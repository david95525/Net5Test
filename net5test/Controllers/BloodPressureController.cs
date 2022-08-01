using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net5test.Common;
using net5test.Models.Auth;
using net5test.Models.BloodPressureModel;
using net5test.Repositories.DataModels;
using net5test.Services.AuthorizeService;
using net5test.Services.BloodPressureService;
using net5test.Services.DomainModels;
using net5test.Services.MemberService;
using Newtonsoft.Json;
using System;

using System.Linq;
using System.Net;
using System.Security.Claims;


namespace net5test.Controllers
{
    public class BloodPressureController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IBloodPressureService _bloodPressureService;
        private readonly IAuthorizeService _authorizeService;
        public BloodPressureController(IMemberService memberservice,IBloodPressureService bloodPressureService,IAuthorizeService authorizeService)
        {
            _memberService = memberservice;
            _bloodPressureService = bloodPressureService;
            _authorizeService = authorizeService;
        }
  
      [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Vue()
        {
            return View();
        }
  
        public IActionResult ReactIndex()
        {
            return View();
        }
        public IActionResult CallAPI()
        {
            return View();
        }
        /// <summary>
        /// 獲取血壓量測資料
        /// </summary>
        [HttpPost]
        public IActionResult GetBpmData(int? draw, int start, int length, DateTime? startdate, DateTime? enddate)
        {
            length = 10;
            string email = User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;
            try
            {
                //token驗證
                TokenResponseModel TK = new TokenResponseModel();
                var readCookie = HttpContext.Request.Cookies["TK"];
                string token=string.Empty;
                if (readCookie != null)
                {
                    TK = JsonConvert.DeserializeObject<TokenResponseModel>(WebUtility.UrlDecode(readCookie));
                    token = TK.access_token;
                }
                var accessToken = _authorizeService.ClientVerify(token, "bpm_search");
                if (!accessToken.IsValid)
                {
                    if (accessToken.Result.code == 5102)
                    {
                        return StatusCode((int)HttpStatusCode.Forbidden, accessToken.Result);
                    }

                    return Ok(accessToken.Result);
                }
                var bplist= _bloodPressureService.GetBloodPressureList(pageIndex: (start / length),
                pageSize: length,
                email: email,
                startDate: startdate,
                endDate: enddate,
                time_type: 1);
                return Json(new
                {
                    draw = draw,
                    recordsTotal = bplist.TotalCount,
                    recordsFiltered = bplist.TotalCount,
                    data = bplist.Select(PrepareBloodPressureModel) 
                });
            }
            catch(Exception ex)
            {
                return Json(new { 
                message=ex.Message
                });
            }
        }
        /// <summary>
        /// 增加血壓資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddBpmData(BloodPressureAddModel data)
        {
            string email = User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;
            try
            {
                Member member = _memberService.GetByEmail(email);
                LogBloodpressure bloodpressure = new LogBloodpressure
                {
                    BpmId = 2,
                    Sys=data.sys,
                    Dia=data.dia,
                    Pul=data.pul,
                    Mode= (byte)data.mode,
                    Pad= (byte)data.pad,
                    Afib= (byte)data.afib,
                    MacAddress=data.mac_address,
                    UpdateDate=data.datetime.ToUnixTimestampTW(),
                    ModifyDate=data.datetime.ToUnixTimestampTW()
                };
              

                bloodpressure.MemberId= member.Id;
                bloodpressure.UpdateDateH = byte.Parse(data.datetime.Hour.ToString());
                foreach (var frontData in bloodpressure.GetType().GetProperties())
                {
                    if (frontData.GetValue(bloodpressure, null) == null)
                    {
                        switch (frontData.PropertyType.Name)
                        {
                            case "String":
                                frontData.SetValue(bloodpressure, "");
                                break;
                            case "Nullable`1":
                                frontData.SetValue(bloodpressure, 0);
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (_bloodPressureService.CreateLogBloodPressure(bloodpressure) <= 0)
                {
                    return null;                  
                } else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 轉換為PrepareBloodPressureModel
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [NonAction]
        private BloodPressureModel PrepareBloodPressureModel(BloodPressureDM arg)
        {
            return new BloodPressureModel
            {
                email = arg.email,
                name = arg.name,
                log_bloodpress_id = arg.id,
                member_id = arg.member_id,
                bpm_id = arg.bpm_id,
                user_id = arg.user_id,
                note = arg.note,
                photo = arg.photo,
                recording = arg.recording,
                recording_time = arg.recording_time,
                sys = arg.sys,
                dia = arg.dia,
                pul = arg.pul,
                afib = arg.afib,
                pad = arg.pad,
                mode = arg.mode,
                mac_address = arg.mac_address,
                update_date = arg.update_date,
                update_date_H = arg.update_date_H,
                cuffokr = arg.cuffokr,
            };
        }
    }
}
