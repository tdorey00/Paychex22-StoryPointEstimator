using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccessLibrary.Models
{
    internal class roomUserModel
    {
        //This model contains variables the relate to the linkage between users and their rooms exactly as they appear in the database in dbo.roomUserTable
        public int Id { get; set; } //Generic primary key (Only used for storage)
        public int userId { get; set; } //UserID stored in user table
        public int roomId { get; set; } //RoomID stored in room table
    }
}
