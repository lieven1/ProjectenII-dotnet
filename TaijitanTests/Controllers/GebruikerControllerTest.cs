using Microsoft.AspNetCore.Mvc;
using Taijitan.Controllers;
using Xunit;
using Taijitan.Models.Domain;
using TaijitanTests.Data;
using Taijitan.Models.GebruikerViewModels;
using Moq;

namespace TaijitanTests.Controllers {
    public class GebruikerControllerTest {

        private readonly GebruikerController _controller;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Gebruiker _bruceLee;
        private readonly Mock<IGebruikerRepository> _mockLidRepository;

        public GebruikerControllerTest() {
            _dummyContext = new DummyApplicationDbContext();
            _bruceLee = _dummyContext.BruceLee;
            _mockLidRepository = new Mock<IGebruikerRepository>();
            _controller = new GebruikerController(_mockLidRepository.Object);
        }

        #region -- Index --
        [Fact]
        public void Index_GeeftGegevensLid() {
            var lid = _bruceLee;
            var result = _controller.Index(lid) as ViewResult;
            var viewLid = result.Model as Gebruiker;
            Assert.Equal("Lee",viewLid.Naam);
            Assert.Equal("Bruce", viewLid.Voornaam);
            //...
        }
        #endregion

        #region -- Edit --
        [Fact]
        public void EditHttpPost_ValidEdit_UpdatesAndPersistsData() {
            _mockLidRepository.Setup(r => r.GetById(1)).Returns(_dummyContext.BruceLee);
            var gebruikerVM = new EditViewModel(_bruceLee) {
                Gebruikersnaam = "BruceLeeGewijzigd",
                Adres = new Adres("Japan", "1000", "Tokyo", "eenanderestraat", "15")
            };
            _controller.Edit(1, gebruikerVM);
            Assert.Equal("BruceLeeGewijzigd", _bruceLee.Naam);
            Assert.Equal(new Adres("Japan", "1000", "Tokyo", "eenanderestraat", "15"), _bruceLee.Adres);
            _mockLidRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void EditHttpPost_ValidEdit_RedirectsToIndex() {
            _mockLidRepository.Setup(r => r.GetById(1)).Returns(_dummyContext.BruceLee);
            var gebruikerVM = new EditViewModel(_bruceLee);
            var result = _controller.Edit(1, gebruikerVM) as RedirectToActionResult;
            Assert.Equal("Index", result?.ActionName);
        }

        [Fact]
        public void EditHttpPost_InValidEdit_DoesNotUpdateAndPersistData() {
            _mockLidRepository.Setup(r => r.GetById(1)).Returns(_dummyContext.BruceLee);
            var gebruikerVM = new EditViewModel(_bruceLee) {
                Naam = ""
            };
            _controller.Edit(1, gebruikerVM);
            Assert.Equal("Lee", _bruceLee.Naam);
            _mockLidRepository.Verify(r => r.SaveChanges(), Times.Never);
        }
        #endregion
    }
}
