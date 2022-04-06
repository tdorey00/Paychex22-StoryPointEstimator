namespace StoryPointEstimatorBlazorApp.Models
{
    public interface IDisplayGroupedModel
    {
        //ROOM MODEL
        string roomName { get; set; }
        public int roomId { get; set; }
        string scaleTitle { get; set; }
        public int currentScale { get; set; }
        //USER MODEL

        string userName { get; set; }
        int userId { get; set; }
        bool isAdmin { get; set; } 
        string fibVote { get; set; }
        string scaleVote { get; set; } 
        string fistVote { get; set; } 
        string tshirtVote { get; set; }
    }
}
