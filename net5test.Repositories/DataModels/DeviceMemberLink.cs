using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class DeviceMemberLink
    {
        public int Id { get; set; }
        public string MacAddress { get; set; }
        public byte DeviceType { get; set; }
        public int MemberId { get; set; }
        public byte IsDel { get; set; }
        public long AddTime { get; set; }
    }
}
