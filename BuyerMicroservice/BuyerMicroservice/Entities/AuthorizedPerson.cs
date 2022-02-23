using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyerMicroservice.Entities
{
<<<<<<< HEAD
    /// <summary>
    /// Authorized person model
    /// </summary>
    public class AuthorizedPerson
    {
        /// <summary>
        /// Authorized person ID ID - ID OVLASCENOG LICA
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        [Key]
        public Guid authorizedPersonID { get; set; }
        /// <summary>
        /// Name - ovlascenog lica
        /// Example : Dimitrije
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Surename - prezime ovlascenog lica
        /// Example : Corlija
        /// </summary>
        public string surname { get; set; }
        /// <summary>
        /// personalDocNum - Broj licnog dokumenta
        /// Example : 8767834637274
        /// </summary>
        public string personalDocNum { get; set; }
        /// <summary>
        /// Address- adresa ovlascenog lica
        /// Example : Partizanski put 10
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Country - zemlja stanovanja ovlascenog lica 
        /// Example : Srbija
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// board Number ID(Foreign key) from boardNumber entity - ID broja table
        /// Example : 861f142c-4707-416d-ad14-7debbd2031ed
        /// </summary>
        [ForeignKey("BoardNumber")]
        public Guid boardNumbID { get; set; }
        /// <summary>
        /// board - enity board number
        /// </summary>
        public BoardNumber board { get; set; }
        /// <summary>
        /// Buyers- lista kupaca
        /// </summary>
=======
    public class AuthorizedPerson
    {
        [Key]
        public Guid authorizedPersonID { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string personalDocNum { get; set; }

        public string address { get; set; }
        //lice za koje se vrsi aukcija 
        // ne treba foreign key zato sto strelica pokazuje u kontra smeru 
        public string country { get; set; }

        [ForeignKey("BoardNumber")]
        public Guid boardNumbID { get; set; }
        public BoardNumber board { get; set; }

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
        public ICollection<Buyer> buyers { get; set; }

        
    }
}
