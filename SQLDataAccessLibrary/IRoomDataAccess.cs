using SQLDataAccessLibrary.Models;

namespace SqlDataAccessLib
{
    public interface IRoomDataAccess
    {
        public Task<userModel> GetUserData(int userId);
        public Task<roomModel> GetRoomData(int roomId);
        List<int> GetUserIds();
        List<int> GetRoomIds();
        void createRoomSaveData(roomModel room, userModel user);
        void joinRoomSaveData(int roomId, userModel user);
        List<roomModel> GetRoomModels();
        void UpdateUsername(int userid, string username);
        void UpdateCustomScale(int roomid, string customtitle);
        Task<List<userModel>> getConnectedUsers(int roomid);
        void UpdateAdmin(int userid, bool isAdmin);
        void UpdateVote(int userid, int voting_mode, string vote);
        void removeUserData(int roomId, int userId);
    }
}