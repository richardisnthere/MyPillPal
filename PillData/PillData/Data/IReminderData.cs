using PillData.Models;

namespace PillData.Data
{
    public interface IReminderData
    {
        List<ReminderModel> ReadAllReminder();

        // For CRUD test: ReminderModel ReadReminderByID(int ID);
        // Update once API: Task<ReminderModel> ReadReminderByID(int ID);
        Task<ReminderModel> ReadReminderByID(int ID);

        bool CheckRemAvailable(DateTime frequency, TimeSpan time, int medID);
        bool CreateReminder(ReminderModel reminder);
        Task<bool> UpdateReminder(ReminderModel reminder);
        Task<bool> DeleteReminder(ReminderModel reminder);

        // For API:
        Task<List<ReminderModel>> ReadRemByWeek(int medID);
        Task<List<ReminderModel>> ReadRemByDay(int medID);
        Task<List<ReminderModel>> ReadRemByMedID(int medID);
        Task<List<ReminderModel>> ReadRemByUserID(int userID);
    }
}