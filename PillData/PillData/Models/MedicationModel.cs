using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillData.Models
{
    public class MedicationModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public float dose { get; set; }
        public float originalSize { get; set; }
        public string form { get; set; }
        public string addInfo { get; set; }
        public float currentSize { get; set; }
        public int userID { get; set; }

        public override string ToString()
        {
            string output = $"Medication ID: {ID}\n" +
                $"Name: {name}\n" +
                $"Dose: {dose}\n" +
                $"Original Size: {originalSize}\n" +
                $"Form: {form}\n" +
                $"Additional Info: {addInfo}\n" +
                $"Current Size: {currentSize}\n" +
                $"User ID: {userID}\n";

            return output;
        }
    }
}
