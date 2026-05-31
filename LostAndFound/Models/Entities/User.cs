namespace LostAndFound.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string StudentNumber { get; set; }

        public string Password { get; set; }

        public string Role { get; set; } // Student or Guest
    }
}