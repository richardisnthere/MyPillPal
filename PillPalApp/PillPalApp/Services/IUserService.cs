using PillData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalApp.Services
{
    public interface IUserService
    {
        // CREATE
        Task<bool> CreateUser(string username, string pwd);

        // READ
        Task<UserModel> ReadUser(string username);

        // UPDATE
        Task<bool> UpdateUser(string username, string newPassword);

        // DELETE
        Task<bool> DeleteUser(string username);

        // VALIDATION
        Task<bool> ValidateUser(string username, string pwd);
        Task<bool> ValidateUsername(string username);
    }
}
