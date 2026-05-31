namespace LostAndFound.Models.Entities
{
    public class ClaimHistory
    {
        public Guid Id { get; set; }

        public string ItemTitle { get; set; }

        public string ClaimedBy { get; set; }

        public DateTime DateClaimed { get; set; }
    }
}
