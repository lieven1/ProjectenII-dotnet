using Microsoft.AspNetCore.Mvc;
using Moq;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using Taijitan.Models.GebruikerViewModels;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Controllers
{
    public class GebruikerControllerTest
    {

        private readonly GebruikerController _controller;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Gebruiker _bruceLee;
        private readonly Mock<IGebruikerRepository> _mockLidRepository;

        public GebruikerControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _bruceLee = _dummyContext.BruceLee;
            _mockLidRepository = new Mock<IGebruikerRepository>();
            _controller = new GebruikerController(_mockLidRepository.Object);
        }

        #region -- Index --
        /*
        [Fact]
        public void Index_GeeftGegevensLid() {
            var gebruiker = _bruceLee;
            var result = _controller.Index(gebruiker) as ViewResult;
            var viewLid = result.Model as Gebruiker;
            Assert.Equal("Lee",viewLid.Naam);
            Assert.Equal("Bruce", viewLid.Voornaam);
            //...
        }
        */
        #endregion

        #region -- Edit --
        [Fact]
        public void EditHttpPost_ValidEdit_UpdatesAndPersistsData()
        {
            _mockLidRepository.Setup(r => r.GetBy("brucelee")).Returns(_dummyContext.BruceLee);
            var gebruikerVM = new GebruikerEditViewModel(_bruceLee)
            {
                Naam = "BruceGewijzig"
            };
            _controller.Edit(_dummyContext.BruceLee, gebruikerVM);
            Assert.Equal("BruceGewijzigd", _bruceLee.Naam);
            _mockLidRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void EditHttpPost_ValidEdit_RedirectsToIndex()
        {
            _mockLidRepository.Setup(r => r.GetBy("brucelee")).Returns(_dummyContext.BruceLee);
            var gebruikerVM = new GebruikerEditViewModel(_bruceLee);
            var result = _controller.Edit(_dummyContext.BruceLee, gebruikerVM) as RedirectToActionResult;
            Assert.Equal("Index", result?.ActionName);
        }

        [Fact]
        public void EditHttpPost_InValidEdit_DoesNotUpdateAndPersistData()
        {
            _mockLidRepository.Setup(r => r.GetBy("brucelee")).Returns(_dummyContext.BruceLee);
            var gebruikerVM = new GebruikerEditViewModel(_bruceLee)
            {
                Naam = ""
            };
            _controller.Edit(_dummyContext.BruceLee, gebruikerVM);
            Assert.Equal("Lee", _bruceLee.Naam);
            _mockLidRepository.Verify(r => r.SaveChanges(), Times.Never);
        }
        #endregion
    }
}
