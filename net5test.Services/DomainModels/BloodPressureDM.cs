using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Services.DomainModels
{
   public class BloodPressureDM
    {
        public string email { get; set; }
        public string name { get; set; }
        public int id { get; set; }
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
        public long modify_date { get; set; }
        public Nullable<sbyte> cuffokr { get; set; }
    }
}
