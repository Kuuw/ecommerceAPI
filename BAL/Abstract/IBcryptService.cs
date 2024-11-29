namespace BAL.Abstract
{
    public interface IBcryptService
    {
        public bool VerifyPassword(string password, string passwordHash);
        public string HashPassword(string password);
    }
}
