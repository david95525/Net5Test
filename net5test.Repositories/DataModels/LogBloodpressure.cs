using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class LogBloodpressure
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BpmId { get; set; }
        public string UserId { get; set; }
        public string Note { get; set; }
        public string Photo { get; set; }
        public string Recording { get; set; }
        public string RecordingTime { get; set; }
        public int Sys { get; set; }
        public int Dia { get; set; }
        public int Pul { get; set; }
        public byte Afib { get; set; }
        public byte Pad { get; set; }
        public byte Mode { get; set; }
        public string MacAddress { get; set; }
        public long UpdateDate { get; set; }
        public long ModifyDate { get; set; }
        public byte UpdateDateH { get; set; }
        public byte Cuffokr { get; set; }
    }
}
