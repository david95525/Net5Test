using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class LogBodytemperatureList
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int EventCode { get; set; }
        public string MacAddress { get; set; }
        public int BtId { get; set; }
        public string Note { get; set; }
        public string Photo { get; set; }
        public string Recording { get; set; }
        public string RecordingTime { get; set; }
        public float BodyTemp { get; set; }
        public float RoomTemp { get; set; }
        public long UpdateDate { get; set; }
        public long ModifyDate { get; set; }
        public int UpdateDateH { get; set; }
    }
}
