using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class PushList
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string DeviceType { get; set; }
        public string MemberEmail { get; set; }
        public byte PushType { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string LinkUrl { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public byte IsForever { get; set; }
        public byte IsPush { get; set; }
        public byte IsDel { get; set; }
        public long AddTime { get; set; }
    }
}
