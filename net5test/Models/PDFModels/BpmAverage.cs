using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5test.Models.PDFModels
{
    public class BpmAverage
    {
        public int sys { get; set; }
        public int dia { get; set; }
        public int pulse { get; set; }
        public int afib { get; set; }
        public int readings { get; set; }
    }
}
