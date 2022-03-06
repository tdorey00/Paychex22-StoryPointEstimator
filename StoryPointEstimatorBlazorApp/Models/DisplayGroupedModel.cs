using System.ComponentModel.DataAnnotations;
namespace StoryPointEstimatorBlazorApp.Models
{
    public class DisplayGroupedModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Room name is too long.")]
        [MinLength(3, ErrorMessage = "Room name is too short.")]
        public string roomName { get; set; } = "";
        [Required]
        [Range(1000, 9999, ErrorMessage = "Room Code invalid (1000-9999).")]
        public int roomId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Username is too long.")]
        [MinLength(3, ErrorMessage = "Username is too short.")]
        public string userName { get; set; } = "";
        public int userId { get; set; }
        public bool isAdmin { get; set; } = false;
        public string vote { get; set; } = "";
        
    }
}
