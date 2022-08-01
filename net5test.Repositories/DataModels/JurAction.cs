using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class JurAction
    {
        public byte JurId { get; set; }
        public byte ParentId { get; set; }
        public string ActionName { get; set; }
        public string ActionCode { get; set; }
        public string GroupFunction { get; set; }
        public byte SortOrder { get; set; }
        public string Link { get; set; }
        public bool Disable { get; set; }
    }
}
