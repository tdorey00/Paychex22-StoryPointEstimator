using SQLDataAccessLibrary.Models;

namespace SqlDataAccessLib
{
    public interface IRoomDataAccess
    {
        public Task<userModel> GetUserData(int userId);
        public Task<roomModel> GetRoomData(int roomId);
        List<int> GetUserIds();
        List<int> GetRoomIds();
        List<string> GetRoomNames();
        void createRoomSaveData(roomModel room, userModel user);
        void joinRoomSaveData(int roomId, userModel user);
        List<roomModel> GetRoomModels();
        void UpdateUsername(int userid, string username);
        void UpdateHideVotes(int roomid, bool status);
        void UpdateHideUsers(int roomid, bool status);
        void UpdateCustomScaleTitle(int roomid, string customtitle);
        void UpdateCustomScale(int roomid, int scale);
        Task<List<userModel>> getConnectedUsers(int roomid);
        void UpdateAdmin(int userid, bool isAdmin);
        void UpdateVote(int userid, int voting_mode, string vote);
        void removeUserData(int roomId, int userId);
        void removeRoomData(int roomId);
    }
}