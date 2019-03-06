using Moq;
using System;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Xunit;

namespace TaijitanTests.Models.Domain {
    public class GebruikerTest {
        private Mock<Adres> adresMock;
        private Mock<Gradatie> gradatieMock;
        public GebruikerTest() {
            this.adresMock = new Mock<Adres>();
            this.gradatieMock = new Mock<Gradatie>();
        }
        #region constructor tests
        #region valid tests
        [Fact]
        public void maakGebruiker_valid() {
            var gebruiker = new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man,
                new DateTime(1999, 1, 1), "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object,
                0, gradatieMock.Object, TypeGebruiker.Lid);
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
                adresMock.Object, 0, gradatieMock.Object, TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_NaamIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, null, "voornaamtest", Geslacht.Man, new DateTime(1999, 1, 1),
                "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 0, gradatieMock.Object,
                TypeGebruiker.Lid));
        }
        #endregion
        #region voornaam invalid
        [Fact]
        public void maakGebruiker_Invalid_VoornaamIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "", Geslacht.Man, new DateTime(1999, 1, 1),
                "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 0, gradatieMock.Object,
                TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_VoornaamIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", null, Geslacht.Man, new DateTime(1999, 1, 1),
                "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 0, gradatieMock.Object,
                TypeGebruiker.Lid));
        }
        #endregion
        #region geboortedatum invalid
        [Fact]
        public void maakGebruiker_Invalid_GeboortedatumIsInToekomst_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, 
                new DateTime(DateTime.Now.Year + 1, 1, 1),"Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 
                0, gradatieMock.Object,TypeGebruiker.Lid));
        }
        #endregion
        #region email invalid
        [Fact]
        public void maakGebruiker_Invalid_EmailIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest",
                Geslacht.Man, new DateTime(1999, 1, 1), "Gent", "00712345679", "0236587497", "", "testouders@test.com",
                adresMock.Object, 0, gradatieMock.Object, TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_EmailIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest",
                Geslacht.Man, new DateTime(1999, 1, 1), "Gent", "00712345679", "0236587497", null, "testouders@test.com",
                adresMock.Object, 0, gradatieMock.Object, TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_EmailFoutFormaat_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, 
                new DateTime(1999, 1, 1),"Gent", "00712345679", "0236587497", "test@test..com", "testouders@test.com", adresMock.Object, 
                0, gradatieMock.Object,TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_EmailMetSpatie_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, 
                new DateTime(1999, 1, 1),"Gent", "00712345679", "0236587497", "test @test.com", "testouders@test.com", adresMock.Object, 
                0, gradatieMock.Object,TypeGebruiker.Lid));
        }
        #endregion
        #region rijksregister invalid
        [Fact]
        public void maakGebruiker_Invalid_RijksregisterLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, new DateTime(1999, 1, 1),
                "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 0, gradatieMock.Object,
                TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_RijksregisterNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", null, DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, new DateTime(1999, 1, 1),
                "Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 0, gradatieMock.Object,
                TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_RijksregisterTeLang_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "123123123123", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, 
                new DateTime(1999, 1, 1),"Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 
                0, gradatieMock.Object,TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_RijksregisterTeKort_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "1231231231", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, 
                new DateTime(1999, 1, 1),"Gent", "00712345679", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 
                0, gradatieMock.Object,TypeGebruiker.Lid));
        }
        #endregion
        #region telefoonnummer/gsmnummer invalid
        [Fact]
        public void maakGebruiker_Invalid_TelefoonnummerFoutFormaat_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man, 
                new DateTime(1999, 1, 1),"Gent", "123", "0236587497", "test@test.com", "testouders@test.com", adresMock.Object, 
                0, gradatieMock.Object,TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_GsmNummerLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man,
                new DateTime(1999, 1, 1), "Gent", "00712345679", "", "test@test.com", "testouders@test.com", adresMock.Object,
                0, gradatieMock.Object, TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_GsmNummerNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man,
                new DateTime(1999, 1, 1), "Gent", "00712345679", null, "test@test.com", "testouders@test.com", adresMock.Object,
                0, gradatieMock.Object, TypeGebruiker.Lid));
        }
        [Fact]
        public void maakGebruiker_Invalid_GsmNummerFoutFormaat_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Gebruiker("usernametest", "12312312312", DateTime.Now, "naamtest", "voornaamtest", Geslacht.Man,
                new DateTime(1999, 1, 1), "Gent", "00712345679", "123", "test@test.com", "testouders@test.com", adresMock.Object,
                0, gradatieMock.Object, TypeGebruiker.Lid));
        }
        #endregion
        #endregion
    }
}
