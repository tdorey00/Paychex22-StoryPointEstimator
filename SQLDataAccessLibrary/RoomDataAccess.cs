using System;
using Dapper;
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
            var parameters = new { userID = userId };
            string sql = "select * from dbo.userTable where userId = @UserID"; //gets user data from database that belongs to user
            return _dB.LoadSingleData<userModel, dynamic>(sql, parameters);
        }
        //Returns Room Data based on given Room ID
        //Parameters : roomId = room's Id num
        public Task<roomModel> GetRoomData(int roomId)
        {
            var parameters = new { roomID = roomId };
            string sql = "select * from dbo.roomTable where roomId = @roomID"; //gets room data from database that user is joining
            return _dB.LoadSingleData<roomModel, dynamic>(sql, parameters);            
        }
        //Returns a List of User Ids
        public List<int> GetUserIds()
        {
            string sql = "select userId from dbo.userTable";
            return _dB.LoadListDataSync<int, dynamic>(sql, new { });
        }
        //Returns a list of Room Codes
        public List<int> GetRoomIds()
        {
            string sql = "select roomId from dbo.roomTable";
            return _dB.LoadListDataSync<int, dynamic>(sql, new { });
        }

        public List<roomModel> GetRoomModels()
        {
            string sql = "select * from dbo.roomTable";
            return _dB.LoadListDataSync<roomModel, dynamic>(sql, new { });
        }

        //insert records of room and user into the database while also linking the records in the roomUser table
        //only used for createRoom page b/c function creates a new room in dB
        //Parameters: room = roomModel with room data from Grouped Model
        //            user = userModel with user data from Grouped Model
        public void createRoomSaveData(roomModel room, userModel user)
        {
            var roomParameters = new { roomId = room.roomId, roomName = room.roomName };
            var userParameters = new { userId = user.userId, userName = user.userName, isAdmin = user.isAdmin}; //array of parameters for user data
            var roomUserParameters = new {userId = user.userId, roomId = room.roomId};

            string roomSql = "INSERT INTO dbo.roomTable (roomId, roomName)" +
                             "VALUES (@roomId, @roomName);";            //insert room data into database using sql statement
            
            string userSql = "INSERT INTO dbo.userTable (userId, userName, isAdmin)" + 
                             "VALUES (@userId, @userName, @isAdmin);";  //insert user data into database with the sql statement 

            string roomUserSql = "INSERT INTO dbo.roomUserTable(roomId, userId)" +
                                 "VALUES (@roomId, @userId)";           //insert the roomid and userid into the roomUser table to make the connection between them

            _dB.SaveDataSync(roomSql, roomParameters);  //creating a record for room
            _dB.SaveDataSync(userSql, userParameters);  
            _dB.SaveDataSync(roomUserSql, roomUserParameters); //creating a record for roomUser table 
        }

        //insert records of user and linkage between user and room into the database
        //we only need user information and the linkage information for joining a room
        //Parameters: user = userModel with user data from the Grouped model
        public void joinRoomSaveData(int roomId, userModel user)
        {
            var userParameters = new { userId = user.userId, userName = user.userName, isAdmin = user.isAdmin}; //array of parameters for user data
            var roomUserParameters = new {userId = user.userId, roomId = roomId};

            string userSql = "INSERT INTO dbo.userTable (userId, userName, isAdmin)" + 
                             "VALUES (@userId, @userName, @isAdmin);";  //insert user data into database with the sql statement 

            string roomUserSql = "INSERT INTO dbo.roomUserTable(roomId, userId)" +
                                 "VALUES (@roomId, @userId)";           //insert the roomid and userid into the roomUser table to make the connection between them

            _dB.SaveDataSync(userSql, userParameters);  
            _dB.SaveDataSync(roomUserSql, roomUserParameters); //creating a record for roomUser table 
        }

    }
}
