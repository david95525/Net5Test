using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class OauthAdmin
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CompanyName { get; set; }
        public string BusinessPhone { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public string Purpose { get; set; }
        public long AddTime { get; set; }
    }
}
