using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class PushActivity
    {
        public int Id { get; set; }
        public string Pushname { get; set; }
        public byte Pushtype { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ToEmail { get; set; }
        public int? ToRegion { get; set; }
        public int? ToDevicetype { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime Dontsendbeforedate { get; set; }
        public DateTime Updatedate { get; set; }
        public DateTime? Senddate { get; set; }
        public bool IsDelete { get; set; }
        public int? Successcount { get; set; }
        public int? Failcount { get; set; }
    }
}
