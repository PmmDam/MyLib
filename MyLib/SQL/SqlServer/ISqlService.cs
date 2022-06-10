using Microsoft.Data.SqlClient;
using System.Data;

namespace MyLib.SQL.SqlServer
{
    public interface ISqlService
    {
        string DefaultConnectionString { get; }

        bool CheckConnection();
        string CreateConnectionString(string serverName, string login, string password, string dbname);
        Task<int> ExecuteNonQueryAsync(string query, CommandType queryType, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryAsync(string connectionString, string query, CommandType queryType, params SqlParameter[] parameters);
        Task ExecuteReaderAsync(string query, CommandType queryType, Func<SqlDataReader, Task> dataHandlerAsync, params SqlParameter[] parameters);
        Task ExecuteReaderAsync(string connectionString, string query, CommandType queryType, Func<SqlDataReader, Task> dataHandlerAsync, params SqlParameter[] parameters);
        Task<object> ExecuteScalarAsync(string query, CommandType queryType, params SqlParameter[] parameters);
        Task<object> ExecuteScalarAsync(string connectionString, string query, CommandType queryType, params SqlParameter[] parameters);
    }
}