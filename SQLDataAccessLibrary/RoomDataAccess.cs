using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLDataAccessLibrary.Models;

namespace SqlDataAccessLib
{
    public class RoomDataAccess : IRoomDataAccess
    {
        private readonly ISqlDataAccess _dB;
        public RoomDataAccess(ISqlDataAccess db)
        {
            _dB = db;
        }

        //Returns User Data based on given User ID
        //Parameters : userId = user's Id num
        public Task<userModel> GetUserData(int userId)
        {
            string sql = "select * from dbo.userTable where userId=" + userId; //gets user data from database that belongs to user
            return _dB.LoadSingleData<userModel, dynamic>(sql, new { });
        }
        //Returns Room Data based on given Room ID
        //Parameters : roomId = room's Id num
        public Task<roomModel> GetRoomData(int roomId)
        {
            string sql = "select * from dbo.roomTable where roomId=" + roomId; //gets room data from database that user is joining
            return _dB.LoadSingleData<roomModel, dynamic>(sql, new { });            
        }
        //Returns a List of User Ids
        public Task<List<int>> GetUserIds()
        {
            string sql = "select userId from dbo.userTable";
            return _dB.LoadListData<int, dynamic>(sql, new { });
        }
        //Returns a list of Room Codes
        public Task<List<int>> GetRoomIds()
        {
            string sql = "select roomId from dbo.roomTable";
            return _dB.LoadListData<int, dynamic>(sql, new { });
        }

    }
}
