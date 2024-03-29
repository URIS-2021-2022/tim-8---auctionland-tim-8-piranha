﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Models
{
    /// <summary>
    /// Dto za izmenu javnog nadmetanja
    /// </summary>
    public class PublicBiddingUpdateDto : IValidatableObject
    {
        /// <summary>
        /// Id javnog nadmetanja
        /// </summary>
        public Guid PublicBiddingId { get; set; }
        /// <summary>
        /// Pocetna cena po hektaru
        /// </summary>
        [Required(ErrorMessage = "Početna cena po hektaru je obavezna.")]
        public double StartPricePerHa { get; set; }
        /// <summary>
        /// Vremenski period zakupa
        /// </summary>
        [Required(ErrorMessage = "Period vremena na koje se deo parcele zakupljuje je obavezno uneti.")]
        public int PeriodZakupa { get; set; }
        /// <summary>
        /// Izlicitirana cena
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Broj prijavljenih lica na javnom nadmetanju
        /// </summary>
        public int NumberOfApplicants { get; set; }
        /// <summary>
        /// Broju kruga
        /// </summary>
        public int Round { get; set; }
        /// <summary>
        /// Pokazuje da li je javno nadmetanje izuzeto
        /// </summary>
        public bool IsExcepted { get; set; }
        /// <summary>
        /// ID statusa javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Id statusa javnog nadmetanja je obavezno uneti.")]
        public Guid StatusId { get; set; }
        /// <summary>
        /// ID tipa javnog nadmetanja
        /// </summary>
        [Required(ErrorMessage = "Id javnog nadmetanja je obavezno obeležje.")]
        public Guid TypeId { get; set; }
        /// <summary>
        /// ID kupca koji je ponudio najveću cenu
        /// </summary>
        public Guid? BuyerId { get; set; }
        /// <summary>
        /// ID adrese na kom se održava javno nadmetanje
        /// </summary>
        public Guid? AddressId { get; set; }
        /// <summary>
        /// Lista ID-eva ovlašćenih lica
        /// </summary>
        public List<Guid>? AuthorizedPersons { get; set; }
        /// <summary>
        /// Lista ID-eva kupaca
        /// </summary>
        public List<Guid>? Bidders { get; set; }
        /// <summary>
        /// Lista ID-eva delova parcele
        /// </summary>
        public List<Guid>? PlotParts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price < StartPricePerHa || Price < 0)
            {
                yield return new ValidationResult("Konačna cena mora biti veća od nule i manja od početne cene po hektaru!", new[] { "PublicBiddingCreationDto" });
            }
        }
    }
}
