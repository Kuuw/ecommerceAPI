using Entities.Models;

namespace Entities.Context.Abstract;

public interface IUserContext
{
    int UserId { get; set; }
    string Role { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
}