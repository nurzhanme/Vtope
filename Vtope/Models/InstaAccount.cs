namespace Vtope.Models
{
    public class InstaAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? SessionData { get; set; }
        public bool IsUtil { get; set; }
    }
}
