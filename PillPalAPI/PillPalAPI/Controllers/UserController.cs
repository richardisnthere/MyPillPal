using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Digests;
using PillData.Data;
using PillData.Models;

namespace PillPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserData _db = new UserData();

        // CRUD Operations:

        // Create:
        [Route("create/{username}/{salt}/{hash}")]
        [HttpPost]
        public async Task<bool> CreateUser(string username, string hash, string salt)
        {
            if (!_db.CheckUserNameAvailable(username))
            {
                return false;
            }

            UserModel user = new UserModel
            {
                UserName = username,
                Salt = salt,
                PwdHash = hash
            };

            return _db.CreateUser(user);
        }

        // Read:
        [Route("search/{username}")]
        [HttpGet]
        public async Task<UserModel> ReadUser(string username)
        {
            UserModel result = _db.ReadUser(username);
            return result;
        }

        // Update:
        [Route("update/{username}/{hash}/{salt}")]
        [HttpPut]
        public async Task<bool> UpdateUser(string username, string hash, string salt)
        {
            UserModel user = _db.ReadUser(username);
            if (user == null)
            {
                return false;
            }
            user.Salt = salt;
            user.PwdHash = hash;
           
            return await _db.UpdateUser(user);
        }

        // Delete:
        [Route("delete/{username}")]
        [HttpDelete]
        public async Task<bool> DeleteUser(string username)
        {
            UserModel user = _db.ReadUser(username);
            if (user == null)
            {
                return false;
            }
            return await _db.DeleteUser(user);
        }

        // Everything else:

        [Route("available/{username}")]
        [HttpGet]
        public async Task<bool> ValidateUser(string username)
        {
            return _db.CheckUserNameAvailable(username);
        }

        
    }
}
