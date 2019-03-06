using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using Taijitan.Models.GebruikerViewModels;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Controllers {
    public class GebruikerControllerTest {

        private readonly GebruikerController _controller;
        private readonly DummyDBcontext _dummyContext;
        private readonly Gebruiker _gebruiker1;
        private readonly Mock<IGebruikerRepository> _mockLidRepository;

        public GebruikerControllerTest() {
            var mockHttpContext = new Mock<HttpContext>();
            var tempDataProvider = new Mock<SessionStateTempDataProvider>();

            _dummyContext = new DummyDBcontext();
            _gebruiker1 = _dummyContext.GebruikerInLijst;
            _mockLidRepository = new Mock<IGebruikerRepository>();
            _controller = new GebruikerController(_mockLidRepository.Object) {
                ControllerContext = new ControllerContext {
                    HttpContext = mockHttpContext.Object
                },
                TempData = new TempDataDictionary(mockHttpContext.Object, tempDataProvider.Object)
            };
            _controller.TempData.Add("error", new TempDataAttribute());
        }

        #region -- Index --
        [Fact]
        public void Index_GeeftGegevensLid() {
            var gebruiker = _gebruiker1;
            var result = _controller.Index(gebruiker) as ViewResult;
            var viewLid = result.Model as Gebruiker;
            Assert.Equal("name1", viewLid.Naam);
            Assert.Equal("first name1", viewLid.Voornaam);
        }
        #endregion

        #region -- Edit GET --
        [Fact]
        public void Edit_PassesGebruikerInEditViewModel() {
            _mockLidRepository.Setup(m => m.GetBy("username1")).Returns(_dummyContext.GebruikerInLijst);
            IActionResult action = _controller.Edit(_dummyContext.GebruikerInLijst);
            GebruikerEditViewModel gebruikerVM = (action as ViewResult)?.Model as GebruikerEditViewModel;
            Assert.Equal("name1", gebruikerVM?.Naam);
        }

        [Fact]
        public void Edit_GebruikerNull_ReturnsNotFound() {
            IActionResult action = _controller.Edit(null);
            Assert.IsType<NotFoundResult>(action);
        }
        #endregion

        #region -- Edit POST --
        [Fact]
        public void EditHttpPost_ValidEdit_UpdatesAndPersistsData() {
            _mockLidRepository.Setup(r => r.GetBy("username1")).Returns(_dummyContext.GebruikerInLijst);
            GebruikerEditViewModel gebruikerVM = new GebruikerEditViewModel(_gebruiker1) {
                Naam = "gebruikergewijzigd"
            };

            _controller.Edit(_gebruiker1, gebruikerVM);

            Gebruiker gebruikerGewijzigd = _dummyContext.GebruikerInLijst;
            Assert.Equal("first name1", gebruikerGewijzigd.Voornaam);
            Assert.Equal("gebruikergewijzigd", gebruikerGewijzigd.Naam);
            _mockLidRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void EditHttpPost_ValidEdit_RedirectsToIndex() {
            _mockLidRepository.Setup(r => r.GetBy("username1")).Returns(_dummyContext.GebruikerInLijst);
            var gebruikerVM = new GebruikerEditViewModel(_gebruiker1);
            var result = _controller.Edit(_dummyContext.GebruikerInLijst, gebruikerVM) as RedirectToActionResult;
            Assert.Equal("Index", result?.ActionName);
        }

        [Fact]
        public void EditHttpPost_InValidEdit_DoesNotUpdateAndPersistData() {
            _mockLidRepository.Setup(r => r.GetBy("username1")).Returns(_dummyContext.GebruikerInLijst);
            var gebruikerVM = new GebruikerEditViewModel(_gebruiker1) {
                Naam = ""
            };
            _controller.Edit(_dummyContext.GebruikerInLijst, gebruikerVM);
            Assert.Equal("username1", _gebruiker1.Gebruikersnaam);
            _mockLidRepository.Verify(r => r.SaveChanges(), Times.Never);
        }
        #endregion
    }
}