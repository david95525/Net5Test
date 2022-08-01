using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class MenuRole
    {
        public int Id { get; set; }
        public byte MenuId { get; set; }
        public byte RoleId { get; set; }
    }
}
