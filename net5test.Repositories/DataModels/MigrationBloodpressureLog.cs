using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class MigrationBloodpressureLog
    {
        public int Id { get; set; }
        public string ImportId { get; set; }
        public int MemberId { get; set; }
        public byte Status { get; set; }
        public int? Transfercount { get; set; }
        public DateTime Createdate { get; set; }
    }
}
