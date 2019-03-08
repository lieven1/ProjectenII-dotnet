using System;
using System.Collections.Generic;
using System.Text;
using Taijitan.Models.Domain;
using Xunit;

namespace TaijitanTests.Models.Domain {
    public class AdresTest {
        #region constructor tests
        [Fact]
        public void maakAdres_valid() {
            var adres = new Adres("België","9000","Gent","Voskenslaan","1");
            Assert.IsType<Adres>(adres);
            Assert.Equal("België", adres.Land);
            Assert.Equal("9000", adres.Postcode);
            Assert.Equal("Gent", adres.Stad);
            Assert.Equal("Voskenslaan", adres.Straat);
            Assert.Equal("1", adres.Nummer);
        }
        [Fact]
        public void maakAdres_Invalid_LandIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("","9000","Gent","Voskenslaan","1"));
        }
        [Fact]
        public void maakAdres_Invalid_LandIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres(null, "9000", "Gent", "Voskenslaan", "1"));
        }
        [Fact]
        public void maakAdres_Invalid_PostcodeIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("België", "", "Gent", "Voskenslaan", "1"));
        }
        [Fact]
        public void maakAdres_Invalid_PostcodeIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("België", null, "Gent", "Voskenslaan", "1"));
        }
        [Fact]
        public void maakAdres_Invalid_PostcodeIsFoutFormaat_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("België", "blablabla", "Gent", "Voskenslaan", "1"));
        }
        [Fact]
        public void maakAdres_Invalid_StadIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("België", "9000", "", "Voskenslaan", "1"));
        }
        [Fact]
        public void maakAdres_Invalid_StadIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("België", "9000", null, "Voskenslaan", "1"));
        }
        [Fact]
        public void maakAdres_Invalid_StraatIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("België", "9000", "Gent", "", "1"));
        }
        [Fact]
        public void maakAdres_Invalid_StraatIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("België", "9000", "Gent", null, "1"));
        }
        [Fact]
        public void maakAdres_Invalid_NummerIsLeeg_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("België", "9000", "Gent", "Voskenslaan", ""));
        }
        [Fact]
        public void maakAdres_Invalid_NummerIsNull_throwsArgumentException() {
            Assert.Throws<ArgumentException>(() =>
                new Adres("België", "9000", "Gent", "Voskenslaan", null));
        }
        #endregion
    }
}