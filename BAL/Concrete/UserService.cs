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
            var PasswordHash = bcryptService.HashPassword(user.PasswordHash);
            user.PasswordHash = PasswordHash;

            userRepository.Insert(user);
            return user;
        }

        public User? GetUserFromEmail(string email) 
        {
            User? user = userRepository.Where(x => x.Email == email).FirstOrDefault();

            return user;
        }
    }
}
