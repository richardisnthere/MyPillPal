using PillData.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PillPalApp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // PRIVATE HELPERS

        private async Task<UserModel> GetUser(string username)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"/api/User/search/{username}");

            if (res.IsSuccessStatusCode && res.Content.Headers.ContentLength > 0)
            {
                return await res.Content.ReadFromJsonAsync<UserModel>();
            }

            return null;
        }

        private string GenerateSalt(int saltSize = 16)
        {
            byte[] saltBytes = new byte[saltSize];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPasswordWithSalt(string pwd, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] combinedBytes = Encoding.UTF8.GetBytes(pwd + salt);
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // CREATE
        public async Task<bool> CreateUser(string username, string pwd)
        {
            bool available = await ValidateUsername(username);
            if (!available) return false;

            string salt = GenerateSalt();

            UserModel user = new UserModel
            {
                UserName = username,
                Salt = salt,
                PwdHash = HashPasswordWithSalt(pwd, salt)
            };

            try
            {
                await _httpClient.PostAsJsonAsync<UserModel>($"/api/User/create/{user.UserName}/{user.Salt}/{user.PwdHash}", user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // READ
        public async Task<UserModel> ReadUser(string username)
        {
            return await GetUser(username);
        }

        // UPDATE
        public async Task<bool> UpdateUser(string username, string newPassword)
        {
            UserModel user = await GetUser(username);
            if (user == null) return false;

            string newSalt = GenerateSalt();
            string newHash = HashPasswordWithSalt(newPassword, newSalt);

            var response = await _httpClient.PutAsync(
                $"/api/User/update/{username}/{newHash}/{newSalt}", null);

            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        // DELETE
        public async Task<bool> DeleteUser(string username)
        {
            var response = await _httpClient.DeleteAsync($"/api/User/delete/{username}");
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        // LOGIN VALIDATION
        public async Task<bool> ValidateUser(string username, string pwd)
        {
            UserModel user = await GetUser(username);
            if (user == null) { return false; }
            string p = HashPasswordWithSalt(pwd, user.Salt);

            return user.PwdHash.Equals(p);
        }

        // USERNAME AVAILABILITY
        public async Task<bool> ValidateUsername(string username)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"/api/User/available/{username}");
        }
    }
}
