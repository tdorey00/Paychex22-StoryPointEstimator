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
        public string vote { get; set; } = "";
    }
}
