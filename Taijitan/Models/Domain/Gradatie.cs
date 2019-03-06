using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class Gradatie
    {
        public int GradatieId { get; private set; }
        // Graad in respectieve kyu/dan
        public int Graadnummer { get; private set; }
        // Kyu/Dan
        public String Onderverdeling { get; private set; }
        // Rang-kyu/dan: vb=" Rokku-kyu , Sho-dan "
        public String Naam { get; private set; }

        public Gradatie (int graadnummer, String onderverdeling, String naam)
        {
            this.Graadnummer = graadnummer;
            this.Onderverdeling = onderverdeling;
            this.Naam = naam;
        }
        public Gradatie() {

        }
    }
}
