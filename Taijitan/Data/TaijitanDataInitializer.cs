using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Databindings;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;

namespace Taijitan.Data {
    public class TaijitanDataInitializer {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILesmomentRepository _lesmomentRepository;

        public TaijitanDataInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager, ILesmomentRepository lesmomentRepository) {
            _context = context;
            _userManager = userManager;
            _lesmomentRepository = lesmomentRepository;
        }

        public async Task InitializeData() {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated()) {
                Lesformule lesformule1 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Wednesday }, "Woensdag", "Ik volg normaal les op woensdag.");
                Lesformule lesformule2 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Saturday }, "Zaterdag", "Ik volg normaal les op zaterdag.");
                Lesformule lesformule3 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Tuesday }, "Dinsdag", "Ik volg normaal les op dinsdag.");
                Lesformule lesformule4 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Wednesday, DayOfWeek.Saturday }, "Woensdag en zaterdag", "Ik volg normaal les op woensdag en zaterdag.");
                Lesformule lesformule5 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Tuesday, DayOfWeek.Saturday }, "Dinsdag en zaterdag", "Ik volg normaal les op dinsdag en zaterdag.");
                Lesformule lesformule6 = new Lesformule(new List<DayOfWeek>() { DayOfWeek.Tuesday, DayOfWeek.Thursday }, "Dinsdag en donderdag", "Ik volg normaal les op dinsdag en donderdag.");
                _context.Lesformules.Add(lesformule1);
                _context.Lesformules.Add(lesformule2);
                _context.Lesformules.Add(lesformule3);
                _context.Lesformules.Add(lesformule4);
                _context.Lesformules.Add(lesformule5);
                _context.Lesformules.Add(lesformule6);
                _context.SaveChanges();

                Adres adres1 = new Adres("België", "9820", "Gent", "MartialArtStraat", "5a");
                Adres adres2 = new Adres("België", "9820", "Gent", "Ledenstraat", "16");
                _context.Adressen.Add(adres1);
                _context.Adressen.Add(adres2);

                Gebruiker BruceLee = new Gebruiker("taijitan2", "11111111111", new DateTime(2018, 05, 16), "Lee", "Bruce", Geslacht.Man,
                    new DateTime(1940, 11, 27), "UZ Gent", null, "0479076258", "BruceLee@MartialArt.com", "BruceLeesMom@MartialArt.com",
                    adres1, 100, Gradatie.SanDan, TypeGebruiker.Lid, lesformule3);
                Gebruiker Lid = new Gebruiker("lid2", "12312312312", new DateTime(2018, 05, 24), "John", "Doe", Geslacht.Man,
                    new DateTime(1960, 3, 24), "Brussel", "0525252525", "0479076258", "lid@MartialArt.com", "LidsMom@MartialArt.com",
                    adres1, 100, Gradatie.IchiKyu, TypeGebruiker.Lid, lesformule2);
                Gebruiker Lid2 = new Gebruiker("Doc", "12345678912", new DateTime(2018, 02, 12), "Johny", "Medicine", Geslacht.Man,
                    new DateTime(1970, 3, 24), "Antwerpen", "0525252526", "0479076259", "doc@MartialArt.com", "docsMom@MartialArt.com",
                    adres2, 101, Gradatie.ShichiDan, TypeGebruiker.Lid, lesformule5);
                _context.Gebruikers.Add(BruceLee);
                _context.Gebruikers.Add(Lid);
                _context.Gebruikers.Add(Lid2);
                _context.SaveChanges();


                DateTime datum = DateTime.Now;
                Lesmoment lesmoment1 = new Lesmoment(datum.AddDays(1), datum.AddDays(1).AddHours(2));
                Lesmoment lesmoment2 = new Lesmoment(datum.AddDays(2), datum.AddDays(2).AddHours(2));
                Lesmoment lesmoment4 = new Lesmoment(datum.AddMinutes(1), datum.AddMinutes(1).AddHours(1));

                LesmomentLeden lesmoment1LedenBruceLee = new LesmomentLeden(lesmoment1, BruceLee, true);
                LesmomentLeden lesmoment1LedenLid = new LesmomentLeden(lesmoment1, Lid, true);
                LesmomentLeden lesmoment2LedenBruceLee = new LesmomentLeden(lesmoment2, BruceLee, true);
                LesmomentLeden lesmoment2LedenLid = new LesmomentLeden(lesmoment2, Lid, true);
                LesmomentLeden lesmoment4LedenBruceLee = new LesmomentLeden(lesmoment4, BruceLee, true);
                LesmomentLeden lesmoment4LedenLid = new LesmomentLeden(lesmoment4, Lid, true);

                List<LesmomentLeden> lesmomentLeden1 = new List<LesmomentLeden>();
                List<LesmomentLeden> lesmomentLeden2 = new List<LesmomentLeden>();
                List<LesmomentLeden> lesmomentLeden4 = new List<LesmomentLeden>();

                lesmomentLeden1.Add(lesmoment1LedenBruceLee);
                lesmomentLeden1.Add(lesmoment1LedenLid);
                lesmomentLeden2.Add(lesmoment2LedenBruceLee);
                lesmomentLeden2.Add(lesmoment2LedenLid);
                lesmomentLeden4.Add(lesmoment4LedenBruceLee);
                lesmomentLeden4.Add(lesmoment4LedenLid);

                lesmoment1.Leden = lesmomentLeden1;
                lesmoment2.Leden = lesmomentLeden2;
                lesmoment4.Leden = lesmomentLeden4;

                _context.Lesmomenten.Add(lesmoment1);
                _context.Lesmomenten.Add(lesmoment2);
                _context.Lesmomenten.Add(lesmoment4);
                _context.SaveChanges();

                Thema thema = new Thema("handworpen");
                Thema thema2 = new Thema("standen");
                _context.Themas.Add(thema);
                _context.Themas.Add(thema2);
                _context.SaveChanges();

                Foto foto1 = new Foto("achterwaartse_stand_1", "jpg");
                Foto foto2 = new Foto("achterwaartse_stand_2", "jpg");
                Foto foto3 = new Foto("achterwaartse_stand_3", "jpg");
                _context.Fotos.Add(foto1);
                _context.Fotos.Add(foto2);
                _context.Fotos.Add(foto3);
                _context.SaveChanges();

                string loremIpsum = "Lorem ipsum dolor sit amet, has reque suscipiantur ad, an duo hinc habeo omnes, ex eam eirmod probatus. Vis cu dicant vocibus urbanitas, nostro facilisi eu nam, vim an aeque adolescens. Nec consequat moderatius ex. Eruditi graecis blandit vix eu, vel aperiri praesent id, ancillae scribentur ex eos.";
                string videoId = "pD3T7WNsw6k";
                Lesmateriaal lesmateriaal1 = new Lesmateriaal("handworpen1_GoKyu", Gradatie.GoKyu, thema, loremIpsum, videoId);
                Lesmateriaal lesmateriaal2 = new Lesmateriaal("standen1_GoKyu", Gradatie.GoKyu, thema2, loremIpsum, videoId);
                Lesmateriaal lesmateriaal3 = new Lesmateriaal("standen2_NiKyu", Gradatie.NiKyu, thema2, loremIpsum, videoId);
                Lesmateriaal lesmateriaal4 = new Lesmateriaal("handworpen2_JuniDan", Gradatie.JuniDan, thema, loremIpsum, videoId);
                

                FotoLesmateriaal fotoLesmateriaal1_1 = new FotoLesmateriaal(lesmateriaal1, foto1);
                FotoLesmateriaal fotoLesmateriaal1_2 = new FotoLesmateriaal(lesmateriaal1, foto2);
                FotoLesmateriaal fotoLesmateriaal1_3 = new FotoLesmateriaal(lesmateriaal1, foto3);
                FotoLesmateriaal fotoLesmateriaal2_1 = new FotoLesmateriaal(lesmateriaal2, foto1);
                FotoLesmateriaal fotoLesmateriaal2_2 = new FotoLesmateriaal(lesmateriaal2, foto2);
                FotoLesmateriaal fotoLesmateriaal2_3 = new FotoLesmateriaal(lesmateriaal2, foto3);
                FotoLesmateriaal fotoLesmateriaal3_1 = new FotoLesmateriaal(lesmateriaal3, foto1);
                FotoLesmateriaal fotoLesmateriaal3_2 = new FotoLesmateriaal(lesmateriaal3, foto2);
                FotoLesmateriaal fotoLesmateriaal3_3 = new FotoLesmateriaal(lesmateriaal3, foto3);
                FotoLesmateriaal fotoLesmateriaal4_1 = new FotoLesmateriaal(lesmateriaal4, foto1);
                FotoLesmateriaal fotoLesmateriaal4_2 = new FotoLesmateriaal(lesmateriaal4, foto2);
                FotoLesmateriaal fotoLesmateriaal4_3 = new FotoLesmateriaal(lesmateriaal4, foto3);

                List<FotoLesmateriaal> fotoLesmateriaal1 = new List<FotoLesmateriaal>();
                fotoLesmateriaal1.Add(fotoLesmateriaal1_1);
                fotoLesmateriaal1.Add(fotoLesmateriaal1_2);
                fotoLesmateriaal1.Add(fotoLesmateriaal1_3);
                List<FotoLesmateriaal> fotoLesmateriaal2 = new List<FotoLesmateriaal>();
                fotoLesmateriaal2.Add(fotoLesmateriaal2_1);
                fotoLesmateriaal2.Add(fotoLesmateriaal2_2);
                fotoLesmateriaal2.Add(fotoLesmateriaal2_3);
                List<FotoLesmateriaal> fotoLesmateriaal3 = new List<FotoLesmateriaal>();
                fotoLesmateriaal3.Add(fotoLesmateriaal3_1);
                fotoLesmateriaal3.Add(fotoLesmateriaal3_2);
                fotoLesmateriaal3.Add(fotoLesmateriaal3_3);
                List<FotoLesmateriaal> fotoLesmateriaal4 = new List<FotoLesmateriaal>();
                fotoLesmateriaal4.Add(fotoLesmateriaal4_1);
                fotoLesmateriaal4.Add(fotoLesmateriaal4_2);
                fotoLesmateriaal4.Add(fotoLesmateriaal4_3);
                lesmateriaal1.Fotos = fotoLesmateriaal1;
                lesmateriaal2.Fotos = fotoLesmateriaal2;
                lesmateriaal3.Fotos = fotoLesmateriaal3;
                lesmateriaal4.Fotos = fotoLesmateriaal4;
                
                _context.Lesmateriaal.Add(lesmateriaal1);
                _context.Lesmateriaal.Add(lesmateriaal2);
                _context.Lesmateriaal.Add(lesmateriaal3);
                _context.Lesmateriaal.Add(lesmateriaal4);
                _context.SaveChanges();

                //_lesmomentRepository.GenereerLesmomentDag();
                //_context.SaveChanges();

                await InitializeUsers(lesformule3);
            }
        }


        private async Task InitializeUsers(Lesformule formule) {
            await InitializeLid(formule);
            await InitializeBeheerder();
        }

        private async Task InitializeLid(Lesformule formule) {
            string email = "lid@taijitan.be";
            string usr = "lid";
            IdentityUser user = new IdentityUser { UserName = usr, Email = email };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "gebruiker"));
            Adres adres1 = new Adres("België", "9820", "Gent", "Ledenstraat", "16");
            var gebruiker = new Gebruiker(usr, "11111545645", new DateTime(2018, 05, 24), "John", "Doe", Geslacht.Man,
                new DateTime(1960, 3, 24), "Brussel", "0525252525", "0479076258", "lid@MartialArt.com", "LidsMom@MartialArt.com",
                adres1, 100, Gradatie.ShoDan, TypeGebruiker.Lid, formule);
            _context.Gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }
        private async Task InitializeBeheerder() {
            string email = "taijitan@taijitan.be";
            string usr = "taijitan";
            IdentityUser user = new IdentityUser { UserName = usr, Email = email };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "beheerder"));


            Adres adres1 = new Adres("België", "9820", "Gent", "MartialArtStraat", "5a");
            var gebruiker = new Gebruiker(usr, "11111111111", new DateTime(2018, 05, 16), "Lee", "Bruce", Geslacht.Man,
                new DateTime(1940, 11, 27), "UZ Gent", null, "0479076258", "BruceLee@MartialArt.com", "BruceLeesMom@MartialArt.com",
                adres1, 100, Gradatie.YonDan, TypeGebruiker.Beheerder, Lesformule.DinsdagZaterdag);
            _context.Gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }
    }
}
