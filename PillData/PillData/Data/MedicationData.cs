using Mysqlx.Crud;
using PillData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillData.Data
{
    public class MedicationData : IMedicationData
    {
        private readonly ISqlDataAccess _db = new SqlDataAccess();

        public List<MedicationModel> ReadAllMedication()
        {
            string sql = "SELECT * FROM pillpal.medication;";

            IEnumerable<MedicationModel> result = _db.LoadData<MedicationModel, dynamic>(sql, new { });
            List<MedicationModel> allMedications = result.ToList();
            if (allMedications.Count > 0) { return allMedications; }
            return null;
        }

        // For CRUD test: public MedicationModel ReadMedicationByID(int ID)
        // Update once API: public async Task<MedicationModel> ReadMedicationByID(int ID)
        public async Task<MedicationModel> ReadMedicationByID(int ID)
        {
            string sql = "SELECT * FROM pillpal.medication WHERE medication.ID = @ID";
            
            IEnumerable<MedicationModel> result = _db.LoadData<MedicationModel, dynamic>(sql, new { ID });
            List<MedicationModel> medication = result.ToList();
            if (medication.Count > 0)
            {
                return medication[0];
            }
            return null;
        }

        public bool CheckMedNameAvailable(string name, int userID)
        {
            bool response = true;
            string sql = "SELECT medication.name " +
                "FROM medication " +
                "WHERE name = @name AND userID = @userID;";
            List<MedicationModel> result = _db.LoadData<MedicationModel, dynamic>(sql, new { name, userID });
            if (result.Count > 0)
            {
                response = false;
            }
            return response;
        }

        public bool CreateMedication(MedicationModel medication)
        {
            if (!CheckMedNameAvailable(medication.name, medication.userID))
            {
                return false;
            }

            string sql = "INSERT INTO pillpal.medication (name, dose, originalSize, form, addInfo, currentSize, userID) VALUES (@name, @dose, @originalSize, @form, @addInfo, @currentSize, @userID);";
            _db.SaveData(sql, medication);
            return true;
        }

        public async Task<bool> UpdateMedication(MedicationModel medication)
        {
            string sql = "UPDATE pillpal.medication SET name = @name, dose = @dose, originalSize = @originalSize, form = @form, addInfo = @addInfo, currentSize = @currentSize, userID = @userID " +
                "WHERE ID = @ID;";

            try
            {
                _db.SaveData(sql, medication);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMedication(MedicationModel medication)
        {
            string sql = "DELETE FROM pillpal.medication WHERE ID = @ID;";

            try
            {
                _db.SaveData(sql, medication);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // For API:
        // Need: ReadMedByName, ReadMedByUserID

        public async Task<List<MedicationModel>> ReadMedByName(string name, int userID)
        {
            string sql = "SELECT medication.ID, medication.name, medication.dose, medication.originalSize, medication.form, medication.addInfo, medication.currentSize, medication.userID " +
                "FROM medication " +
                "WHERE medication.name LIKE CONCAT('%', @name ,'%') " +
                "AND medication.userID = @userID " +
                "ORDER BY medication.name ASC;";

            List<MedicationModel> result = _db.LoadData<MedicationModel, dynamic>(sql, new { name, userID });
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public async Task<List<MedicationModel>> ReadMedByUserID(int userID)
        {
            string sql = "SELECT medication.ID, medication.name, medication.dose, medication.originalSize, medication.form, medication.addInfo, medication.currentSize, medication.userID " +
                "FROM medication " +
                "WHERE medication.userID = @userID " +
                "ORDER BY medication.name ASC;";

            List<MedicationModel> result = _db.LoadData<MedicationModel, dynamic>(sql, new { userID });
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }
    }
}
