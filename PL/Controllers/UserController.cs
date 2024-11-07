using BAL.Concrete;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserRepository userRepository = new UserRepository();

        [HttpPost]
        public IActionResult Register(String FirstName, String LastName, String Email, String PasswordHash)
        {
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
                return StatusCode(500, new { message = "İsteğinizi işlerken bir hata oluştu.", error = e.Message}); 
            }
        }
    }
}
