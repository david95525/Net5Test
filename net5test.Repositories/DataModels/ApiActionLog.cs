using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class ApiActionLog
    {
        public int ApilogId { get; set; }
        public int ApiId { get; set; }
        public long LogTime { get; set; }
    }
}
