namespace Entities.Concrete
{
    public class User
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
