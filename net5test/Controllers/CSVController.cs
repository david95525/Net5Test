using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using net5test.Models.BloodPressureModel;
using net5test.Repositories.DataModels;
using net5test.Services.BloodPressureService;
using net5test.Services.DomainModels;
using net5test.Services.MemberService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Controllers
{
    public class CSVController : Controller
    {
        private readonly IBloodPressureService _bloodPressureService;
        private readonly IMemberService _memberService;
        public  CSVController(IBloodPressureService bloodPressureService,IMemberService memberService)
            {
            _bloodPressureService = bloodPressureService;
            _memberService = memberService;
            }
        //todo CSV檔
        [HttpPost]
        public  IActionResult ExportCSV(int Category)
        {
            string email = User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;
            string filename = $"{email}.csv";
            if (Category == 0)
            {
             
                var bplist = _bloodPressureService.GetBloodPressureByEmail(email);
                StringBuilder sb = new StringBuilder();
                // 取得類別的欄位名稱
                var headerList = typeof(BloodPressureDM).GetProperties().Select(m => m.Name).ToList();
                // 添加所有欄位
                sb.AppendLine(String.Join(',', headerList));
                
                foreach(BloodPressureDM item in bplist)
                {
                    sb.AppendLine($"{item.email},{item.id}");
                }
               
                byte[] buffer = Encoding.UTF8.GetBytes(sb.ToString());
                return File(buffer, "text/csv", filename);
            }
            else
            {
                return null;
            }
            
        }
 

       
    }
}

