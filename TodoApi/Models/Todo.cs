
namespace TodoApi.Models
{
	public class Todo
	{
		public long Id { get; set; }
		public string? Title { get; set; }
		public bool IsComplete { get; set; } = false; // Default value set to false
		
		public long CategoryId { get; set; }
        public Category? Category { get; set; }

		// Constructor (if needed)
		public Todo()
		{
			// You can also set the default value here if you prefer
			// IsComplete = false;
		}

	}
}
