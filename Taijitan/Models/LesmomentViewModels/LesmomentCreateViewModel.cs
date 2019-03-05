
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Taijitan.Models.Domain;

namespace Taijitan.Models.LesmomentViewModels
{
    public class LesmomentCreateViewModel
    {
        // DateTime validation in JQuery

        [Required(ErrorMessage = "Datum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Starttijd is verplicht.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [DataType(DataType.Time)]
        public DateTime Starttijd { get; set; }
        [Required(ErrorMessage = "Eindtijd is verplicht.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [DataType(DataType.Time)]
        public DateTime Eindtijd { get; set; }
        public List<GebruikerVM> Gebruikers { get; set; }

        public LesmomentCreateViewModel()
        {
            Gebruikers = new List<GebruikerVM>();
            Datum = DateTime.Today;
        }

    }
}
