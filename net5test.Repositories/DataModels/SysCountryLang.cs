using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class SysCountryLang
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Lang { get; set; }
        public string Name { get; set; }
    }
}
