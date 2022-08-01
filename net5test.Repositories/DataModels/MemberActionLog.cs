using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class MemberActionLog
    {
        public int LogId { get; set; }
        public int MemberId { get; set; }
        public byte DeviceType { get; set; }
        public int LogAction { get; set; }
        public string IpAddress { get; set; }
        public long LogTime { get; set; }
        public int LogTimeH { get; set; }
    }
}
