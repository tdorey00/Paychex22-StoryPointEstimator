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
    }
}