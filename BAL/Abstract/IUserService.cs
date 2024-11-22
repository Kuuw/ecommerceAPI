using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Abstract
{
    public interface IUserService
    {
        public User Register(User user);
    }
}
