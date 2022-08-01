using Microsoft.AspNetCore.Mvc;
using net5test.Models.Auth;
using net5test.Models.PDFModels;
using net5test.Services.AuthorizeService;
using net5test.Services.BloodPressureService;
using net5test.Services.MemberService;
using Newtonsoft.Json;
using System;
using net5test.Services.DomainModels;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using net5test.Models.Base;
using net5test.Common;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace net5test.Controllers
{
    public class PDFController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment; 
        private readonly IBloodPressureService _bloodPressureService;
        private readonly IMemberService _memberService;
        private readonly IAuthorizeService _authorizeService;
        public PDFController(IBloodPressureService bloodPressureService, IMemberService memberService,IAuthorizeService authorizeService,
             IWebHostEnvironment webHostEnvironment)
        {
            _bloodPressureService = bloodPressureService;
            _memberService = memberService;
            _authorizeService = authorizeService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public IActionResult ExportBPPDF()
        {
            try
            {
                //token驗證
                TokenResponseModel TK = new TokenResponseModel();
                var readCookie = HttpContext.Request.Cookies["TK"];
                string token = "";
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
                //用email尋找資料
                string email = User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;
                //血壓
            
                    var bplist = _bloodPressureService.GetBloodPressureByEmail(email);
                    if(bplist.Count==0)
                    {
                        return Ok(new ApiResult()
                        {
                            code = (int)Models.Base.Status.FetchDataError,
                            info = Models.Base.Status.FetchDataError.ToDescription(),
                        });
                    }
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath ,"Member", email, "1", "Pdf");

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var fileDate = bplist.FirstOrDefault().update_date.FromUnixTimestampTW();

                string sys = "sys";
                string dia = "dia";
                string pulse = "pulse";
                string afib = "afib";
                string numberOfReadings = "numberOfReadings";
                Task.Run(() =>
                {
                    #region 平均數據
                    string[] sysKey = new string[6];
                    string[] diaKey = new string[6];
                    string[] pulseKey = new string[6];
                    string[] afibKey = new string[6];
                    string[] readingsKey = new string[6];

                    List<string[]> listKey = new List<string[]>();
                    var day = new BpmAverage();
                    var morningAvg = new BpmAverage();
                    var eveningAvg = new BpmAverage();
                    if (bplist.Count > 0)
                    {
                        day.readings = bplist.Count;
                        day.sys = bplist.Sum(x => x.sys) / day.readings;
                        day.dia = bplist.Sum(x => x.dia) / day.readings;
                        day.pulse = bplist.Sum(x => x.pul) / day.readings;
                        day.afib = bplist.Sum(x => x.afib);

                        sysKey[0] = "sys";
                        sysKey[1] = day.sys.ToString() + " mmHg";
                        diaKey[0] = "dia";
                        diaKey[1] = day.dia.ToString() + " mmHg";
                        pulseKey[0] = "pulse";
                        pulseKey[1] = day.pulse.ToString() + " BPM";
                        afibKey[0] = "afib";
                        afibKey[1] = day.afib.ToString();
                        readingsKey[0] = "numberOfReadings";
                        readingsKey[1] = day.readings.ToString();
                    }
                    else
                    {
                        sysKey[0] = "sys";
                        sysKey[1] = "0 mmHg";
                        diaKey[0] = "dia";
                        diaKey[1] = "0 mmHg";
                        pulseKey[0] = "pulse";
                        pulseKey[1] = "0 BPM";
                        afibKey[0] = "afib";
                        afibKey[1] = "0";
                        readingsKey[0] = "numberOfReadings";
                        readingsKey[1] = "0";
                    }

                    var tmpMorning = bplist.Where(x => IsInDate(x.update_date.FromUnixTimestampTW(), "moming")).ToList();

                    if (tmpMorning.Count > 0)
                    {
                        morningAvg.readings = tmpMorning.Count();
                        morningAvg.sys = tmpMorning.Sum(x => x.sys) / morningAvg.readings;
                        morningAvg.dia = tmpMorning.Sum(x => x.dia) / morningAvg.readings;
                        morningAvg.pulse = tmpMorning.Sum(x => x.pul) / morningAvg.readings;
                        morningAvg.afib = tmpMorning.Sum(x => x.afib);

                        sysKey[2] = sys;
                        sysKey[3] = morningAvg.sys.ToString() + " mmHg";
                        diaKey[2] = dia;
                        diaKey[3] = morningAvg.dia.ToString() + " mmHg";
                        pulseKey[2] = pulse;
                        pulseKey[3] = morningAvg.pulse.ToString() + " BPM";
                        afibKey[2] = afib;
                        afibKey[3] = morningAvg.afib.ToString();
                        readingsKey[2] = numberOfReadings;
                        readingsKey[3] = morningAvg.readings.ToString();
                    }
                    else
                    {
                        sysKey[2] = sys;
                        sysKey[3] = "0 mmHg";
                        diaKey[2] = dia;
                        diaKey[3] = "0 mmHg";
                        pulseKey[2] = pulse;
                        pulseKey[3] = "0 BPM";
                        afibKey[2] = afib;
                        afibKey[3] = "0";
                        readingsKey[2] = numberOfReadings;
                        readingsKey[3] = "0";
                    }

                    var tmpEvening = bplist.Where(x => IsInDate(x.update_date.FromUnixTimestampTW(), "evening")).ToList();

                    if (tmpEvening.Count > 0)
                    {
                        eveningAvg.readings = tmpEvening.Count();
                        eveningAvg.sys = tmpEvening.Sum(x => x.sys) / eveningAvg.readings;
                        eveningAvg.dia = tmpEvening.Sum(x => x.dia) / eveningAvg.readings;
                        eveningAvg.pulse = tmpEvening.Sum(x => x.pul) / eveningAvg.readings;
                        eveningAvg.afib = tmpEvening.Sum(x => x.afib);

                        sysKey[4] = sys;
                        sysKey[5] = eveningAvg.sys.ToString() + " mmHg";
                        diaKey[4] = dia;
                        diaKey[5] = eveningAvg.dia.ToString() + " mmHg";
                        pulseKey[4] = pulse;
                        pulseKey[5] = eveningAvg.pulse.ToString() + " BPM";
                        afibKey[4] = afib;
                        afibKey[5] = eveningAvg.afib.ToString();
                        readingsKey[4] = numberOfReadings;
                        readingsKey[5] = eveningAvg.readings.ToString();
                    }
                    else
                    {
                        sysKey[4] = sys;
                        sysKey[5] = "0 mmHg";
                        diaKey[4] = dia;
                        diaKey[5] = "0 mmHg";
                        pulseKey[4] = pulse;
                        pulseKey[5] = "0 BPM";
                        afibKey[4] = afib;
                        afibKey[5] = "0";
                        readingsKey[4] = numberOfReadings;
                        readingsKey[5] = "0";
                    }

                    listKey.Add(sysKey);
                    listKey.Add(diaKey);
                    listKey.Add(pulseKey);
                    listKey.Add(afibKey);
                    listKey.Add(readingsKey);
                    #endregion

                    //using (Stream pdfStream = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    //{
                    //    using (Document pdfDoc = new Document(PageSize.A4, 40, 40, 109, 0))
                    //    {
                    //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, pdfStream);
                    //        pdfDoc.Open();

                    //        var summaryTitle = chFontBold_14;
                    //        var summarysTitle = chFontBold_11;
                    //        var summaryValue = chFont_11;
                    //        var summaryValueWarning = chFont_11_Red;

                    //        #region 資訊欄
                    //        var infoModel = new PDFInfoBpmModel()
                    //        {
                    //            NameTitle = nameTitle,
                    //            NameValue = name,
                    //            ReportDateTitle = reportDateTitle,
                    //            ReportDateValue = today.ToString(DateFormat(data.date_format)),
                    //            MorningTitle = morning,
                    //            MorningValue = "06:00 - 11:59",
                    //            SexTitle = sexTitle,
                    //            SexValue = sex,
                    //            ReadingsRangeTitle = readingsRangeTitle,
                    //            ReadingsRangeValue = $"{DateTime.Parse(data.start_date).ToString(DateFormat(data.date_format))} - {end.Value.ToString(DateFormat(data.date_format))}",
                    //            EveningTitle = evening,
                    //            EveningValue = "18:00 - 23:59",
                    //            DobTitle = dobTitle,
                    //            DobValue = dob,
                    //            TargetSysTitle = targetSYS,
                    //            TargetSysValue = data.sys_threshold.ToString() + " mmHg",
                    //            AgeTitle = ageTitle,
                    //            AgeValue = age,
                    //            TargetDiaTitle = targetDIA,
                    //            TargetDiaValue = data.dia_threshold.ToString() + " mmHg"
                    //        };
                    //        //todo 血壓資訊欄12小時制
                    //        if (ConfigurationManager.AppSettings["Region"] == "MLU")
                    //        {
                    //            infoModel.MorningValue = "06:00 - 11:59 AM";
                    //            infoModel.EveningValue = "06:00 - 11:59 PM";
                    //        }
                    //        var spanModel = new List<PDFSpanModel>()
                    //                {
                    //                    new PDFSpanModel{  Name = dob, TitleOrValue = "Value", ColSpan = true, Span = 3},
                    //                    new PDFSpanModel{  Name = age, TitleOrValue = "Value", ColSpan = true, Span = 3}
                    //                };

                    //        CreateTable(InfoTableSpacing(data.lang.ToUpper(), "bpm"), infoModel, new Font[] { chFontBold_9, chFont_9 }, pdfDoc, spanModel);
                    //        #endregion

                    //        #region 平均數據
                    //        PdfPTable summaryTable = new PdfPTable(new float[] { 1f, 1f, 1f, 1f, 1f, 1f });
                    //        summaryTable.TotalWidth = 550f;
                    //        summaryTable.LockedWidth = true;
                    //        summaryTable.SpacingAfter = 20f;

                    //        var averageDayTitleCell = new PdfPCell(new Phrase(avgDay, summaryTitle));
                    //        averageDayTitleCell.Colspan = 2;
                    //        averageDayTitleCell.Padding = 12;
                    //        averageDayTitleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //        averageDayTitleCell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    //        summaryTable.AddCell(averageDayTitleCell);

                    //        var averageMorningTitleCell = new PdfPCell(new Phrase(avgMorning, summaryTitle));
                    //        averageMorningTitleCell.Colspan = 2;
                    //        averageMorningTitleCell.Padding = 12;
                    //        averageMorningTitleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //        averageMorningTitleCell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    //        summaryTable.AddCell(averageMorningTitleCell);

                    //        var averageEveningTitleCell = new PdfPCell(new Phrase(avgEvening, summaryTitle));
                    //        averageEveningTitleCell.Colspan = 2;
                    //        averageEveningTitleCell.Padding = 12;
                    //        averageEveningTitleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //        averageEveningTitleCell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    //        summaryTable.AddCell(averageEveningTitleCell);

                    //        int afibIndex = 0;

                    //        foreach (var array in listKey)
                    //        {
                    //            afibIndex++;
                    //            for (int i = 0; i < array.Length; i += 2)
                    //            {
                    //                //todo afib隱藏
                    //                if (afibIndex != 4 || ConfigurationManager.AppSettings["Region"] == "MLE")
                    //                {
                    //                    var titlePhrase = new Phrase();
                    //                    titlePhrase.Add(new Chunk(array[i], summarysTitle));
                    //                    var titleCell = new PdfPCell(titlePhrase);
                    //                    titleCell.Border = Rectangle.LEFT_BORDER;
                    //                    titleCell.PaddingLeft = 10f;
                    //                    summaryTable.AddCell(titleCell);


                    //                    var valuePhrase = new Phrase();
                    //                    string unit = string.Empty;
                    //                    if (afibIndex == 4)
                    //                    {
                    //                        if (array[i + 1] == "1" || array[i + 1] == "3" || array[i + 1] == "5")
                    //                        {
                    //                            if (int.Parse(array[i + 1]) > 0)
                    //                            {
                    //                                valuePhrase.Add(new Chunk(array[i + 1], summaryValueWarning));
                    //                                valuePhrase.Add(new Chunk(" of " + readingsKey[i + 1], summaryValue));
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            valuePhrase.Add(new Chunk(array[i + 1], summaryValue));
                    //                            valuePhrase.Add(new Chunk(" of " + readingsKey[i + 1], summaryValue));
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        valuePhrase.Add(new Chunk(array[i + 1], summaryValue));
                    //                    }

                    //                    var valueCell = new PdfPCell(valuePhrase);
                    //                    valueCell.Border = Rectangle.RIGHT_BORDER;
                    //                    valueCell.PaddingLeft = 10f;
                    //                    summaryTable.AddCell(valueCell);
                    //                }
                    //            }

                    //            if (listKey.IndexOf(array) == listKey.Count - 1)
                    //            {
                    //                for (int i = 0; i < array.Length / 2; i++)
                    //                {
                    //                    var titlePhrase = new Phrase();
                    //                    titlePhrase.Add(new Chunk(" ", summaryTitle));
                    //                    var titleCell = new PdfPCell(titlePhrase);
                    //                    titleCell.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                    //                    titleCell.PaddingLeft = 10f;
                    //                    summaryTable.AddCell(titleCell);

                    //                    var valuePhrase = new Phrase();
                    //                    valuePhrase.Add(new Chunk(" ", summaryTitle));
                    //                    var valueCell = new PdfPCell(valuePhrase);
                    //                    valueCell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                    //                    valueCell.PaddingLeft = 10f;
                    //                    summaryTable.AddCell(valueCell);
                    //                }
                    //            }
                    //        }

                    //        pdfDoc.Add(summaryTable);
                    //        #endregion

                    //        #region 圖表
                    //        CreateChartTable(chartPath, new float[] { 1f }, chFontBold_12, pdfDoc);
                    //        #endregion

                    //        #region 數據
                    //        var readingsModel = new PDFBpmReadingsTitleModel()
                    //        {
                    //            Date = date,
                    //            Ttime = time,
                    //            Sys = sys,
                    //            Dia = dia,
                    //            Hr = hr,
                    //            Mode = mode,
                    //            Pulse = irregular,
                    //            Cuff = cuff,
                    //            Note = notes
                    //        };
                    //        var list = bloodpressure.Select(x => PrepareBpmReadingsModel(x, new Font[] { chFont_9, chFont_9_Red }, data, DateFormat(data.date_format), imgAFIB, imgPAD)).ToList();

                    //        CreateReadingsTable(readingsModel, new Font[] { chFontBold_12, chFontBold_9 }, page, readRows, list, pdfDoc, ReadingsTableSpacing(data.lang.ToUpper(), "bpm"), 6f);
                    //        #endregion
                });
             }
            catch (Exception ex)
            {

            }
          
                return View();
        }
                private bool IsInDate(DateTime dt_keyin, string mode)
        {
            if (mode == "moming")
            {
                var momingS = new DateTime(dt_keyin.Year, dt_keyin.Month, dt_keyin.Day, 06, 00, 00);
                var momingE = new DateTime(dt_keyin.Year, dt_keyin.Month, dt_keyin.Day, 11, 59, 59);
                return dt_keyin.CompareTo(momingS) >= 0 && dt_keyin.CompareTo(momingE) <= 0;
            }
            else
            {
                var eveningS = new DateTime(dt_keyin.Year, dt_keyin.Month, dt_keyin.Day, 18, 00, 00);
                var eveningE = new DateTime(dt_keyin.Year, dt_keyin.Month, dt_keyin.Day, 23, 59, 59);
                return dt_keyin.CompareTo(eveningS) >= 0 && dt_keyin.CompareTo(eveningE) <= 0;
            }
        }
    }
}
