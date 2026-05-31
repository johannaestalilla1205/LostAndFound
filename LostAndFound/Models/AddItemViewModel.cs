
namespace LostAndFound.Models
{
    public class AddItemViewModel
    {
        public string Title { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime DateLostFound { get; set; }

        public string ContactName { get; set; }

        public string ContactNumber { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}