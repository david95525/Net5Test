using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Repositories.Interface
{
     public interface IDbContextFactory
    {
        DbContext CreatDbContext();
    }
}
