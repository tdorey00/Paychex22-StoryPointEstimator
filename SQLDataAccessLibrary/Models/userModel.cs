using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccessLibrary.Models
{
    public class userModel
    {
        //This model contains variables the relate to users exactly as they appear in the database in dbo.userTable

        public string userName { get; set; } = ""; //Display name of the user
        public int userId { get; set; } //Numeric ID of the user
        public bool isAdmin { get; set; } //Determines if the user is a facilitator or not
        public bool observer { get; set; } //Determines if the user is an observer or not
        public string fibVote { get; set; } = ""; //Vote for the fibonacci tool
        public string scaleVote { get; set; } = ""; //Vote for the custom scale tool
        public string fistVote { get; set; } = ""; //Vote for the fist of five tool
        public string tshirtVote { get; set; } = ""; //Vote for the t-shirt tool
        public override bool Equals(object? obj) //Compares user ids
        {
            userModel otherUser;
            try
            {
                 otherUser = (userModel)obj;
                 return userId == otherUser.userId;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
    }
}
