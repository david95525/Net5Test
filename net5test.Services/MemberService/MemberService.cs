using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net5test.Repositories.DataModels;
using net5test.Repositories.Interface;
using net5test.Common.Cryptography;


namespace net5test.Services.MemberService
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepo;
        private readonly IGenericRepository<SysCountry> _sysCoutryRepo;
        private readonly ICryptography _cryptography;
        public MemberService(IGenericRepository<Member> memberRepo, ICryptography cryptography, IGenericRepository<SysCountry> sysCoutryRepo)
        {
           this._memberRepo = memberRepo;
            this._cryptography = cryptography;
            _sysCoutryRepo = sysCoutryRepo;
        }
        /// <summary>
        /// 
        ///用memberid獲取單一會員資料
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public Member GetById(int memberId)
        {
            return _memberRepo.Get(x => x.Id == memberId);
        }
        /// <summary>
        /// 用email獲取單一會員資料
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Member GetByEmail(string email)
        {
            return _memberRepo.GetAll().FirstOrDefault(x => x.Email == email);
        }
        /// <summary>
        /// 用email獲取單一會員的RegCode
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetAccountRegCodeByEmail(string email)
        {
            return _memberRepo.Get(x => x.Email == email).RegCode;
        }
        /// <summary>
        /// 創立新會員
        /// </summary>
        /// <param name="register_type"></param>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <param name="idtoken"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CreateNewAccount(sbyte register_type, string email,DateTime birthday, int region, string pwd = "", string idtoken = "", string name = "Name")
        {
            bool result = false;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 當地時區
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds;

            try
            {
                Member member = new Member();
                member.RegisterType= (byte)register_type;
                switch (register_type)
                {
                    case 0:
                        member.Password = _cryptography.encryptpwd(pwd);
                        member.RegCode = Guid.NewGuid().ToString("n");
                        member.RegThrData = string.Empty;
                        break;
                    case 1:
                        member.Password = string.Empty;
                        member.RegCode = string.Empty;
                        member.RegThrData = idtoken;
                        break;
                }
                member.IsReg = (byte)register_type;
                member.City = member.Photo = member.Conditions = string.Empty;
                member.Email = email;
                member.CountryId = region;
                member.MemberName = name;
                member.Birthday = TwRegionDateTimeToTimestamp(birthday);
                member.IsDel = 0;
                member.AddTime = member.LastTime = timeStamp;
                member.Height = 170;
                member.Weight = member.GoalWeight = 75;
                member.Bmi= 23;
                member.Sys = 135;
                member.Dia= 85;
                member.BodyFat = 20;
                member.Oxygen = 94;
                member.MeasuringTime = 30;
                member.BmiActivity = member.WeightActivity = member.BodyFatActivity = member.DiaActivity= member.SysActivity = member.OxygenActivity= member.MeasuringTimeActivity= 1;
                _memberRepo.Create(member);
                result = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 用eamil搜尋帳號認證與否 存在回傳true 否則回傳false
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool AccountIsActive(string email)
        {
            return _memberRepo.GetAll().Any(x => x.Email == email && x.IsReg == 1);
        }
       
        public bool AccountExist(string email)
        {
            return _memberRepo.GetAll().Any(x => x.Email == email && x.IsDel == 0);
        }
        /// <summary>
        /// 密碼或帳號是否有效
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VaildAccount(string email, string password)
        {
            bool result = false;

            string encryptPwd = _memberRepo.GetAll().Where(x => x.Email == email && x.IsDel == 0 && x.RegisterType == 0)
                .Select(x => x.Password).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(encryptPwd))
            {
                string sourcePwd = _cryptography.decryptpwd(encryptPwd);
                if (sourcePwd.Equals(password))
                {
                    result = true;
                }
            }
            return result;
        }
        public void UpdateAccountLastTime(Member model)
        {
            try
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 當地時區
                long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds;
                model.LastTime = timeStamp;
                _memberRepo.Update(model);
            }
            catch (Exception ex)
            {
         
            }
        }
        public Member GetAccountByReg_thr_data(string reg_thr_data)
        {
            return _memberRepo.GetAll().FirstOrDefault(x => x.RegThrData == reg_thr_data);
        }
        public long? GetAllRegionIdByRegCode(string reg_code)
        {
            if (string.IsNullOrWhiteSpace(reg_code))
                return null;
            return _sysCoutryRepo.GetAll().FirstOrDefault(x => x.Iso== reg_code.ToUpper())?.Id;
        }
        public int CheckMemberAccount(int memberId)
        {
            Member member = _memberRepo.Get(x => x.Id == memberId);
            if (member == null)
                return 2001;
            if (member.IsDel == 1)
                return 2004;
            if (member.IsReg == 0)
                return 2003;
            return 10000;
        }
        private static long TwRegionDateTimeToTimestamp(DateTime dt)
        {
            DateTime startTime = new DateTime(1970, 1, 1);
            long timeStamp = (long)(dt.AddHours(-8) - startTime).TotalSeconds;
            return timeStamp;
        }
    }
}
