namespace StoryPointEstimatorBlazorApp.Models
{
    public class DisplayUserModel
    {
        public int userId { get; set; }
        public string userName { get; set; } = "";
        public bool isAdmin { get; set; } = false;
        public string vote { get; set; } = "";
    }
}
