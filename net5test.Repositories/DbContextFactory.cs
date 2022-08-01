using Microsoft.EntityFrameworkCore;
using net5test.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Repositories
{
   public class DbContextFactory:IDbContextFactory
    {
        private DbContext dbCondext;

        public DbContextFactory(DbContext dbCondext)
        {
            this.dbCondext = dbCondext;
        }

        DbContext IDbContextFactory.CreatDbContext()
        {
            return dbCondext;
        }
    }
}
