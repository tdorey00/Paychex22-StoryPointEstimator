using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SQLDataAccessLibrary.Models;

namespace SqlDataAccessLib
{
    public class RoomDataAccess : IRoomDataAccess
    {
        //This class contains all functions used to store/remove/get/update data in the database it uses SqlDataAccess to send the data to the database
        //Database Table Guide:
        //  dbo.roomTable contains all room data, see SQLDataAccessLibrary.Models roomModel for columns in database and their datatype
        //  dbo.roomUserTable contains all links between users and the rooms they are connected to, see SQLDataAccessLibrary.Models roomUserModel for columns in database and their datatype
        //  dbo.userTable contains all user data, see SQLDataAccessLibrary.Models userModel for columns in database and their datatype

        private readonly ISqlDataAccess _dB; //database connection instance
        public RoomDataAccess(ISqlDataAccess db) //initializes database connection instance from SqlDataAccess
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

        //Returns a list of Room names
        public List<string> GetRoomNames()
        {
            string sql = "select roomName from dbo.roomTable";
            return _dB.LoadListDataSync<string, dynamic>(sql, new { });
        }

        //Returns a list of roomModels
        public List<roomModel> GetRoomModels()
        {
            string sql = "select * from dbo.roomTable";
            return _dB.LoadListDataSync<roomModel, dynamic>(sql, new { });
        }

        //Updates the username of a given user
        public async void UpdateUsername(int userid, string username)
        {
            var parameters = new {userID = userid, userName = username };
            string sql = "update dbo.userTable set userName = @userName where userId = @userID;";
            await _dB.SaveDataAsync(sql, parameters);
        }

        //updates the custom title of a given room
        public async void UpdateCustomScaleTitle(int roomid, string customtitle)
        {
            var parameters = new {roomID = roomid, customTitle = customtitle};
            string sql = "update dbo.roomTable set scaleTitle = @customTitle where roomId = @roomID;";
            await _dB.SaveDataAsync(sql, parameters);
        }

        //updates the number of buttons on custom scale of a given room
        public async void UpdateCustomScale(int roomid, int scale)
        {
            var parameters = new {roomID = roomid, scale = scale};
            string sql = "update dbo.roomTable set currentScale = @scale where roomId = @roomID";
            await _dB.SaveDataAsync(sql,parameters);
        }

        //Updates the admin status of a given user
        //isAdmin = true if user is an admin, false otherwise
        //observe = true if admin is an observer (does not vote), false otherwise
        public async void UpdateAdmin(int userid, bool isAdmin, bool observe)
        {
            var parameters = new {userId = userid, isAdmin = isAdmin, observer = observe};
            string sql = "update dbo.userTable set isAdmin = @isAdmin, observer = @observer where userId = @userId";
            await _dB.SaveDataAsync(sql, parameters);
        }

        //updates the status of if votes are hidden so all users who join after are in the correct state
        //status = true if hidden, false otherwise
        public async void UpdateHideVotes(int roomid, bool status)
        {
            var parameters = new { roomId = roomid, hideVotes = status };
            string sql = "update dbo.roomTable set hideVotes = @hideVotes where roomId = @roomId";
            await _dB.SaveDataAsync(sql,parameters);
        }

        //updates the status of if users are hidden so all users who join after are in the correct state
        //status = true if hidden, false otherwise
        public async void UpdateHideUsers(int roomid, bool status)
        {
            var parameters = new { roomId = roomid, hideUsers = status };
            string sql = "update dbo.roomTable set hideUsers = @hideUsers where roomId = @roomId";
            await _dB.SaveDataAsync(sql, parameters);
        }

        //Updates the vote of a given user based on which voting_mode the client was in at the time of the function call
        //voting_mode:
        // 1 = fibbonaci vote
        // 2 = fist of five vote
        // 3 = tshirt vote
        // 4 = custom vote
        //vote = the vote to be stored in the database
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

        //This function first clears the votes of everyone connected to a given room then returns an updated list of all users in the given room
        public async Task<List<userModel>> GetClearedVotesList(int roomId)
        {
            var parameters = new {roomId = roomId};
            string sql = "select * from dbo.roomUserTable where roomId = @roomId";
            List<roomUserModel> temp = await _dB.LoadListDataAsync<roomUserModel,dynamic>(sql, parameters); //get list of users in given room
            foreach (roomUserModel model in temp)
            {
                try
                {
                    var param2 = new { userId = model.userId }; 
                    string sql2 = "UPDATE dbo.userTable SET fibVote = '', tshirtVote = '', fistVote = '', scaleVote = '' WHERE userId = @userId"; //clear the votes
                    await _dB.SaveDataAsync(sql2, param2);
                }
                catch (SqlException) { } //if user is not in database it means they disconnected during clear, do nothing
            }
            return await getConnectedUsers(roomId); //return updated list of connected users
        }

        //gets the list of all connected users in a given room to the database including all data associated with each user
        public async Task<List<userModel>> getConnectedUsers(int roomid)
        {
            var parameters = new { roomId = roomid };
            string sql = "select * from dbo.roomUserTable where roomId = @roomId";
            List<roomUserModel> connections = _dB.LoadListDataSync<roomUserModel, dynamic>(sql, parameters); //list of userIds which are connected to the roomId
            List<userModel> connectedUsers = new List<userModel>();
            foreach(roomUserModel data in connections) //for each connection to the room
            {
                var parameters2 = new { userId =  data.userId };
                sql = "select * from dbo.userTable where userId = @userId"; //grab the user's data
                connectedUsers.Add(await _dB.LoadSingleData<userModel, dynamic>(sql, parameters2)); //add the user to the list
            }
            return connectedUsers; //return list of userModel
        }

        //gets the given user's current admin status
        public bool getAdminStatus(int userId)
        {
            var parameters = new {userId = userId};
            string sql = "select isAdmin from dbo.userTable where userId = @userId";
            return _dB.LoadSingleDataSync<bool, dynamic>(sql, parameters);
        }

        //removes the given user from the given room, also deletes the connection in dbo.roomUserTable
        public void removeUserData(int roomId, int userId)
        {
            var param1 = new { roomId = roomId, userId = userId };
            string sql1 = "DELETE FROM dbo.roomUserTable WHERE roomId = @roomId AND userId = @userId";
            var param2 = new {userId = userId };
            string sql2 = "DELETE from dbo.userTable WHERE userId = @userId";
            _dB.SaveDataSync(sql1, param1);
            _dB.SaveDataSync(sql2, param2);
        }

        //removes the given room from the database on room delete also ensures any links to the room in dbo.roomUserTable are cleared
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
