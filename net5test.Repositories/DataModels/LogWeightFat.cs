using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class LogWeightFat
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int WeightId { get; set; }
        public string MacAddress { get; set; }
        public string Note { get; set; }
        public string Photo { get; set; }
        public string Recording { get; set; }
        public string RecordingTime { get; set; }
        public float Weight { get; set; }
        public float Bmi { get; set; }
        public float BodyFat { get; set; }
        public float Water { get; set; }
        public float Skeleton { get; set; }
        public float Muscle { get; set; }
        public float Bmr { get; set; }
        public float OrganFat { get; set; }
        public long UpdateDate { get; set; }
        public long ModifyDate { get; set; }
        public int UpdateDateH { get; set; }
    }
}
