
namespace SqlDataAccessLib
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }
        //Returns a single Row of a table
        Task<T> LoadSingleData<T, U>(string sql, U parameters);
        T LoadSingleDataSync<T, U>(string sql, U parameters);
        //Returns a list of Rows from a table
        Task<List<T>> LoadListDataAsync<T, U>(string sql, U parameters);
        List<T> LoadListDataSync<T, U>(string sql, U parameters);
        Task SaveDataAsync<T>(string sql, T parameters);
        void SaveDataSync<T>(string sql, T parameters); 
    }
}