using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataAccessLib
{
    public class SqlDataAccess : ISqlDataAccess
    {
        //This class contains the code used to connect to the database and execute SQL Queries/Statements on said database
        //it contains definitions for synchronous and asynchronous functions to control when data flows in and out of the database

        private readonly IConfiguration _config; //recieved from StoryPointEstimatorBlazorApp

        public string ConnectionStringName { get; set; } = "Default"; //can be used in place of connection string from appsettings.json but this is not recommended

        public SqlDataAccess(IConfiguration config) //initializes the configuration on startup
        {
            _config = config;
        }
        //Returns a single Row of a table
        public async Task<T> LoadSingleData<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName); //get connection string from config

            using (IDbConnection connection = new SqlConnection(connectionString)) //connect to database
            {
                var data = await connection.QueryAsync<T>(sql, parameters); //execute
                return data.First<T>(); //return single row
            }
        }
        //returns a single row of a table synchronously 
        public T LoadSingleDataSync<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName); //get connection string from config

            using (IDbConnection connection = new SqlConnection(connectionString)) //connect to database
            {
                var data = connection.Query<T>(sql, parameters); //execute
                return data.First<T>(); //return single row
            }
        }
        //Returns a list of Rows from a table Asynchronously 
        public async Task<List<T>> LoadListDataAsync<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList<T>(); //return list of type T
            }
        }

        //Returns a list of Rows from a table Synchrously 
        public List<T> LoadListDataSync<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = connection.Query<T>(sql, parameters);
                return data.ToList<T>(); //return list of type T
            }
        }
        //Saves data to dB synchronously (also used to update records)
        public void SaveDataSync<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, parameters); 
            }
        }
        //saves data to Db asynchronously (also used to update records)
        public async Task SaveDataAsync<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}