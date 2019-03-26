using System;
using System.Collections.Generic;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Databindings;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.GebruikerViewModels;

namespace TaijitanTests.Data
{
    public class DummyDBcontext
    {

        public List<Gebruiker> GeenGebruikers { get { return new List<Gebruiker>(); } }
        public List<Gebruiker> Lijst1Gebruiker { get; }
        public List<GebruikerEditViewModel> Lijst1GebruikerVM { get; }
        public Gebruiker GebruikerNietInLijst { get; }
        public Gebruiker GebruikerInLijst { get; }
        public Lesmoment LesmomentValid { get; }
        public Lesmoment LesmomentActief { get; }
        public Gebruiker GebruikerInLesmomentLedenVanLesmomentValid { get; }
        public List<Lesmoment> Lesmomenten { get; }
        public List<Lesmoment> GeenLesmomenten { get { return new List<Lesmoment>(); } }
        public List<Thema> Themas { get; }
        public List<Lesmateriaal> Lesmateriaal { get; }
        public List<Lesformule> lesformules { get; }

        public DummyDBcontext()
        {
            // lesformules
            Lesformule lesformule1 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Wednesday }, "Woensdag", "Ik volg normaal les op woensdag.");
            Lesformule lesformule2 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Saturday }, "Zaterdag", "Ik volg normaal les op zaterdag.");
            Lesformule lesformule3 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Tuesday }, "Dinsdag", "Ik volg normaal les op dinsdag.");
            Lesformule lesformule4 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Wednesday, DayOfWeek.Saturday }, "Woensdag en zaterdag", "Ik volg normaal les op woensdag en zaterdag.");
            Lesformule lesformule5 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Tuesday, DayOfWeek.Saturday }, "Dinsdag en zaterdag", "Ik volg normaal les op dinsdag en zaterdag.");
            Lesformule lesformule6 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Tuesday, DayOfWeek.Thursday }, "Dinsdag en donderdag", "Ik volg normaal les op dinsdag en donderdag.");
            lesformules = new List<Lesformule>() { lesformule1, lesformule2, lesformule3, lesformule4, lesformule5, lesformule6 };

            Gebruiker gebruiker1 = new Gebruiker("username1", "25632112569", DateTime.Now, "name1", "first name1", Taijitan.Models.Domain.Enums.Geslacht.Man, new DateTime(1990, 1, 1), "Gent", "00712345678", "0236587496", "something@some.th", "somet@som.th", new Adres("België", "9000", "Gent", "Voskenslaan", "1"), 100, Gradatie.HachiDan, TypeGebruiker.Lid, lesformule4);
            GebruikerNietInLijst = new Gebruiker("username2", "25632100000", DateTime.Now, "name2", "first name2", Taijitan.Models.Domain.Enums.Geslacht.Vrouw, new DateTime(1990, 1, 1), "Gent", "00712345678", "0236587496", "something@some.th", "somet@som.th", new Adres("België", "9000", "Gent", "Voskenslaan", "1"), 100, Gradatie.JuichiDan, TypeGebruiker.Lid, lesformule1);
            GebruikerInLijst = gebruiker1;

            Lijst1Gebruiker = new List<Gebruiker>();
            Lijst1Gebruiker.Add(gebruiker1);

            Lijst1GebruikerVM = new List<GebruikerEditViewModel>();
            Lijst1GebruikerVM.Add(new GebruikerEditViewModel(gebruiker1));


            Adres adres1 = new Adres("België", "9820", "Gent", "MartialArtStraat", "5a");
            Adres adres2 = new Adres("België", "9820", "Gent", "Ledenstraat", "16");

            Gebruiker BruceLee = new Gebruiker("taijitan2", "11111111111", new DateTime(2018, 05, 16), "Lee", "Bruce", Geslacht.Man, new DateTime(1940, 11, 27), "UZ Gent", null, "0479076258", "BruceLee@MartialArt.com", "BruceLeesMom@MartialArt.com", adres1, 100, Gradatie.NiDan, TypeGebruiker.Lid,lesformule4);
            GebruikerInLesmomentLedenVanLesmomentValid = new Gebruiker("lid2", "12312312312", new DateTime(2018, 05, 24), "John", "Doe", Geslacht.Man, new DateTime(1960, 3, 24), "Brussel", "0525252525", "0479076258", "lid@MartialArt.com", "LidsMom@MartialArt.com", adres1, 100, Gradatie.JuichiDan, TypeGebruiker.Lid, lesformule2);


            // LesmomentController
            LesmomentLeden lesmomentLedenBruceLee = new LesmomentLeden() { Gebruiker = BruceLee };
            LesmomentLeden lesmomentLedenLid = new LesmomentLeden() { Gebruiker = GebruikerInLesmomentLedenVanLesmomentValid };
            List<LesmomentLeden> lesmomentLeden = new List<LesmomentLeden>();
            lesmomentLeden.Add(lesmomentLedenBruceLee);
            lesmomentLeden.Add(lesmomentLedenLid);

            LesmomentValid = new Lesmoment(DateTime.Now.AddHours(1), DateTime.Now.AddHours(2), lesmomentLeden);
            LesmomentActief = new Lesmoment(DateTime.Now, DateTime.Now.AddHours(2), lesmomentLeden);
            LesmomentActief.ZetActief(true);
            Lesmomenten = new List<Lesmoment>();
            Lesmomenten.Add(LesmomentValid);
            Lesmomenten.Add(LesmomentActief);

            //LesmateriaalController
            Thema thema1 = new Thema("handworpen") { ThemaId = 1 };
            Thema thema2 = new Thema("standen") { ThemaId = 2 };
            Themas = new List<Thema>();
            Themas.Add(thema1);
            Themas.Add(thema2);

            Lesmateriaal lesmateriaal1 = new Lesmateriaal("handworpen1", Gradatie.GoKyu, thema1) { LesmateriaalId = 1 };
            Lesmateriaal lesmateriaal2 = new Lesmateriaal("handworpen2", Gradatie.JuniDan, thema1) { LesmateriaalId = 2 };
            Lesmateriaal lesmateriaal3 = new Lesmateriaal("standen1", Gradatie.JuniDan, thema2) { LesmateriaalId = 3 };
            Lesmateriaal = new List<Lesmateriaal>();
            Lesmateriaal.Add(lesmateriaal1);
            Lesmateriaal.Add(lesmateriaal2);

        }
    }
}
