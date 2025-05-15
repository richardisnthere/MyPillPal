using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PillData.Data;
using PillData.Models;

namespace PillPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        IReminderData _db = new ReminderData();

        // Create:
        [Route("create/{frequency}/{time}/{medID}/{userID}")]
        [HttpPost]
        public async Task<bool> CreateReminder(DateTime frequency, TimeSpan time, int medID, int userID)
        {
            if (!_db.CheckRemAvailable(frequency, time, medID))
            {
                return false;
            }

            ReminderModel reminder = new ReminderModel
            {
                frequency = frequency,
                time = time,
                medID = medID,
                userID = userID
            };

            return _db.CreateReminder(reminder);
        }

        // Update:
        [Route("update/{ID}/{frequency}/{time}")]
        [HttpPut]
        public async Task<bool> UpdateReminder(int ID, DateTime frequency, TimeSpan time)
        {
            ReminderModel reminder = await _db.ReadReminderByID(ID);

            if ((reminder == null) || (!_db.CheckRemAvailable(frequency, time, reminder.medID)))
            {
                return false;
            }

            reminder.frequency = frequency;
            reminder.time = time;

            return await _db.UpdateReminder(reminder);
        }

        // Delete:
        [Route("delete/{ID}")]
        [HttpDelete]
        public async Task<bool> DeleteMedication(int ID)
        {
            ReminderModel reminder = await _db.ReadReminderByID(ID);
            if (reminder == null)
            {
                return false;
            }
            return await _db.DeleteReminder(reminder);
        }

        // Read:

        [Route("ReadReminderByID/{ID}")]
        [HttpGet]
        public async Task<ReminderModel> ReadReminderByID(int ID)
        {
            ReminderModel result = await _db.ReadReminderByID(ID);
            return result;
        }

        [Route("ReadRemByWeek/{medID}")]
        [HttpGet]
        public async Task<List<ReminderModel>> ReadRemByWeek(int medID)
        {
            List<ReminderModel> result = await _db.ReadRemByWeek(medID);
            return result;
        }

        [Route("ReadRemByDay/{medID}")]
        [HttpGet]
        public async Task<List<ReminderModel>> ReadRemByDay(int medID)
        {
            List<ReminderModel> result = await _db.ReadRemByDay(medID);
            return result;
        }

        [Route("ReadRemByMedID/{medID}")]
        [HttpGet]
        public async Task<List<ReminderModel>> ReadRemByMedID(int medID)
        {
            List<ReminderModel> result = await _db.ReadRemByMedID(medID);
            return result;
        }

        [Route("ReadRemByUserID/{userID}")]
        [HttpGet]
        public async Task<List<ReminderModel>> ReadRemByUserID(int userID)
        {
            List<ReminderModel> result = await _db.ReadRemByUserID(userID);
            return result;
        }

        // Everything else:

        [Route("available/{frequency}/{time}/{medID}")]
        [HttpGet]
        public async Task<bool> ValidateReminder(DateTime frequency, TimeSpan time, int medID)
        {
            return _db.CheckRemAvailable(frequency, time, medID);
        }

    }
}
