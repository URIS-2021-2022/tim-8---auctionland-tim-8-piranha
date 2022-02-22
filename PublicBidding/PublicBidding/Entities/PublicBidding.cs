using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    /// <summary>
    /// Entitet javnog nadmetanja
    /// </summary>
    public class PublicBidding
    {
        /// <summary>
        /// Id javnog nadmetanja
        /// </summary>
        [Key]
        public Guid PublicBiddingId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Id tipa javnog nadmetanja
        /// </summary>
        public Guid TypeId { get; set; }
        /// <summary>
        /// Tip javnog nadmetanja
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Id statusa javnog nadmetanja
        /// </summary>
        public Guid StatusId { get; set; }
        /// <summary>
        /// Status javnog nadmetanja
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// Vreme početka javnog nadmetanja
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Vreme kraja javnog nadmetanja
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Datum javnog nadmetanja
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
        /// <summary>
        /// Početna cena
        /// </summary>
        [Required]
        public double StartPricePerHa { get; set; }
        /// <summary>
        /// Izuzetost javnog nadmetanja
        /// </summary>
        public bool IsExcepted{ get; set; }
        /// <summary>
        /// Id adrese javnog nadmetanja
        /// </summary>
        public Guid? AddressId { get; set; }
        /// <summary>
        /// Ovlascena lica
        /// </summary>
        [NotMapped]
        public List<Guid> AuthorizedPersons { get; set; }
        /// <summary>
        /// Kupci
        /// </summary>
        [NotMapped]
        public List<Guid> Bidders { get; set; }
        /// <summary>
        /// Delovi parcela koji ucestvuju na javnom nadmetanju
        /// </summary>
        [NotMapped]
        public List<Guid> Plots { get; set; }
        /// <summary>
        /// Konačna cena
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Najbolji ponudjac
        /// </summary>
        public Guid? BestBidder { get; set; }
        /// <summary>
        /// Vreme zakupa
        /// </summary>
        public int RentPeriod { get; set; }
        /// <summary>
        /// Broj prijavljenih na javnom nadmetanju
        /// </summary>
        public int NumberOfApplicants { get; set; }
        /// <summary>
        /// Dopuna depozita javnog nadmetanja
        /// </summary>
        public double DepositSupplement { get; set; }
        /// <summary>
        /// Runda javnog nadmetanja
        /// </summary>
        public int Round { get; set; }

    }
}
