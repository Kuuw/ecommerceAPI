namespace Entities.Context.Abstract;

public interface IUserContext
{
    int UserId { get; set; }
    string Role { get; set; }
}
