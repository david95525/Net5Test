using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class DeviceConnLog
    {
        public int Id { get; set; }
        public byte DeviceType { get; set; }
        public string MacAddress { get; set; }
        public int MemberId { get; set; }
        public long LogTime { get; set; }
    }
}
