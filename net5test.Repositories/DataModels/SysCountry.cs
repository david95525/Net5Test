using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class SysCountry
    {
        public long Id { get; set; }
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public short Numcode { get; set; }
        public string ChtCountryname { get; set; }
        public string Countryname { get; set; }
        public string PrintableName { get; set; }
        public int Sortorder { get; set; }
    }
}
