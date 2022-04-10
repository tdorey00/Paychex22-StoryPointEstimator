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

        public List<string> GetRoomNames()
        {
            string sql = "select roomName from dbo.roomTable";
            return _dB.LoadListDataSync<string, dynamic>(sql, new { });
        }
        public List<roomModel> GetRoomModels()
        {
            string sql = "select * from dbo.roomTable";
            return _dB.LoadListDataSync<roomModel, dynamic>(sql, new { });
        }

        public async void UpdateUsername(int userid, string username)
        {
            var parameters = new {userID = userid, userName = username };
            string sql = "update dbo.userTable set userName = @userName where userId = @userID;";
            await _dB.SaveDataAsync(sql, parameters);
        }

        public async void UpdateCustomScaleTitle(int roomid, string customtitle)
        {
            var parameters = new {roomID = roomid, customTitle = customtitle};
            string sql = "update dbo.roomTable set scaleTitle = @customTitle where roomId = @roomID;";
            await _dB.SaveDataAsync(sql, parameters);
        }

        public async void UpdateCustomScale(int roomid, int scale)
        {
            var parameters = new {roomID = roomid, scale = scale};
            string sql = "update dbo.roomTable set currentScale = @scale where roomId = @roomID";
            await _dB.SaveDataAsync(sql,parameters);
        }

        public async void UpdateAdmin(int userid, bool isAdmin)
        {
            var parameters = new {userId = userid, isAdmin = isAdmin};
            string sql = "update dbo.userTable set isAdmin = @isAdmin where userId = @userId";
            await _dB.SaveDataAsync(sql, parameters);
        }

        public async void UpdateHideVotes(int roomid, bool status)
        {
            var parameters = new { roomId = roomid, hideVotes = status };
            string sql = "update dbo.roomTable set hideVotes = @hideVotes where roomId = @roomId";
            await _dB.SaveDataAsync(sql,parameters);
        }

        public async void UpdateHideUsers(int roomid, bool status)
        {
            var parameters = new { roomId = roomid, hideUsers = status };
            string sql = "update dbo.roomTable set hideUsers = @hideUsers where roomId = @roomId";
            await _dB.SaveDataAsync(sql, parameters);
        }

        public async void UpdateVote(int userid, int voting_mode ,string vote)
        {
            if(voting_mode == 1) //fibbonaci
            {
                var parameters = new { userId = userid, fibVote = vote };
                string sql = "update dbo.userTable set fibVote = @fibVote where userId = @userId";
                await _dB.SaveDataAsync(sql,parameters);
            }
            else if (voting_mode == 2) //fist of five
            {
                var parameters = new { userId = userid, fistVote = vote };
                string sql = "update dbo.userTable set fistVote = @fistVote where userId = @userId";
                await _dB.SaveDataAsync(sql, parameters);
            }
            else if (voting_mode == 3) //tshirt vote
            {
                var parameters = new { userId = userid, tshirtVote = vote };
                string sql = "update dbo.userTable set tshirtVote = @tshirtVote where userId = @userId";
                await _dB.SaveDataAsync(sql, parameters);
            }
            else if(voting_mode == 4) //custom vote
            {
                var parameters = new { userId = userid, scaleVote = vote };
                string sql = "update dbo.userTable set scaleVote = @scaleVote where userId = @userId";
                await _dB.SaveDataAsync(sql, parameters);
            }
        }

        public async Task<List<userModel>> getConnectedUsers(int roomid)
        {
            var parameters = new { roomId = roomid };
            string sql = "select * from dbo.roomUserTable where roomId = @roomId";
            List<roomUserModel> connections = _dB.LoadListDataSync<roomUserModel, dynamic>(sql, parameters);
            List<userModel> connectedUsers = new List<userModel>();
            foreach(roomUserModel data in connections)
            {
                var parameters2 = new { userId =  data.userId };
                sql = "select * from dbo.userTable where userId = @userId";
                connectedUsers.Add(await _dB.LoadSingleData<userModel, dynamic>(sql, parameters2));
            }
            return connectedUsers;
        }

        public void removeUserData(int roomId, int userId)
        {
            var param1 = new { roomId = roomId, userId = userId };
            string sql1 = "DELETE FROM dbo.roomUserTable WHERE roomId = @roomId AND userId = @userId";
            var param2 = new {userId = userId };
            string sql2 = "DELETE from dbo.userTable WHERE userId = @userId";
            _dB.SaveDataSync(sql1, param1);
            _dB.SaveDataSync(sql2, param2);
        }

        public void removeRoomData(int roomId)
        {
            var parameters = new {roomId = roomId};
            string sql1 = "DELETE FROM dbo.roomUserTable WHERE roomId = @roomId";
            string sql2 = "DELETE FROM dbo.roomTable WHERE roomId = @roomId";
            _dB.SaveDataSync(sql1, parameters);
            _dB.SaveDataSync(sql2, parameters);
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
