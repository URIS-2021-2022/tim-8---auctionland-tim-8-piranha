using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    /// <summary>
    /// User DTO - Model za korisnika sistema
    /// </summary>
    public class UserDto
    {

            /// <summary>
            /// Ime korisnika sistema
            /// Example: Dimitrje
            /// </summary>
            public string nameU { get; set; }

            /// <summary>
            /// Prezime korisnika sistema
            /// Example: Corlija
            /// </summary>
            public string surnameU { get; set; }

            /// <summary>
            /// Korisnicko ime korisnika sistema
            /// Example: dimitrije.corlija
            /// </summary>
            public string username { get; set; }
            /// <summary>
            /// Lozinka korisnika sistema
            /// Example: dimitrije123
            /// </summary>
            public string password { get; set; }
        
    }
}
