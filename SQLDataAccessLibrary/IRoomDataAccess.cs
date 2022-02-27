using SQLDataAccessLibrary.Models;

namespace SqlDataAccessLib
{
    public interface IRoomDataAccess
    {
        public Task<userModel> GetUserData(int userId);
        public Task<roomModel> GetRoomData(int roomId);
        Task<List<int>> GetUserIds();
        Task<List<int>> GetRoomIds();
    }
}