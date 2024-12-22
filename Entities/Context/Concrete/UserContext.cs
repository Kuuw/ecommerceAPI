using Entities.Context.Abstract;

namespace Entities.Context.Concrete;

public class UserContext : IUserContext
{
    public int UserId { get; set; }
}
