using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class DeviceErrorLog
    {
        public int Id { get; set; }
        public string MacAddress { get; set; }
        public byte DeviceType { get; set; }
        public int MemberId { get; set; }
        public int LogAction { get; set; }
        public long LogTime { get; set; }
    }
}
