using net5test.Common;
using net5test.Repositories.DataModels;
using net5test.Services.DomainModels;
using System;
using System.Collections.Generic;

namespace net5test.Services.BloodPressureService
{
   public interface IBloodPressureService
    {
        IPagedList<BloodPressureDM> GetBloodPressureList(int pageIndex = 0, int pageSize = int.MaxValue,string email = null,
              DateTime? startDate = null,DateTime? endDate = null,int time_type = 0,string mode = null);
       int CreateLogBloodPressure(LogBloodpressure bloodpressure);
        List< BloodPressureDM>  GetBloodPressureByEmail(string email);
    }
}
