using System;
using System.Collections.Generic;

#nullable disable

namespace net5test.Repositories.DataModels
{
    public partial class Member
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string MemberName { get; set; }
        public string Photo { get; set; }
        public byte Gender { get; set; }
        public long Birthday { get; set; }
        public string Conditions { get; set; }
        public byte RegisterType { get; set; }
        public string RegThrData { get; set; }
        public string RegCode { get; set; }
        public byte Threshold { get; set; }
        public double CuffSize { get; set; }
        public byte UnitType { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public float Bmi { get; set; }
        public byte BmiActivity { get; set; }
        public int BpMeasurementArm { get; set; }
        public double Sys { get; set; }
        public byte SysUnit { get; set; }
        public byte SysActivity { get; set; }
        public double Dia { get; set; }
        public byte DiaActivity { get; set; }
        public double GoalWeight { get; set; }
        public byte WeightActivity { get; set; }
        public double BodyFat { get; set; }
        public byte BodyFatActivity { get; set; }
        public int Resistance { get; set; }
        public int MeasuringTime { get; set; }
        public byte MeasuringTimeActivity { get; set; }
        public int Oxygen { get; set; }
        public byte OxygenActivity { get; set; }
        public byte IsDel { get; set; }
        public byte IsReg { get; set; }
        public byte DateFormat { get; set; }
        public long AddTime { get; set; }
        public long LastTime { get; set; }
        public string ResetpwdCode { get; set; }
        public DateTime? ResetpwdDate { get; set; }
    }
}
