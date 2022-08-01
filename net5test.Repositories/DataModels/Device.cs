using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class Device
    {
        public int Id { get; set; }
        public byte DeviceType { get; set; }
        public string DeviceModel { get; set; }
        public string MacAddress { get; set; }
        public int ErrorCode1 { get; set; }
        public int ErrorCode2 { get; set; }
        public int ErrorCode3 { get; set; }
        public int ErrorCode4 { get; set; }
        public byte IsDel { get; set; }
        public long AddTime { get; set; }
        public long LastTime { get; set; }
        public long ModifyDate { get; set; }
    }
}
