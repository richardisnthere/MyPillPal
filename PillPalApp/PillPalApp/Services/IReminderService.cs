using PillData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalApp.Services
{
    public interface IReminderService
    {
        // CREATE
        Task<bool> CreateReminder(DateTime frequency, TimeSpan time, int medID, int userID);

        // READ
        Task<ReminderModel> ReadReminderByID(int ID);
        Task<List<ReminderModel>> ReadRemByWeek(int medID);
        Task<List<ReminderModel>> ReadRemByDay(int medID);
        Task<List<ReminderModel>> ReadRemByMedID(int medID);
        Task<List<ReminderModel>> ReadRemByUserID(int userID);

        // UPDATE
        Task<bool> UpdateReminder(int ID, DateTime frequency, TimeSpan time);

        // DELETE
        Task<bool> DeleteReminder(int ID);

        // VALIDATE
        Task<bool> ValidateReminder(DateTime frequency, TimeSpan time, int medID);
    }
}
