using PillData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PillPalApp.Services
{
    public interface IMedicationService
    {
        // CREATE
        Task<bool> CreateMedication(string name, float dose, float originalSize, string form, string addInfo, int userID);

        // READ
        Task<MedicationModel> ReadMedicationByID(int ID);
        Task<List<MedicationModel>> ReadMedByName(string name, int userID);
        Task<List<MedicationModel>> ReadMedByUserID(int userID);

        // UPDATE
        Task<bool> UpdateMedication(int ID, string name, float dose, float originalSize, string form, string addInfo, float currentSize);

        // DELETE
        Task<bool> DeleteMedication(int ID);

        // VALIDATE
        Task<bool> ValidateMedication(string name, int userID);
    }
}
