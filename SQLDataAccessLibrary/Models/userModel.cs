using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccessLibrary.Models
{
    public class userModel
    {
        public string userName { get; set; } = "";
        public int userId { get; set; }
        public bool isAdmin { get; set; }
        public string fibVote { get; set; } = "";
        public string scaleVote { get; set; } = "";
        public string fistVote { get; set; } = "";
        public string tshirtVote { get; set; } = "";
    }
}
