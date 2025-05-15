using PillData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillData.Data
{
    public class UserData : IUserData
    {

        private readonly ISqlDataAccess _db = new SqlDataAccess();

        public bool CheckUserNameAvailable(string userName)
        {
            bool response = true;
            string sql = "SELECT username FROM users WHERE username = @userName";
            List<UserModel> result = _db.LoadData<UserModel, dynamic>(sql, new { userName });
            if (result.Count > 0)
            {
                response = false;
            }
            return response;
        }

        public bool CreateUser(UserModel u)
        {
            if (!CheckUserNameAvailable(u.UserName))
            {
                return false;
            }
            string sql = "INSERT INTO users (username, pwdhash, salt) VALUES (@UserName, @PwdHash, @Salt);";
            _db.SaveData(sql, u);
            return true;
        }

        public UserModel ReadUser(string uName)
        {
            string sql = "SELECT * FROM users WHERE username = @uName";
            List<UserModel> result = _db.LoadData<UserModel, dynamic>(sql, new { uName });
            if (result.Count > 0)
            {
                return result[0];
            }
            return null;
        }

        public async Task<bool> UpdateUser(UserModel u)
        {
            string sql = "UPDATE pillpal.users SET username = @UserName, pwdhash = @PwdHash, salt = @Salt " +
                "WHERE ID = @ID;";

            try
            {
                _db.SaveData(sql, u);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(UserModel u)
        {
            string sql = "DELETE FROM pillpal.users WHERE ID = @ID;";

            try
            {
                _db.SaveData(sql, u);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
