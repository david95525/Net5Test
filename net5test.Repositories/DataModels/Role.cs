using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class Role
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
