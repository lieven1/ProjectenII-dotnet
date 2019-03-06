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

        public LesmomentTest()
        {
            _context = new DummyDBcontext();
        }

        #region constructorTests

        [Fact]
        public void MaakLesmomentAan_NullCheckGebruikers_ExceptionRaised()
        {
            Assert.Throws<ArgumentException>(() => new Lesmoment(new DateTime(2019, 1, 1), new DateTime(2020, 1, 1), null));
        }

        [Fact]
        public void MaakLesmomentAan_LegeGebruikers_Valid()
        {
            Assert.Empty(new Lesmoment(new DateTime(2019, 1, 1), new DateTime(2020, 1, 1), _context.GeenGebruikers).Leden);
        }

        [Fact]
        public void MaakLesmomentAan_LaterStartDanEindTijd_ExceptionRaised()
        {
            Assert.Throws<ArgumentException>(() => new Lesmoment(new DateTime(2020, 1, 1), new DateTime(2019, 1, 1), _context.Lijst1Gebruiker));
        }

        [Fact]
        public void MaakLesmomentAan_AllesCorrect_Valid()
        {
            var result = new Lesmoment(new DateTime(2020, 1, 1), new DateTime(2020, 1, 2), _context.Lijst1Gebruiker);
            Assert.IsType<Lesmoment>(result);
            Assert.Empty(result.geefAanwezigeLeden());
            Assert.Single(result.geefIngeschrevenLeden());
        }

        [Fact]
        public void MaakLesmomentAan_StartijdInVerleden_ExceptionRaised()
        {
            Assert.Throws<ArgumentException>(() => new Lesmoment(new DateTime(1900, 1, 1), new DateTime(1900, 2, 2), _context.Lijst1Gebruiker));
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

            Assert.Throws<Exception>(() => lesmoment.RegistreerLid(null));
        }

        #endregion
    }
}
