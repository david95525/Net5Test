using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class MagLog
    {
        public int LogId { get; set; }
        public int AdminId { get; set; }
        public string AdminEmail { get; set; }
        public string LogInfo { get; set; }
        public string IpAddress { get; set; }
        public long LogTime { get; set; }
    }
}
