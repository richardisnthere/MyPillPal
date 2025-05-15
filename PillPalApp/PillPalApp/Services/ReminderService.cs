using PillData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PillPalApp.Services
{
    public class ReminderService : IReminderService
    {
        private readonly HttpClient _httpClient;

        public ReminderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CreateReminder(DateTime frequency, TimeSpan time, int medID, int userID)
        {
            var response = await _httpClient.PostAsync(
                $"api/reminder/create/{frequency:o}/{time}/{medID}/{userID}", null);
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        // READ
        public async Task<ReminderModel> ReadReminderByID(int ID)
        {
            return await _httpClient.GetFromJsonAsync<ReminderModel>($"api/reminder/ReadReminderByID/{ID}");
        }

        public async Task<List<ReminderModel>> ReadRemByWeek(int medID)
        {
            return await _httpClient.GetFromJsonAsync<List<ReminderModel>>($"api/reminder/ReadRemByWeek/{medID}");
        }

        public async Task<List<ReminderModel>> ReadRemByDay(int medID)
        {
            return await _httpClient.GetFromJsonAsync<List<ReminderModel>>($"api/reminder/ReadRemByDay/{medID}");
        }

        public async Task<List<ReminderModel>> ReadRemByMedID(int medID)
        {
            return await _httpClient.GetFromJsonAsync<List<ReminderModel>>($"api/reminder/ReadRemByMedID/{medID}");
        }

        public async Task<List<ReminderModel>> ReadRemByUserID(int userID)
        {
            return await _httpClient.GetFromJsonAsync<List<ReminderModel>>($"api/reminder/ReadRemByUserID/{userID}");
        }

        // UPDATE
        public async Task<bool> UpdateReminder(int ID, DateTime frequency, TimeSpan time)
        {
            var response = await _httpClient.PutAsync(
                $"api/reminder/update/{ID}/{frequency:o}/{time}", null);
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        // DELETE
        public async Task<bool> DeleteReminder(int ID)
        {
            var response = await _httpClient.DeleteAsync($"api/reminder/delete/{ID}");
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        // VALIDATE
        public async Task<bool> ValidateReminder(DateTime frequency, TimeSpan time, int medID)
        {
            return await _httpClient.GetFromJsonAsync<bool>(
                $"api/reminder/available/{frequency:o}/{time}/{medID}");
        }
    }
}
