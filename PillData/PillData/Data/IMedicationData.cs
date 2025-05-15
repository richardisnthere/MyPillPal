using PillData.Models;

namespace PillData.Data
{
    public interface IMedicationData
    {
        List<MedicationModel> ReadAllMedication();

        // For CRUD test: MedicationModel ReadMedicationByID(int ID);
        // Update once API: Task<MedicationModel> ReadMedicationByID(int ID);
        Task<MedicationModel> ReadMedicationByID(int ID);
        bool CheckMedNameAvailable(string name, int userID);
        bool CreateMedication(MedicationModel medication);
        Task<bool> UpdateMedication(MedicationModel medication);
        Task<bool> DeleteMedication(MedicationModel medication);

        // For API:
        Task<List<MedicationModel>> ReadMedByName(string name, int userID);
        Task<List<MedicationModel>> ReadMedByUserID(int userID);

    }
}