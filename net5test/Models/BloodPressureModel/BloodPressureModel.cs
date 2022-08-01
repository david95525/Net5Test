using net5test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5test.Models.BloodPressureModel
{
    public class BloodPressureModel
    {
        public string email { get; set; }
        public string name { get; set; }
        public int log_bloodpress_id { get; set; }
        public int member_id { get; set; }
        public int bpm_id { get; set; }
        public string user_id { get; set; }
        public string note { get; set; }
        public string photo { get; set; }
        public string recording { get; set; }
        public string recording_time { get; set; }
        public int sys { get; set; }
        public int dia { get; set; }
        public int pul { get; set; }
        public sbyte afib { get; set; }
        public sbyte pad { get; set; }
        public sbyte mode { get; set; }
        public string mac_address { get; set; }
        public long update_date { get; set; }
        public sbyte update_date_H { get; set; }
        public Nullable<sbyte> cuffokr { get; set; }

        public DateTime updatedate
        {
            get { return update_date.FromUnixTimestamp(); }
            //set { this.update_date = value.ToUnixTimestamp(); }
        }
    }
    public class BloodPressureAddModel
    {
        /// <summary>
        /// 血壓機ID
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// 收縮壓
        /// </summary>

        public int sys { get; set; }

        /// <summary>
        /// 舒張壓
        /// </summary>

        public int dia { get; set; }

        /// <summary>
        /// 心律
        /// </summary>

        public int pul { get; set; }

        /// <summary>
        /// 血壓編號
        /// </summary>

        public int bpm_id { get; set; }

        /// <summary>
        /// AFIB偵測
        /// 0:沒有量測到
        /// 1:有量測到
        /// </summary>

        public sbyte afib { get; set; }

        /// <summary>
        /// PAD偵測
        /// 0:沒有量測到
        /// 1:有量測到
        /// </summary>

        public sbyte pad { get; set; }

        /// <summary>
        /// 模式
        /// 0: Normal Mode
        /// 1: Normal Mode + AFIB Screening
        /// 2: MAM Mode
        /// 3: MAM Mode + AFIB Screening
        /// </summary>
  
        public sbyte mode { get; set; }

        /// <summary>
        /// 臂帶鬆緊
        /// 0:未綁緊
        /// 1:綁緊
        /// 2:未檢測
        /// 預設值為2
        /// </summary>

        public sbyte cuffokr { get; set; }

        /// <summary>
        /// 日期
        /// 格式:2016-08-03 15:20:30
        /// </summary>
        public DateTime datetime{ get; set; }


        /// <summary>
        /// 產品序號
        /// 格式:BC:6A:29:27:2F:F1
        /// </summary>
        public string mac_address { get; set; }
    }
}
