using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        string fullName = string.Empty;

        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = FirstName + " " + LastName;
            }
        }
    }
}
            
    

