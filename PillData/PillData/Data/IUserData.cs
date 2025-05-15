using PillData.Models;

namespace PillData.Data
{
    public interface IUserData
    {
        bool CheckUserNameAvailable(string userName);
        bool CreateUser(UserModel u);
        UserModel ReadUser(string uName);
        Task<bool> UpdateUser(UserModel u);
        Task<bool> DeleteUser(UserModel u);
    }
}