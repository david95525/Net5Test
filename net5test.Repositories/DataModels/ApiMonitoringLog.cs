using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class ApiMonitoringLog
    {
        public int Id { get; set; }
        public byte ApiName { get; set; }
        public string StatusCode { get; set; }
        public long RespTime { get; set; }
        public long CallTime { get; set; }
        public long CreateTime { get; set; }
    }
}
