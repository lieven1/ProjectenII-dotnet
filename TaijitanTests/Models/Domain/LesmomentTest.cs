using System;
using Taijitan.Models.Domain;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Models.Domain
{


    public class LesmomentTest
    {

        private readonly DummyDBcontext _context;
        private Lesmoment lesmoment;

        // Een start- en eindtijd voor correcte initialisiatie Lesmoment object
        private readonly DateTime startTijd;
        private readonly DateTime eindTijd;

        public LesmomentTest()
        {
            _context = new DummyDBcontext();

            startTijd = DateTime.Now.AddDays(5);
            eindTijd = DateTime.Now.AddDays(6);
        }

        #region constructorTests

        [Fact]
        public void MaakLesmomentAan_NullCheckGebruikers_ExceptionRaised()
        {
            Assert.Throws<ArgumentNullException>(() => new Lesmoment(startTijd, eindTijd, null));
        }

        [Fact]
        public void MaakLesmomentAan_LegeGebruikers_Valid()
        {
            Assert.Empty(new Lesmoment(startTijd, eindTijd, _context.GeenGebruikers).Leden);
        }

        [Fact]
        public void MaakLesmomentAan_LaterStartDanEindTijd_ExceptionRaised()
        {
            Assert.Throws<ArgumentException>(() => new Lesmoment(eindTijd, startTijd, _context.Lijst1Gebruiker));
        }

        [Fact]
        public void MaakLesmomentAan_AllesCorrect_Valid()
        {
            var result = new Lesmoment(startTijd, eindTijd, _context.Lijst1Gebruiker);
            Assert.IsType<Lesmoment>(result);
            Assert.Empty(result.geefAanwezigeLeden());
            Assert.Single(result.geefIngeschrevenLeden());
        }

        [Fact]
        public void MaakLesmomentAan_StartijdInVerleden_ExceptionRaised()
        {
            Assert.Throws<ArgumentException>(() => new Lesmoment(DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-4), _context.Lijst1Gebruiker));
        }

        [Fact]
        public void MaakLesmomentAan_StarttijdMeerDanTweeUurInVerleden_ExceptionRaised()
        {
            DateTime tooLongAgo = DateTime.Now.AddMinutes(-121);

            Assert.Throws<ArgumentException>(() => new Lesmoment(tooLongAgo, eindTijd, _context.Lijst1Gebruiker));
        }
        #endregion

        #region registreerLidTests

        [Fact]
        public void RegistreerLid_OnbestaandeGebruiker_valid()
        {
            lesmoment = new Lesmoment(new DateTime(2020, 1, 1), new DateTime(2021, 1, 1), _context.Lijst1Gebruiker);

            lesmoment.RegistreerLid(_context.GebruikerNietInLijst);

            Assert.Contains(_context.GebruikerNietInLijst, lesmoment.geefAanwezigeLeden());
            Assert.Single(lesmoment.geefIngeschrevenLeden());
        }

        [Fact]
        public void RegistreerLid_IngeschrevenGebruiker_Valid()
        {
            lesmoment = new Lesmoment(new DateTime(2020, 1, 1), new DateTime(2021, 1, 1), _context.Lijst1Gebruiker);

            lesmoment.RegistreerLid(_context.GebruikerInLijst);

            Assert.Contains(_context.GebruikerInLijst, lesmoment.geefAanwezigeLeden());
            Assert.Single(lesmoment.geefIngeschrevenLeden());
            Assert.Single(lesmoment.geefAanwezigeLeden());
        }

        [Fact]
        public void RegistreerLid_nullArgument_ExceptionRaised()
        {
            lesmoment = new Lesmoment(new DateTime(2020, 1, 1), new DateTime(2021, 1, 1), _context.Lijst1Gebruiker);

            Assert.Throws<ArgumentNullException>(() => lesmoment.RegistreerLid(null));
        }

        #endregion
    }
}
