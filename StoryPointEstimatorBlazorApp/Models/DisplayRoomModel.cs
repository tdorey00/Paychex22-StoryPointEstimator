using System.ComponentModel.DataAnnotations;
namespace StoryPointEstimatorBlazorApp.Models
{
    public class DisplayRoomModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Room name is too long.")]
        [MinLength(2, ErrorMessage = "Room name is too short.")]
        public string roomName { get; set; }
        [Required]
        [MaxLength(4, ErrorMessage = "Room code is too long.")] //join room
        [MinLength(4, ErrorMessage = "Room code is too short.")] //join room
        public int roomId { get; set; }
        
        //Potential Fix instead of using two data models, using redundant models where each instance of the user stores all room info
        [Required]
        [StringLength(50, ErrorMessage = "Username is too long.")]
        [MinLength(2, ErrorMessage = "Username is too short.")]
        public string userName { get; set; }
        public int userId { get; set; }
        public bool isAdmin { get; set; }
        //add vote later
        
        public DisplayRoomModel()
        {
            roomName = "";
            userName = "";
            isAdmin = false;
        }
    }
}
