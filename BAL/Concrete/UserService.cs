using Entities.Models;
using DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Abstract;

namespace BAL.Concrete
{
    public class UserService: IUserService
    {
        UserRepository userRepository = new UserRepository();
        BcryptService bcryptService = new BcryptService();

        public User Register(User user)
        {
            user.IsAdmin = false;
            user.UpdatedAt = DateTime.Now;
            user.CreatedAt = DateTime.Now;
            user.Addresses = [];
            user.CartItems = [];
            user.Orders = [];
            user.PasswordHash = bcryptService.HashPassword(user.PasswordHash);

            userRepository.Insert(user);
            return user;
        }
    }
}
