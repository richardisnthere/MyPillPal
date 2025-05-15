using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillData.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private string connectionString = "server=localhost; port=3306; uid=TheaterUser; pwd=AppDevRules!; database=pillpal";

        public List<T> LoadData<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var data = connection.Query<T>(sql, parameters);
                return data.ToList();
            }
        }

        public void SaveData<T>(string sql, T parameters)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }
    }
}
