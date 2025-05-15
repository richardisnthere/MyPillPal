using PillData.Data;
using PillData.Models;

namespace PillData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IMedicationData _db = new MedicationData();
            //IReminderData _db = new ReminderData();

            // C.R.U.D Testing

            // Medication:

            // Test 1 - Read All
            //List<MedicationModel> medications = _db.ReadAllMedication();
            //foreach (MedicationModel m in medications)
            //{
            //    Console.WriteLine(m);
            //}

            // Read by ID
            //MedicationModel medication = _db.ReadMedicationByID(1);
            //Console.WriteLine(medication);

            // Test 2 - Create
            //MedicationModel testMedication = new MedicationModel();

            //testMedication.ID = 3;
            //testMedication.name = "test1";
            //testMedication.dose = 3;
            //testMedication.originalSize = 3;
            //testMedication.form = "liquid";
            //testMedication.addInfo = "Green";
            //testMedication.currentSize = 3;
            //testMedication.userID = 1;

            //_db.CreateMedication(testMedication);
            //Console.WriteLine(testMedication);

            // Test 3 - Update
            //MedicationModel testMedication = _db.ReadMedicationByID(3);

            //testMedication.ID = 3;
            //testMedication.name = "update";
            //testMedication.dose = 3;
            //testMedication.originalSize = 3;
            //testMedication.form = "update";
            //testMedication.addInfo = "update";
            //testMedication.currentSize = 3;
            //testMedication.userID = 1;

            //_db.UpdateMedication(testMedication);
            //Console.WriteLine(testMedication);

            // Test 4 - Delete
            //MedicationModel testMedication = _db.ReadMedicationByID(3);
            //_db.DeleteMedication(3);

            //List<MedicationModel> medications = _db.ReadAllMedication();
            //foreach (MedicationModel m in medications)
            //{
            //    Console.WriteLine(m);
            //}

            //-----------------------------------------------------------------

            // Reminder:

            //Test 1 - Read All
            //List<ReminderModel> reminders = _db.ReadAllReminder();
            //foreach (ReminderModel r in reminders)
            //{
            //    Console.WriteLine(r);
            //}

            // Read by ID
            //ReminderModel reminder = _db.ReadReminderByID(1);
            //Console.WriteLine(reminder);

            // Test 2 - Create
            //ReminderModel testReminder = new ReminderModel();

            //testReminder.ID = 3;
            //testReminder.frequency = DateTime.Parse("2025/04/29");
            //testReminder.time = TimeSpan.Parse("13:00:00");
            //testReminder.medID = 2;
            //testReminder.userID = 2;

            //_db.CreateReminder(testReminder);
            //Console.WriteLine(testReminder);

            // Test 3 - Update
            //ReminderModel testReminder = _db.ReadReminderByID(3);

            //testReminder.ID = 3;
            //testReminder.frequency = DateTime.Parse("2025/04/27");
            //testReminder.time = TimeSpan.Parse("01:00:00");
            //testReminder.medID = 2;
            //testReminder.userID = 2;

            //_db.UpdateReminder(testReminder);
            //Console.WriteLine(testReminder);

            // Test 4 - Delete
            //ReminderModel testReminder = _db.ReadReminderByID(3);
            //_db.DeleteReminder(3);

            //List<ReminderModel> reminders = _db.ReadAllReminder();
            //foreach (ReminderModel r in reminders)
            //{
            //    Console.WriteLine(r);
            //}
        }
    }
}