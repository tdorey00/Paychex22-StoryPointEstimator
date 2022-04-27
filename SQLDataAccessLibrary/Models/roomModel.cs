using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccessLibrary.Models
{
    public class roomModel
    {
        //This model contains variables the relate to rooms exactly as they appear in the database in dbo.roomTable

        public string roomName { get; set; } = ""; //Name of the room
        public int roomId { get; set; } //Numeric ID of the room
        public string scaleTitle { get; set; } = "Custom Title"; //1-24 scale title
        public int currentScale { get; set; } //Current size of the scale
        public bool hideVotes { get; set; } //Used for determining whether votes are hidden or unhidden
        public bool hideUsers { get; set; } //Used for determining whether users are hidden or unhidden

    }
}
