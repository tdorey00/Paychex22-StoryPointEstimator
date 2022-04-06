using System.ComponentModel.DataAnnotations;
namespace StoryPointEstimatorBlazorApp.Models
{
    public class DisplayGroupedModel : IDisplayGroupedModel
    {
        //ROOM MODEL
        [Required]
        [StringLength(100, ErrorMessage = "Room name is too long.")]
        [MinLength(3, ErrorMessage = "Room name is too short.")]
        public string roomName { get; set; } = "";
        [Required]
        [Range(1000, 9999, ErrorMessage = "Room Code invalid (1000-9999).")]
        public int roomId { get; set; }
        public string scaleTitle { get; set; } = "Enter A Title"; //1-24 scale title
        public int currentScale { get; set; }
        //USER MODEL
        [Required]
        [StringLength(50, ErrorMessage = "Username is too long.")]
        [MinLength(3, ErrorMessage = "Username is too short.")]
        public string userName { get; set; } = "";
        public int userId { get; set; }
        public bool isAdmin { get; set; } = false;
        public string fibVote { get; set; } = "";
        public string scaleVote { get; set; } = "";
        public string fistVote { get; set; } = "";
        public string tshirtVote { get; set; } = "";
        
    }
}
