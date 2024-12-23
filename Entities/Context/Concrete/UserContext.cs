using Entities.Context.Abstract;

namespace Entities.Context.Concrete;

public class UserContext : IUserContext
{
    public int UserId { get; set; }
    public string Role { get; set; } = "User";
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}
