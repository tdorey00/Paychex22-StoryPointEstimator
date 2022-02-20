using System.ComponentModel.DataAnnotations;
namespace StoryPointEstimatorBlazorApp.Models
{
    public class DisplayRoomModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "Room name is too long.")]
        [MinLength(2, ErrorMessage = "Room name is too short.")]
        public string roomName { get; set; }
        public int roomId { get; set; }
        //public List<> users { get; set; } //Maybe use a list?
        public DisplayRoomModel()
        {
            roomName = "";
        }
    }
}
