using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class AdminUser
    {
        public byte AdminId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActionList { get; set; }
        public long AddTime { get; set; }
        public long LastLogin { get; set; }
        public byte IsSuspended { get; set; }
    }
}
