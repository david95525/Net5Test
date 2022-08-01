using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class LogOxymeter
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int OxymeterId { get; set; }
        public string MacAddress { get; set; }
        public string Note { get; set; }
        public string Photo { get; set; }
        public string Recording { get; set; }
        public string RecordingTime { get; set; }
        public float Spo2 { get; set; }
        public float PulseRate { get; set; }
        public float PerfusionIndex { get; set; }
        public int? MeasurementLength { get; set; }
        public string Spo2RawData { get; set; }
        public string PulseRateRawData { get; set; }
        public string PerfusionIndexRawData { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte UpdateDateH { get; set; }
        public string Spo2WaveformData { get; set; }
    }
}
