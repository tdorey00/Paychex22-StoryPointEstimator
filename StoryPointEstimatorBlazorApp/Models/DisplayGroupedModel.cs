using System.ComponentModel.DataAnnotations;
namespace StoryPointEstimatorBlazorApp.Models
{
    public class DisplayGroupedModel : IDisplayGroupedModel
    {
        //This Datamodel contains both the roomModel and userModel variables found in SQLDataAccessLibrary.Models in one class for ease of use

        //ROOM MODEL
        [Required]
        [StringLength(100, ErrorMessage = "Room name is too long.")]
        [MinLength(3, ErrorMessage = "Room name is too short.")]
        public string roomName { get; set; } = ""; //name of room
        [Required]
        [Range(1000, 9999, ErrorMessage = "Room Code invalid (1000-9999).")]
        public int roomId { get; set; } //id of room
        public string scaleTitle { get; set; } = "Enter A Title"; //Custom scale title
        public int currentScale { get; set; } //Custom button count
        //USER MODEL
        [Required]
        [StringLength(50, ErrorMessage = "Username is too long.")]
        [MinLength(3, ErrorMessage = "Username is too short.")]
        public string userName { get; set; } = "";
        public int userId { get; set; }
        public bool isAdmin { get; set; } = false;
        public bool observer { get; set; } = false;
        public string fibVote { get; set; } = ""; //Fibonacci Vote
        public string scaleVote { get; set; } = ""; //Custom Vote
        public string fistVote { get; set; } = ""; //Fist of 5 Vote
        public string tshirtVote { get; set; } = ""; //Tshirt vote
        
    }
}
