using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    /// <summary>
    /// Model za korisnika sistema
    /// </summary>
    public class UserDto
    {
       
        

            /// <summary>
            /// Ime korisnika sistema
            /// </summary>
            public string nameU { get; set; }

            /// <summary>
            /// Prezime korisnika sistema
            /// </summary>
            public string surnameU { get; set; }

            /// <summary>
            /// Korisnicko ime korisnika sistema
            /// </summary>
            public string username { get; set; }
            /// <summary>
            /// Lozinka korisnika sistema
            /// </summary>
            public string password { get; set; }
        
    }
}
