using BAL.Concrete;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserRepository userRepository = new UserRepository();
        BcryptService bcryptService = new BcryptService();


        [HttpPost("Register")]
        public IActionResult Register(String FirstName, String LastName, String Email, String Password)
        {
            String PasswordHash = bcryptService.HashPassword(Password);
            User newUser = new User();
            newUser.FirstName = FirstName;
            newUser.LastName = LastName;
            newUser.Email = Email;
            newUser.PasswordHash = PasswordHash;
            newUser.IsAdmin = false;

            try
            {
                if (newUser == null)
                {
                    BadRequest(new { message="Kullanıcı verisi geçersiz." });
                }
                userRepository.Insert(newUser);
                return Ok(new { message = "Kullanıcı başarıyla oluşturuldu."});
            } 
            catch(Exception e) 
            {
                if (e.InnerException != null)
                {
                    Console.WriteLine(e.InnerException.ToString());
                }
                return StatusCode(500, new { message = "İsteğinizi işlerken bir hata oluştu.", error = e.Message}); 
            }
        }
    }
}
