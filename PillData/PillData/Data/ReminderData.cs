using PillData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PillData.Data
{
    public class ReminderData : IReminderData
    {
        private readonly ISqlDataAccess _db = new SqlDataAccess();

        public List<ReminderModel> ReadAllReminder()
        {
            string sql = "SELECT * FROM pillpal.reminder;";

            IEnumerable<ReminderModel> result = _db.LoadData<ReminderModel, dynamic>(sql, new { });
            List<ReminderModel> allReminders = result.ToList();
            if (allReminders.Count > 0) { return allReminders; }
            return null;
        }

        // For CRUD test: public ReminderModel ReadReminderByID(int ID)
        // Update once API: public async Task<ReminderModel> ReadReminderByID(int ID)
        public async Task<ReminderModel> ReadReminderByID(int ID)
        {
            string sql = "SELECT * FROM pillpal.reminder WHERE reminder.ID = @ID";

            IEnumerable<ReminderModel> result = _db.LoadData<ReminderModel, dynamic>(sql, new { ID });
            List<ReminderModel> reminder = result.ToList();
            if (reminder.Count > 0)
            {
                return reminder[0];
            }
            return null;
        }
        public bool CheckRemAvailable(DateTime frequency, TimeSpan time, int medID)
        {
            bool response = true;
            string sql = "SELECT reminder.frequency " +
                "FROM reminder " +
                "WHERE DATE(frequency) = DATE(@frequency) AND TIME_FORMAT(time, '%H:%i:%s') = TIME_FORMAT(@time, '%H:%i:%s') " +
                "AND reminder.medID = @medID;";
            List<ReminderModel> result = _db.LoadData<ReminderModel, dynamic>(sql, new { frequency, time, medID });
            if (result.Count > 0)
            {
                response = false;
            }
            return response;
        }

        public bool CreateReminder(ReminderModel reminder)
        {
            if (!CheckRemAvailable(reminder.frequency, reminder.time, reminder.medID))
            {
                return false;
            }

            string sql = "INSERT INTO pillpal.reminder (frequency, time, medID, userID) VALUES (@frequency, @time, @medID, @userID);";
            _db.SaveData(sql, reminder);
            return true;
        }

        public async Task<bool> UpdateReminder(ReminderModel reminder)
        {
            string sql = "UPDATE pillpal.reminder SET frequency = @frequency, time = @time, medID = @medID, userID = @userID " +
                "WHERE ID = @ID;";

            try
            {
                _db.SaveData(sql, reminder);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteReminder(ReminderModel reminder)
        {
            string sql = "DELETE FROM pillpal.reminder WHERE ID = @ID;";

            try
            {
                _db.SaveData(sql, reminder);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // For API:
        // Need: ReadRemByWeek, ReadRemByDay, ReadRemByMedID, ReadRemByUserID

        public async Task<List<ReminderModel>> ReadRemByWeek(int medID)
        {
            DateTime today = DateTime.Today;
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            string sql = "SELECT reminder.ID, reminder.frequency, reminder.time, reminder.medID, reminder.userID " +
                "FROM reminder " +
                "WHERE reminder.frequency " +
                "BETWEEN @startOfWeek AND @endOfWeek " +
                "AND reminder.medID = @medID " +
                "ORDER BY reminder.frequency ASC, reminder.time ASC;";

            List<ReminderModel> result = _db.LoadData<ReminderModel, dynamic>(sql, new { startOfWeek, endOfWeek, medID });
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public async Task<List<ReminderModel>> ReadRemByDay(int medID)
        {
            DateTime today = DateTime.Today;

            string sql = "SELECT reminder.ID, reminder.frequency, reminder.time, reminder.medID, reminder.userID " +
                "FROM reminder " +
                "WHERE DATE(frequency) = DATE(@today) " +
                "AND reminder.medID = @medID " +
                "ORDER BY reminder.time ASC;";


            List<ReminderModel> result = _db.LoadData<ReminderModel, dynamic>(sql, new { today, medID });
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public async Task<List<ReminderModel>> ReadRemByMedID(int medID)
        {
            string sql = "SELECT reminder.ID, reminder.frequency, reminder.time, reminder.medID, reminder.userID " +
                "FROM reminder " +
                "WHERE reminder.medID = @medID " +
                "ORDER BY reminder.frequency ASC, reminder.time ASC;";

            List<ReminderModel> result = _db.LoadData<ReminderModel, dynamic>(sql, new { medID });
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public async Task<List<ReminderModel>> ReadRemByUserID(int userID)
        {
            string sql = "SELECT reminder.ID, reminder.frequency, reminder.time, reminder.medID, reminder.userID " +
                "FROM reminder " +
                "WHERE reminder.userID = @userID " +
                "ORDER BY reminder.frequency ASC, reminder.time ASC;";

            List<ReminderModel> result = _db.LoadData<ReminderModel, dynamic>(sql, new { userID });
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }
    }
}
