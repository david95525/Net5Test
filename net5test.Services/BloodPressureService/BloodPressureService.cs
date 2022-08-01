using net5test.Common;
using net5test.Repositories.DataModels;
using net5test.Repositories.Interface;
using net5test.Services.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Services.BloodPressureService
{
   public class BloodPressureService:IBloodPressureService
    {
        private readonly IGenericRepository<Member> _memberRepo;
        private IGenericRepository<LogBloodpressure> _btRepo;
        private static object objLock = new object();

        public BloodPressureService(IGenericRepository<LogBloodpressure> btRepo,   IGenericRepository<Member> memberRepo)
        {
            this._memberRepo = memberRepo;
            this._btRepo = btRepo;
        }
        public IPagedList<BloodPressureDM> GetBloodPressureList(int pageIndex = 0, int pageSize = int.MaxValue,
            string email = null, DateTime? startDate = null, DateTime? endDate = null, int time_type = 0, string mode = null)
        {
            if (mode != null)
            {
                if (startDate.HasValue)
                    startDate = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, startDate.Value.Hour, startDate.Value.Minute, 0);
                if (endDate.HasValue)
                    endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, endDate.Value.Hour, endDate.Value.Minute, 59);
            }
            else
            {
                if (startDate.HasValue)
                    startDate = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, 00, 00, 00);
                if (endDate.HasValue)
                    endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59);
            }

            var query = from b in _btRepo.GetAll()
                        join m in _memberRepo.GetAll()
                        on b.MemberId equals m.Id
                        select new BloodPressureDM
                        {
                            email = m.Email,
                            name = m.MemberName,
                            id = b.Id,
                            member_id = b.MemberId,
                            bpm_id = b.BpmId,
                            user_id = b.UserId,
                            note = b.Note,
                            photo = b.Photo,
                            recording = b.Recording,
                            recording_time = b.RecordingTime,
                            sys = b.Sys,
                            dia = b.Dia,
                            pul = b.Pul,
                            afib = (sbyte)b.Afib,
                            pad = (sbyte)b.Pad,
                            mode = (sbyte)b.Mode,
                            mac_address = b.MacAddress,
                            update_date = b.UpdateDate,
                            update_date_H = (sbyte)b.UpdateDateH,
                            cuffokr = (sbyte?)b.Cuffokr,
                            modify_date = b.ModifyDate
                        };
            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(x => x.email.ToUpper().Contains(email.Trim().ToUpper()) || x.name.ToUpper().Contains(email.Trim().ToUpper()));
            }
            if (startDate.HasValue)
            {
                long startTimestamp = startDate.Value.ToUnixTimestampTW();

                if (time_type == 1)
                {

                    query = query.Where(x => x.update_date >= startTimestamp);
                }
                else
                {
                    query = query.Where(x => x.update_date >= startTimestamp);
                }
            }
            if (endDate.HasValue)
            {
                long endTimestamp = endDate.Value.ToUnixTimestampTW();

                if (time_type == 1)
                {
                    query = query.Where(x => x.modify_date <= endTimestamp);
                }
                else
                {
                    query = query.Where(x => x.modify_date <= endTimestamp);
                }
            }
            query = query.OrderByDescending(x => x.update_date);

            return new PagedList<BloodPressureDM>(query, pageIndex, pageSize);
        }
        public int CreateLogBloodPressure(LogBloodpressure model)
        {
            lock (objLock)
            {
                try
                {
                    if (_btRepo.GetAll().Where(x => x.MemberId == model.MemberId).Any(x => x.BpmId == model.BpmId))
                    {
                        model.BpmId = _btRepo.GetAll().Where(x => x.MemberId == model.MemberId).Max(x => x.BpmId) + 1;
                    }

                    if (model == null)
                        throw new ArgumentNullException("log_bloodpressure");

                    return _btRepo.Create(model);
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        public List< BloodPressureDM> GetBloodPressureByEmail(string email)
        {
            var member = _memberRepo.Get(x => x.Email == email);
            var list = _btRepo.GetAll().Where(b => b.MemberId == member.Id);
            List<BloodPressureDM> result = new List< BloodPressureDM>();
            foreach (LogBloodpressure item in list)
            {
                BloodPressureDM bpDM = new BloodPressureDM
                {
                    email = member.Email,
                    name = member.MemberName,
                    id = item.Id,
                    member_id = item.MemberId,
                    bpm_id = item.BpmId,
                    user_id = item.UserId,
                    note = item.Note,
                    photo = item.Photo,
                    recording = item.Recording,
                    recording_time = item.RecordingTime,
                    sys = item.Sys,
                    dia = item.Dia,
                    pul = item.Pul,
                    afib = (sbyte)item.Afib,
                    pad = (sbyte)item.Pad,
                    mode = (sbyte)item.Mode,
                    mac_address = item.MacAddress,
                    update_date = item.UpdateDate,
                    update_date_H = (sbyte)item.UpdateDateH,
                    cuffokr = (sbyte?)item.Cuffokr,
                    modify_date = item.ModifyDate
                };
                result.Add(bpDM);
            }

            return result;
        }
    }
}
