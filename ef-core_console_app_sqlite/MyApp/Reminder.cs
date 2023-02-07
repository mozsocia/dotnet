using System.ComponentModel.DataAnnotations;

namespace MyApp {
    public class Reminder {
        [Key]
        public int Id { get; set;}
        [Required]
        public string Title { get; set;}
    }
}