using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
                // Lesformules
                List<Lesformule> lesformules = new List<Lesformule>();
                lesformules.Add(new Lesformule(new List<DayOfWeek>() { DayOfWeek.Wednesday }, "Woensdag", "Ik volg normaal les op woensdag."));
                lesformules.Add(new Lesformule(new List<DayOfWeek>() { DayOfWeek.Saturday }, "Zaterdag", "Ik volg normaal les op zaterdag."));
                lesformules.Add(new Lesformule(new List<DayOfWeek>() { DayOfWeek.Tuesday }, "Dinsdag", "Ik volg normaal les op dinsdag."));
                lesformules.Add(new Lesformule(new List<DayOfWeek>() { DayOfWeek.Wednesday, DayOfWeek.Saturday }, "Woensdag en zaterdag", "Ik volg normaal les op woensdag en zaterdag."));
                lesformules.Add(new Lesformule(new List<DayOfWeek>() { DayOfWeek.Tuesday, DayOfWeek.Saturday }, "Dinsdag en zaterdag", "Ik volg normaal les op dinsdag en zaterdag."));
                lesformules.Add(new Lesformule(new List<DayOfWeek>() { DayOfWeek.Tuesday, DayOfWeek.Thursday }, "Dinsdag en donderdag", "Ik volg normaal les op dinsdag en donderdag."));
                foreach(Lesformule lesformule in lesformules)
                {
                    _context.Lesformules.Add(lesformule);
                }

                // Gebruikers
                List<Gebruiker> _gebruikers = new List<Gebruiker>();
                Adres adres;
                Gebruiker gebruiker;
                Random rand = new Random();
                for (int lesformule = 0; lesformule < 6; lesformule++)
                {
                    for(int i = 101; i <= 110; i++)
                    {
                        adres = new Adres("België", "9000", "Gent", "Teststraat", i.ToString());
                        gebruiker = new Gebruiker(
                            "gebruiker_"+ lesformule.ToString()+"_"+i.ToString(),
                            "1111111"+ lesformule.ToString()+ i.ToString(),
                            new DateTime(2018, 03, 12),
                            "Naam"+ i.ToString(),
                            "Voornaam"+ i.ToString(),
                            (Geslacht) (rand.Next(2)+1),
                            new DateTime(1970, 10, 25),
                            "Gent",
                            "078151525",
                            "0468431531",
                            "emailgebruiker@test.be",
                            "emailouders@test.be",
                            adres,
                            rand.Next(1000),
                            (Gradatie) (rand.Next(17)+1),
                            TypeGebruiker.Lid,
                            lesformules[lesformule]
                            );
                        _gebruikers.Add(gebruiker);
                        _context.Gebruikers.Add(gebruiker);
                    }
                }

                // Lesmomenten toekomst
                DateTime now = DateTime.Now;
                DateTime start, end;
                for (int i = 0; i < 7; i++)
                {
                    switch ((int)now.DayOfWeek)
                    {
                        // Zondag
                        case 0:
                            start = now.Date + new TimeSpan(11, 00, 00);
                            end = now.Date + new TimeSpan(12, 30, 00);
                            // Alle gebruikers (geen formule voorzien)
                            _context.Lesmomenten.Add(new Lesmoment(start, end, _gebruikers));
                            break;
                        // Maandag
                        case 1:
                            // Geen les
                            break;
                        // Dinsdag
                        case 2:
                            start = now.Date + new TimeSpan(18, 00, 00);
                            end = now.Date + new TimeSpan(19, 00, 00);
                            _context.Lesmomenten.Add(new Lesmoment(start, end, _gebruikers.Where(
                                g => g.Lesformule.Equals(lesformules[4]) ||
                                g.Lesformule.Equals(lesformules[5]) ||
                                g.Lesformule.Equals(lesformules[2])).ToList()));
                            break;
                        // Woensdag
                        case 3:
                            start = now.Date + new TimeSpan(14, 00, 00);
                            end = now.Date + new TimeSpan(16, 00, 00);
                            _context.Lesmomenten.Add(new Lesmoment(start, end, _gebruikers.Where(
                                g => g.Lesformule.Equals(lesformules[0]) ||
                                g.Lesformule.Equals(lesformules[3])).ToList()));
                            break;
                        // Donderdag
                        case 4:
                            start = now.Date + new TimeSpan(18, 00, 00);
                            end = now.Date + new TimeSpan(20, 00, 00);
                            _context.Lesmomenten.Add(new Lesmoment(start, end, _gebruikers.Where(
                                g => g.Lesformule.Equals(lesformules[5])).ToList()));
                            break;
                        // Vrijdag
                        case 5:
                            // Geen les
                            break;
                        // Zaterdag
                        case 6:
                            start = now.Date + new TimeSpan(11, 30, 00);
                            end = now.Date + new TimeSpan(13, 00, 00);
                            _context.Lesmomenten.Add(new Lesmoment(start, end, _gebruikers.Where(
                                g => g.Lesformule.Equals(lesformules[4]) ||
                                g.Lesformule.Equals(lesformules[3]) ||
                                g.Lesformule.Equals(lesformules[1])).ToList()));
                            break;
                    }
                    now = now.AddDays(1);
                }

                // Lesmomenten historiek
                now = DateTime.Now;
                Lesmoment lesmomentTemp;
                for (int i = 0; i < 400; i++)
                {
                    switch ((int)now.DayOfWeek)
                    {
                        // Zondag
                        case 0:
                            start = now.Date + new TimeSpan(11, 00, 00);
                            end = now.Date + new TimeSpan(12, 30, 00);
                            // Alle gebruikers (geen formule voorzien)
                            lesmomentTemp = new Lesmoment(start, end, _gebruikers);
                            foreach(Gebruiker ingeschrevenLid in lesmomentTemp.geefIngeschrevenLeden())
                            {
                                if (rand.Next(4).Equals(0))
                                {
                                    lesmomentTemp.RegistreerLid(ingeschrevenLid);
                                }
                            }
                            _context.Lesmomenten.Add(lesmomentTemp);
                            break;
                        // Maandag
                        case 1:
                            // Geen les
                            break;
                        // Dinsdag
                        case 2:
                            start = now.Date + new TimeSpan(18, 00, 00);
                            end = now.Date + new TimeSpan(19, 00, 00);
                            lesmomentTemp = new Lesmoment(start, end, _gebruikers.Where(
                                g => g.Lesformule.Equals(lesformules[4]) ||
                                g.Lesformule.Equals(lesformules[5]) ||
                                g.Lesformule.Equals(lesformules[2])).ToList());
                            foreach (Gebruiker ingeschrevenLid in lesmomentTemp.geefIngeschrevenLeden())
                            {
                                if (rand.Next(4).Equals(0))
                                {
                                    lesmomentTemp.RegistreerLid(ingeschrevenLid);
                                }
                            }
                            _context.Lesmomenten.Add(lesmomentTemp);
                            break;
                        // Woensdag
                        case 3:
                            start = now.Date + new TimeSpan(14, 00, 00);
                            end = now.Date + new TimeSpan(16, 00, 00);
                            lesmomentTemp = new Lesmoment(start, end, _gebruikers.Where(
                                g => g.Lesformule.Equals(lesformules[0]) ||
                                g.Lesformule.Equals(lesformules[3])).ToList());
                            foreach (Gebruiker ingeschrevenLid in lesmomentTemp.geefIngeschrevenLeden())
                            {
                                if (rand.Next(4).Equals(0))
                                {
                                    lesmomentTemp.RegistreerLid(ingeschrevenLid);
                                }
                            }
                            _context.Lesmomenten.Add(lesmomentTemp);
                            break;
                        // Donderdag
                        case 4:
                            start = now.Date + new TimeSpan(18, 00, 00);
                            end = now.Date + new TimeSpan(20, 00, 00);
                            lesmomentTemp = new Lesmoment(start, end, _gebruikers.Where(
                                g => g.Lesformule.Equals(lesformules[5])).ToList());
                            foreach (Gebruiker ingeschrevenLid in lesmomentTemp.geefIngeschrevenLeden())
                            {
                                if (rand.Next(4).Equals(0))
                                {
                                    lesmomentTemp.RegistreerLid(ingeschrevenLid);
                                }
                            }
                            _context.Lesmomenten.Add(lesmomentTemp);
                            break;
                        // Vrijdag
                        case 5:
                            // Geen les
                            break;
                        // Zaterdag
                        case 6:
                            start = now.Date + new TimeSpan(11, 30, 00);
                            end = now.Date + new TimeSpan(13, 00, 00);
                            lesmomentTemp = new Lesmoment(start, end, _gebruikers.Where(
                                g => g.Lesformule.Equals(lesformules[4]) ||
                                g.Lesformule.Equals(lesformules[3]) ||
                                g.Lesformule.Equals(lesformules[1])).ToList());
                            foreach (Gebruiker ingeschrevenLid in lesmomentTemp.geefIngeschrevenLeden())
                            {
                                if (rand.Next(4).Equals(0))
                                {
                                    lesmomentTemp.RegistreerLid(ingeschrevenLid);
                                }
                            }
                            _context.Lesmomenten.Add(lesmomentTemp);
                            break;
                    }
                    now = now.AddDays(-1);
                }

                // Themas
                List<Thema> themas = new List<Thema>();
                themas.Add(new Thema("Handworpen"));
                themas.Add(new Thema("Standen"));
                themas.Add(new Thema("Sprongen"));
                themas.Add(new Thema("Vallen"));

                // Fotos
                Foto foto1 = new Foto("achterwaartse_stand_1", "jpg");
                Foto foto2 = new Foto("achterwaartse_stand_2", "jpg");
                Foto foto3 = new Foto("achterwaartse_stand_3", "jpg");
                _context.Fotos.Add(foto1);
                _context.Fotos.Add(foto2);
                _context.Fotos.Add(foto3);

                // Lesmateriaal
                string loremIpsum = "Lorem ipsum dolor sit amet, has reque suscipiantur ad, an duo hinc habeo omnes, ex eam eirmod probatus. Vis cu dicant vocibus urbanitas, nostro facilisi eu nam, vim an aeque adolescens. Nec consequat moderatius ex. Eruditi graecis blandit vix eu, vel aperiri praesent id, ancillae scribentur ex eos.";
                loremIpsum += loremIpsum + loremIpsum;
                string videoId = "pD3T7WNsw6k";


                _context.Lesmateriaal.Add(new Lesmateriaal("test_lege_velden", Gradatie.RokkuKyu, themas[0]));

                List<Lesmateriaal> lesmateriaal = new List<Lesmateriaal>();
                foreach(Thema thema in themas)
                {
                    foreach(Gradatie gradatie in (Gradatie[]) Enum.GetValues(typeof(Gradatie)))
                    {
                        for(int i = 1; i < 4; i++)
                        {
                            lesmateriaal.Add(new Lesmateriaal(
                                "oefeningtitel_"+thema.Naam+"_"+gradatie.ToString()+"_"+i.ToString(),
                                gradatie,
                                thema,
                                loremIpsum,
                                videoId));
                        }
                    }
                }
                
                FotoLesmateriaal fotoLesmateriaal1, fotoLesmateriaal2, fotoLesmateriaal3;
                foreach(Lesmateriaal _lesmateriaal in lesmateriaal)
                {
                    fotoLesmateriaal1 = new FotoLesmateriaal(_lesmateriaal, foto1);
                    fotoLesmateriaal2 = new FotoLesmateriaal(_lesmateriaal, foto2);
                    fotoLesmateriaal3 = new FotoLesmateriaal(_lesmateriaal, foto3);
                    _lesmateriaal.Fotos = new List<FotoLesmateriaal>() { fotoLesmateriaal1, fotoLesmateriaal2, fotoLesmateriaal3 };
                    _context.Lesmateriaal.Add(_lesmateriaal);
                }
                _context.SaveChanges();
                await InitializeUsers(_gebruikers);
            }
        }


        private async Task InitializeUsers(List<Gebruiker> _gebruikers) {
            await InitializeLid(_gebruikers);
            await InitializeBeheerder();
        }

        private async Task InitializeLid(List<Gebruiker> _gebruikers) {
            foreach(Gebruiker gebruiker in _gebruikers)
            {
                IdentityUser user = new IdentityUser { UserName = gebruiker.Gebruikersnaam, Email = gebruiker.Email };
                await _userManager.CreateAsync(user, "P@ssword1");
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "gebruiker"));
            }
            _context.SaveChanges();
        }

        private async Task InitializeBeheerder() {
            string email = "taijitan@taijitan.be";
            string usr = "taijitan";
            IdentityUser user = new IdentityUser { UserName = usr, Email = email };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "beheerder"));
            _context.SaveChanges();
        }
    }
}
