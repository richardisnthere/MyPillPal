using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PillData.Data;
using PillData.Models;
using System.Security.Policy;
using System.Xml.Linq;

namespace PillPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        IMedicationData _db = new MedicationData();

        // CRUD Operations:

        // Create:
        [Route("create/{name}/{dose}/{originalSize}/{form}/{addInfo}/{userID}")]
        [HttpPost]
        public async Task<bool> CreateMedication(string name, float dose, float originalSize, string form, string addInfo, int userID)
        {
            if (!_db.CheckMedNameAvailable(name, userID))
            {
                return false;
            }

            MedicationModel medication = new MedicationModel
            {
                name = name,
                dose = dose,
                originalSize = originalSize,
                form = form,
                addInfo = addInfo,
                currentSize = originalSize,
                userID = userID
            };

            return _db.CreateMedication(medication);
        }

        // Update:
        [Route("update/{ID}/{name}/{dose}/{originalSize}/{form}/{addInfo}/{currentSize}")]
        [HttpPut]
        public async Task<bool> UpdateMedication(int ID, string name, float dose, float originalSize, string form, string addInfo, float currentSize)
        {
            MedicationModel medication = await _db.ReadMedicationByID(ID);

            if (medication == null)
            {
                return false;
            }

            if (_db.CheckMedNameAvailable(name, medication.userID))
            {
                medication.name = name;
            }

            medication.dose = dose;
            medication.originalSize = originalSize;
            medication.form = form;
            medication.addInfo = addInfo;
            medication.currentSize = currentSize;

            return await _db.UpdateMedication(medication);
        }
        
        // Delete:
        [Route("delete/{ID}")]
        [HttpDelete]
        public async Task<bool> DeleteMedication(int ID)
        {
            MedicationModel medication = await _db.ReadMedicationByID(ID);
            if (medication == null)
            {
                return false;
            }
            return await _db.DeleteMedication(medication);
        }


        // Read:

        [Route("ReadMedicationByID/{ID}")]
        [HttpGet]
        public async Task<MedicationModel> ReadMedicationByID(int ID)
        {
            MedicationModel result = await _db.ReadMedicationByID(ID);
            return result;
        }

        [Route("ReadMedByName/{name}/{userID}")]
        [HttpGet]
        public async Task<List<MedicationModel>> ReadMedByName(string name, int userID)
        {
            List<MedicationModel> result = await _db.ReadMedByName(name, userID);
            return result;
        }

        [Route("ReadMedByUserID/{userID}")]
        [HttpGet]
        public async Task<List<MedicationModel>> ReadMedByUserID(int userID)
        {
            List<MedicationModel> result = await _db.ReadMedByUserID(userID);
            return result;
        }

        // Everything else:

        [Route("available/{name}/{userID}")]
        [HttpGet]
        public async Task<bool> ValidateMedication(string name, int userID)
        {
            return _db.CheckMedNameAvailable(name, userID);
        }
    }
}
