namespace LostAndFound.Models.Entities
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Type { get; set; } // Lost or Found

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime DateLostFound { get; set; }

        public string ContactName { get; set; }

        public string ContactNumber { get; set; }

        public string Status { get; set; } = "Pending";

        public string ImagePath { get; set; }
    }
}