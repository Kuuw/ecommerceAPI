using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Abstract
{
    public interface IBcryptService
    {
        public bool VerifyPassword(string password, string passwordHash);
        public string HashPassword(string password);
    }
}
