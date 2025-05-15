using Microsoft.Maui.ApplicationModel;
using PillData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace PillPalApp.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly HttpClient _httpClient;

        public MedicationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<bool> CreateMedication(string name, float dose, float originalSize, string form, string addInfo, int userID)
        {
            var response = await _httpClient.PostAsync($"api/medication/create/{name}/{dose}/{originalSize}/{form}/{addInfo}/{userID}", null);
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        // READ
        public async Task<MedicationModel> ReadMedicationByID(int ID)
        {
            return await _httpClient.GetFromJsonAsync<MedicationModel>($"api/medication/ReadMedicationByID/{ID}");
        }

        public async Task<List<MedicationModel>> ReadMedByName(string name, int userID)
        {
            return await _httpClient.GetFromJsonAsync<List<MedicationModel>>($"api/medication/ReadMedByName/{name}/{userID}");
        }

        public async Task<List<MedicationModel>> ReadMedByUserID(int userID)
        {
            return await _httpClient.GetFromJsonAsync<List<MedicationModel>>($"api/medication/ReadMedByUserID/{userID}");
        }

        // UPDATE
        public async Task<bool> UpdateMedication(int ID, string name, float dose, float originalSize, string form, string addInfo, float currentSize)
        {
            var response = await _httpClient.PutAsync($"api/medication/update/{ID}/{name}/{dose}/{originalSize}/{form}/{addInfo}/{currentSize}", null);
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        // DELETE
        public async Task<bool> DeleteMedication(int ID)
        {
            var response = await _httpClient.DeleteAsync($"api/medication/delete/{ID}");
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }

        // VALIDATE
        public async Task<bool> ValidateMedication(string name, int userID)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"api/medication/available/{name}/{userID}");
        }
    }
}
