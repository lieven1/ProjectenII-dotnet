using Moq;
using System;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Models.Domain {
    public class GebruikerTest {
        private Mock<Adres> adresMock;
        private readonly DummyDBcontext _context;


        public GebruikerTest() {
            this.adresMock = new Mock<Adres>();
            _context = new DummyDBcontext();
        }
        #region constructor tests
        #region valid tests
        [Fact]
        public void maakGebruiker_valid() {
            var gebruiker = new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man,
                new DateTime(1999, 1, 1), "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object,
                0, Gradatie.GoKyu, TypeGebruiker.Lid,_context.Lesformules[1]);
            Assert.IsType<Gebruiker>(gebruiker);
            Assert.Equal("usernametest", gebruiker.Gebruikersnaam);
            Assert.Equal("naamtest", gebruiker.Naam);
            Assert.Equal("00712345679", gebruiker.Telefoonnummer);
            Assert.Equal("0236587497", gebruiker.Gsmnummer);
            Assert.Equal("test@test.com", gebruiker.Email);
        }
        #endregion
        #region naam invalid
        [Fact]
        public void maakGebruiker_Invalid_NaamIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "", "voornaamtest",
                Geslacht.Man, new DateTime(1999, 1, 1), "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com",
                adresMock.Object, 0, Gradatie.HachiDan, TypeGebruiker.Lid, _context.Lesformules[3]));
        }
        [Fact]
        public void maakGebruiker_Invalid_NaamIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, null, "voornaamtest", Geslacht.Man, new DateTime(1999, 1, 1),
                "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 0, Gradatie.JuDan,
                TypeGebruiker.Lid, _context.Lesformules[2]));
        }
        #endregion
        #region voornaam invalid
        [Fact]
        public void maakGebruiker_Invalid_VoornaamIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "", Geslacht.Man, new DateTime(1999, 1, 1),
                "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 0, Gradatie.JuniDan,
                TypeGebruiker.Lid, _context.Lesformules[4]));
        }
        [Fact]
        public void maakGebruiker_Invalid_VoornaamIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", null, Geslacht.Man, new DateTime(1999, 1, 1),
                "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 0, Gradatie.NiDan,
                TypeGebruiker.Lid, _context.Lesformules[2]));
        }
        #endregion
        #region email invalid
        [Fact]
        public void maakGebruiker_Invalid_EmailIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest",
                Geslacht.Man, new DateTime(1999, 1, 1), "Gent", "00712345679", "0236587497", "", "testouders@test.com",
                adresMock.Object, 0, Gradatie.RokkuKyu, TypeGebruiker.Lid, _context.Lesformules[4]));
        }
        [Fact]
        public void maakGebruiker_Invalid_EmailIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest",
                Geslacht.Man, new DateTime(1999, 1, 1), "Gent", "00712345679", "0236587497", null, "testouders@test.com",
                adresMock.Object, 0, Gradatie.SanKyu, TypeGebruiker.Lid, _context.Lesformules[5]));
        }
        [Fact]
        public void maakGebruiker_Invalid_EmailFoutFormaat_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, 
                new DateTime(1999, 1, 1),"Gent", "00712345679", "0236587497", "test@test..com", "testouders@test.com", adresMock.Object, 
                0, Gradatie.RokkuDan,TypeGebruiker.Lid, _context.Lesformules[2]));
        }
        [Fact]
        public void maakGebruiker_Invalid_EmailMetSpatie_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, 
                new DateTime(1999, 1, 1),"Gent", "00712345679", "0236587497", "test @test.com", "testouders@test.com", adresMock.Object, 
                0, Gradatie.ShichiDan,TypeGebruiker.Lid, _context.Lesformules[4]));
        }
        #endregion
        #region telefoonnummer/gsmnummer invalid
        [Fact]
        public void maakGebruiker_Invalid_TelefoonnummerFoutFormaat_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, 
                new DateTime(1999, 1, 1),"Gent", "123", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 
                0, Gradatie.YonKyu,TypeGebruiker.Lid, _context.Lesformules[2]));
        }
        [Fact]
        public void maakGebruiker_Invalid_GsmNummerLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man,
                new DateTime(1999, 1, 1), "Gent", "00712345679", "", "test@test.com", "testouders@test.com", adresMock.Object,
                0, Gradatie.ShichiDan, TypeGebruiker.Lid, _context.Lesformules[0]));
        }
        [Fact]
        public void maakGebruiker_Invalid_GsmNummerNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man,
                new DateTime(1999, 1, 1), "Gent", "00712345679", null, "test@test.com", "testouders@test.com", adresMock.Object,
                0, Gradatie.YonDan, TypeGebruiker.Lid, _context.Lesformules[4]));
        }
        [Fact]
        public void maakGebruiker_Invalid_GsmNummerFoutFormaat_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man,
                new DateTime(1999, 1, 1), "Gent", "00712345679", "123", "test@test.com", "testouders@test.com", adresMock.Object,
                0, Gradatie.SanKyu, TypeGebruiker.Lid, _context.Lesformules[2]));
        }
        #endregion
        #endregion
    }
}
