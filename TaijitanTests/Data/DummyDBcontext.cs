using System;
using System.Collections.Generic;
using Taijitan.Models.Domain;
using Taijitan.Models.LesmomentViewModels;

namespace TaijitanTests.Data
{
    public class DummyDBcontext
    {

        public List<Gebruiker> GeenGebruikers { get { return new List<Gebruiker>(); } }
        public List<Gebruiker> Lijst1Gebruiker { get; }
        public List<GebruikerVM> Lijst1GebruikerVM { get; }


        public DummyDBcontext()
        {
            Gebruiker gebruiker1 = new Gebruiker("username1", "256321-12569", DateTime.Now, "name1", "first name1", Taijitan.Models.Domain.Enums.Geslacht.Man, DateTime.Now, "Gent", "00712345678", "0236587496", "something@some.th", "somet@som.th", new Adres("België", "9000", "Gent", "Voskenslaan", "1"), 100, new Gradatie(1, "", "name"), Taijitan.Models.Domain.Enums.TypeGebruiker.Lid);

            Lijst1Gebruiker = new List<Gebruiker>();
            Lijst1Gebruiker.Add(gebruiker1);

            Lijst1GebruikerVM = new List<GebruikerVM>();
            Lijst1GebruikerVM.Add(new GebruikerVM(gebruiker1));
        }
    }
}
