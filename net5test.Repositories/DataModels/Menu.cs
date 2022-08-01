using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class Menu
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public byte Parentid { get; set; }
        public bool? Status { get; set; }
        public byte Order { get; set; }
        public string Css { get; set; }
        public string Url { get; set; }
    }
}
