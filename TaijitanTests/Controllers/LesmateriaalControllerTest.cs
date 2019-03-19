using Microsoft.AspNetCore.Mvc;
using Moq;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using Taijitan.Models.Domain.Enums;
using Taijitan.Models.Domain.IRepositories;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Controllers {
    public class LesmateriaalControllerTest {
        private readonly LesmateriaalController _controller;
        private readonly DummyDBcontext _context;
        private readonly Mock<ILesmateriaalRepository> _lesmateriaalRepo;
        private readonly Gebruiker _gebruiker1;

        public LesmateriaalControllerTest() {

            _context = new DummyDBcontext();
            _gebruiker1 = _context.GebruikerInLijst;
            _lesmateriaalRepo = new Mock<ILesmateriaalRepository>();
            _controller = new LesmateriaalController(_lesmateriaalRepo.Object);
        }
        
        [Fact]
        public void Index_GeeftGradenGebruiker() {
            var result = _controller.Index(_gebruiker1.Gebruikersnaam) as ViewResult;
            var viewLid = result.Model as Gebruiker;
            Assert.Equal(Gradatie.HachiDan, viewLid.Gradatie);
        }
    }
}
