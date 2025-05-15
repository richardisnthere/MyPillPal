using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillData.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PwdHash { get; set; }
        public string Salt { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Username: {UserName}";
        }
    }
}
