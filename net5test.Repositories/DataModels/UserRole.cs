using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class UserRole
    {
        public byte Id { get; set; }
        public byte UserId { get; set; }
        public byte RoleId { get; set; }
    }
}
