using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net5test.Common.Cryptography
{
   public interface ICryptography
    {
        public string encryptpwd(string password);
        public string decryptpwd(string encryptPwd);
    }
}
