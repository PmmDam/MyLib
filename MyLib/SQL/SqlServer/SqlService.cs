using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Sql.SqlServer
{
    /// <summary>
    /// This class is used to make querys to a SQL SERVER database, it must have a default connection string 
    /// but also all methods have and overload to put another connection string
    /// </summary>
    public class SqlService : ISqlService
    {

        private string _serverNameOrIp { get; set; }
        private string _login { get; set; }
        private string _password { get; set; }
        private string _dbName { get; set; }


        private string _defaultConnectionString { get; set; }
        public string DefaultConnectionString
        {
            get
            {
                if (this._defaultConnectionString == null)
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                    builder.DataSource = this._serverNameOrIp;
                    builder.UserID = this._login;
                    builder.Password = this._password;
                    builder.InitialCatalog = this._dbName;
                    builder.TrustServerCertificate = true;

                    this._defaultConnectionString = builder.ToString();

                }

                return this._defaultConnectionString;
            }
        }


        public SqlService(string connectionString)
        {
            this._defaultConnectionString = connectionString;
        }
        public SqlService(string serverName, string login, string password, string dbname)
        {
            this._serverNameOrIp = serverName;
            this._login = login;
            this._password = password;
            this._dbName = dbname;
        }

        /// <summary>
        /// Connects to the default database and execute a query which do not return objects, only the number of rows affected:
        /// INSERT,DELETE,UPDATE,Etc..
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(string query, CommandType queryType, params SqlParameter[] parameters)
        {
            int affectedRows = -1;




            SqlConnection connection = new SqlConnection(this.DefaultConnectionString);

            try
            {
                await connection.OpenAsync();
                SqlCommand cmd = connection.CreateCommand();
                try
                {
                    cmd.CommandText = query;
                    cmd.CommandType = queryType;
                    cmd.Parameters.AddRange(parameters);
                    affectedRows = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        await cmd.DisposeAsync().ConfigureAwait(false);
                    }
                }

            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync().ConfigureAwait(false);
                    await connection.DisposeAsync().ConfigureAwait(false);
                }

            }
            return affectedRows;
        }
        /// <summary>
        /// Specifies the connectionString and execute a query which do not return objects, only the number of rows affected: INSERT,DELETE,UPDATE,Etc..
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="query"></param>
        /// <param name="queryType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(string connectionString, string query, CommandType queryType, params SqlParameter[] parameters)
        {
            int affectedRows = -1;




            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                SqlCommand cmd = connection.CreateCommand();
                try
                {
                    cmd.CommandText = query;
                    cmd.CommandType = queryType;
                    cmd.Parameters.AddRange(parameters);
                    affectedRows = await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        await cmd.DisposeAsync().ConfigureAwait(false);
                    }
                }

            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync().ConfigureAwait(false);
                    await connection.DisposeAsync().ConfigureAwait(false);
                }

            }
            return affectedRows;
        }
        /// <summary>
        /// Connects to the default database and execute a query which return only one object, 
        /// For example: SELECT * from [Table] WHERE parameter = @parameter
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<object> ExecuteScalarAsync(string query, CommandType queryType, params SqlParameter[] parameters)
        {
            object objectResult = null;

            SqlConnection connection = new SqlConnection(this.DefaultConnectionString);

            try
            {
                await connection.OpenAsync();
                SqlCommand cmd = connection.CreateCommand();
                try
                {
                    cmd.CommandText = query;
                    cmd.CommandType = queryType;
                    cmd.Parameters.AddRange(parameters);
                    objectResult = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        await cmd.DisposeAsync().ConfigureAwait(false);
                    }
                }
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync().ConfigureAwait(false);
                    await connection.DisposeAsync().ConfigureAwait(false);
                }

            }
            return objectResult;
        }
        /// <summary>
        /// Specifies the connectionString and execute a query which return only one object, 
        /// For example: SELECT * from [Table] WHERE parameter = @parameter
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="query"></param>
        /// <param name="queryType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<object> ExecuteScalarAsync(string connectionString, string query, CommandType queryType, params SqlParameter[] parameters)
        {
            object objectResult = null;

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                SqlCommand cmd = connection.CreateCommand();
                try
                {
                    cmd.CommandText = query;
                    cmd.CommandType = queryType;
                    cmd.Parameters.AddRange(parameters);
                    objectResult = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        await cmd.DisposeAsync().ConfigureAwait(false);
                    }
                }
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync().ConfigureAwait(false);
                    await connection.DisposeAsync().ConfigureAwait(false);
                }

            }
            return objectResult;
        }
        /// <summary>
        /// Connects to the default database and execute a query which return one or more objects, 
        /// For example: SELECT * from [Table]
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryType"></param>
        /// <param name="dataHandlerAsync"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task ExecuteReaderAsync(string query, CommandType queryType, Func<SqlDataReader, Task> dataHandlerAsync, params SqlParameter[] parameters)
        {


            SqlConnection connection = new SqlConnection(this.DefaultConnectionString);

            try
            {
                await connection.OpenAsync();
                SqlCommand cmd = connection.CreateCommand();
                try
                {
                    cmd.CommandText = query;
                    cmd.CommandType = queryType;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                    try
                    {
                        await dataHandlerAsync(reader).ConfigureAwait(false);
                    }

                    finally
                    {
                        if (reader != null)
                        {
                            await reader.DisposeAsync().ConfigureAwait(false);
                        }
                    }



                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        await cmd.DisposeAsync().ConfigureAwait(false);
                    }
                }
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync().ConfigureAwait(false);
                    await connection.DisposeAsync().ConfigureAwait(false);
                }

            }


        }
        /// <summary>
        /// Specifies the connectionString and execute a query which return one or more objects, 
        /// For example: SELECT * from [Table]
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="query"></param>
        /// <param name="queryType"></param>
        /// <param name="dataHandlerAsync"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task ExecuteReaderAsync(string connectionString, string query, CommandType queryType, Func<SqlDataReader, Task> dataHandlerAsync, params SqlParameter[] parameters)
        {


            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                SqlCommand cmd = connection.CreateCommand();
                try
                {
                    cmd.CommandText = query;
                    cmd.CommandType = queryType;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                    try
                    {
                        await dataHandlerAsync(reader).ConfigureAwait(false);
                    }

                    finally
                    {
                        if (reader != null)
                        {
                            await reader.DisposeAsync().ConfigureAwait(false);
                        }
                    }



                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        await cmd.DisposeAsync().ConfigureAwait(false);
                    }
                }
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync().ConfigureAwait(false);
                    await connection.DisposeAsync().ConfigureAwait(false);
                }

            }


        }
        /// <summary>
        /// Check the connection with the database
        /// </summary>
        /// <returns>Return true if the connection works properly</returns>
        public async Task<bool>CheckConnectionAsync()
        {
            bool result = false;

            SqlConnection conn = new SqlConnection(this.DefaultConnectionString);

            try
            {
                await conn.OpenAsync();
                result = true;
            }

            finally
            {
                if (conn != null)
                {
                    await conn.DisposeAsync();
                }
            }

            return result;
        }
        /// <summary>
        ///  Create a valid SqlServer connection string
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="dbname"></param>
        /// <returns>a string which can be used to connect to the database</returns>
        public string CreateConnectionString(string serverName, string login, string password, string dbname)
        {
            SqlConnectionStringBuilder ConnStringBuilder = new SqlConnectionStringBuilder();
            ConnStringBuilder.DataSource = serverName;

            ConnStringBuilder.InitialCatalog = dbname;
            ConnStringBuilder.UserID = login;
            ConnStringBuilder.Password = password;
            ConnStringBuilder.TrustServerCertificate = true;

            return ConnStringBuilder.ToString();
        }
    }
}
