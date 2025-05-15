using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillData.Models
{
    public class ReminderModel
    {
        public int ID { get; set; }
        public DateTime frequency { get; set; }
        public TimeSpan time {  get; set; }
        public int medID { get; set; }
        public int userID { get; set; }

        public override string ToString()
        {
            string output = $"Reminder ID: {ID}\n" +
                $"Frequency: {frequency:yyyy-MM-dd}\n" +
                $"Time: {time}\n" +
                $"Medication ID: {medID}\n" +
                $"User ID: {userID}\n";

            return output;
        }
    }
}
