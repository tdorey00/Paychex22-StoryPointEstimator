using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccessLibrary.Models
{
    internal class roomUserModel
    {
        public int Id { get; set; }
        public int userId { get; set; } 
        public int roomId { get; set; }
    }
}
