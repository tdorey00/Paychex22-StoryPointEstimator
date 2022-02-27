
namespace SqlDataAccessLib
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }
        //Returns a single Row of a table
        Task<T> LoadSingleData<T, U>(string sql, U parameters);
        //Returns a list of Rows from a table
        Task<List<T>> LoadListData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
    }
}