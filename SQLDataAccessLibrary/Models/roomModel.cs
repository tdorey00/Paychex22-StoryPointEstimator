using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccessLibrary.Models
{
    public class roomModel
    {
        public string roomName { get; set; } = "";
        public int roomId { get; set; }
        public string scaleTitle { get; set; } = "Enter A Title"; //1-24 scale title
        public int currentScale { get; set; }

    }
}
