using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class LogBodytemperature
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int EventCode { get; set; }
        public string Event { get; set; }
        public string Type { get; set; }
        public long EventTime { get; set; }
        public long ModifyDate { get; set; }
    }
}
