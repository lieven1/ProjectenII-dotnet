using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Taijitan.Models.Domain
{        
    public class Lesformule
    {
        public int Id { get; private set; }
        [NotMapped]
        public List<DayOfWeek> Days { get; private set; }
        public string TitleText { get; private set; }
        public string LesText { get; private set; }

        public Lesformule(List<DayOfWeek> days, string titleText, string lesText)
        {
            Days = days;
            TitleText = titleText;
            LesText = lesText;
        }

        public Lesformule(string titleText, string lesText) : this(new List<DayOfWeek>(), titleText, lesText) { }

        public Lesformule()
        {

        }

    }
    
}
