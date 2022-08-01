using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net5test.Repositories.DataModels;

namespace net5test.Services.MemberService
{
    public interface IMemberService
    {
        Member GetById(int memberId);
        Member GetByEmail(string email);
        string GetAccountRegCodeByEmail(string email);
        bool CreateNewAccount(sbyte register_type, string email, DateTime birthday, int region, string pwd = "", string idtoken = "", string name = "Name");
        bool AccountIsActive(string email);
        /// <summary>
        /// 用eamil搜尋帳號存在與否 存在回傳true 否則回傳false
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool AccountExist(string email);
        bool VaildAccount(string email, string password);
        void UpdateAccountLastTime(Member model);
        public int CheckMemberAccount(int memberId);
        long? GetAllRegionIdByRegCode(string reg_code);
        Member GetAccountByReg_thr_data(string reg_thr_data);
    }
}
