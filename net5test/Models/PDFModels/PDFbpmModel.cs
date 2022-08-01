using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace net5test.Models.PDFModels
{
        public class PDFBpmModel
        {
            /// <summary>
            /// 開始時間
            /// 格式:2020-08-01
            /// </summary>
            [Required(ErrorMessage = "1001")]
            [RegularExpression(@"^\d{4}(-\d{2}){1,2}", ErrorMessage = "1010")]
            public string start_date { get; set; }

            /// <summary>
            /// 結束時間
            /// 格式:2020-08-01
            /// </summary>
            [Required(ErrorMessage = "1001")]
            [RegularExpression(@"^\d{4}(-\d{2}){1,2}", ErrorMessage = "1010")]
            public string end_date { get; set; }

            /// <summary>
            /// 收縮壓閥值
            /// </summary>
            [Required(ErrorMessage = "1001")]
            [RegularExpression(@"^\d+$", ErrorMessage = "1009")]
            public int sys_threshold { get; set; }

            /// <summary>
            /// 舒張壓閥值
            /// </summary>
            [Required(ErrorMessage = "1001")]
            [RegularExpression(@"^\d+$", ErrorMessage = "1009")]
            public int dia_threshold { get; set; }

            /// <summary>
            /// 語系
            /// 格式:en、tw
            /// </summary>
            public string lang { get; set; }

            /// <summary>
            /// 模式
            /// 0:一般
            /// 1:監控
            /// 2:壓力測試
            /// </summary>
            public int mode { get; set; }

            /// <summary>
            /// 日期格式
            /// 0:YYYY/MM/DD
            /// 1:DD.MM.YYYY
            /// 2:MM/DD/YYYY
            /// </summary>
            public int date_format { get; set; }
            /// <summary>
            /// PDF加密
            /// 0:無
            /// 1:加密
            /// </summary>
            public int pdf_encryption { get; set; }
        }
}
