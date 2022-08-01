using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class DeleteHealthcareHistoryrecord
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public short DeviceType { get; set; }
        public int RecordId { get; set; }
        public long DeleteDate { get; set; }
        public string MacAddress { get; set; }
    }
}
