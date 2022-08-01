using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class PortableClient
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Os { get; set; }
        public string Company { get; set; }
        public string MachineType { get; set; }
        public string Model { get; set; }
        public string ClientUniqueId { get; set; }
        public string PushUniqueId { get; set; }
        public string ApiKey { get; set; }
        public string Gps { get; set; }
        public long AddTime { get; set; }
        public long LastTime { get; set; }
    }
}
