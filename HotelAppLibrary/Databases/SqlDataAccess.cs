using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace HotelAppLibrary.Databases
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        // isStoredProcedure is a flag to tell the method if the sqlStatment is a stored procedure or not. SQL Lite doesn't support stored procedures
        // Detail Summary of the method.
        public List<T> LoadData<T, U>(string sqlStatment,
                                       U parameters,
                                       string connectionStringName,
                                       bool isStoredProcedure = false )
        {
            string connectionString = _config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatment, parameters, commandType: commandType).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatment,
                                T parameters,
                                string connectionStringName,
                                bool isStoredProcedure = false )

        {
            string connectionString = _config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatment, parameters, commandType: commandType);
            }
        }

    }
}
