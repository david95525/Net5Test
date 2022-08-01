using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5test.Models.PDFModels
{
    public class PDFBpmReadingsTitleModel
    {
        public string Date { get; set; }
        public string Ttime { get; set; }
        public string Sys { get; set; }
        public string Dia { get; set; }
        public string Hr { get; set; }
        public string Mode { get; set; }
        public string Pulse { get; set; }
        public string Cuff { get; set; }
        public string Note { get; set; }
    }
}
