using Microsoft.AspNetCore.Mvc;
using Moq;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using Taijitan.Models.LesmomentViewModels;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Controllers
{
    public class RegistratieControllerTest
    {
        private readonly RegistratieController _controller;
        private readonly DummyDBcontext _context;
        private readonly Mock<IGebruikerRepository> _gebruikerRepository;
        private readonly Mock<ILesmomentRepository> _lesmomentRepository;
        private readonly LesmomentCreateViewModel createModel;

        public RegistratieControllerTest()
        {

            _context = new DummyDBcontext();
            _gebruikerRepository = new Mock<IGebruikerRepository>();
            _lesmomentRepository = new Mock<ILesmomentRepository>();

            _controller = new RegistratieController(_gebruikerRepository.Object, _lesmomentRepository.Object);

            createModel = new LesmomentCreateViewModel()
            {
                Datum = new System.DateTime(2020, 1, 1),
                Starttijd = new System.DateTime(2020, 1, 1, 3, 0, 0),
                Eindtijd = new System.DateTime(2020, 1, 1, 4, 0, 0),
                Gebruikers = _context.Lijst1GebruikerVM
            };

        }

        #region Get
        [Fact]
        public void Get_createViewNoUsersInDB_Valid()
        {
            _gebruikerRepository.Setup(v => v.GetAllLeden()).Returns(_context.GeenGebruikers);
            var actionResult = _controller.MaakLesmoment() as ViewResult;
            var viewmodel = actionResult?.Model as LesmomentCreateViewModel;
            Assert.IsType<LesmomentCreateViewModel>(actionResult?.Model);
            Assert.Empty(viewmodel.Gebruikers);
        }

        [Fact]
        public void Get_createView1UsersInDB_Valid()
        {
            _gebruikerRepository.Setup(v => v.GetAllLeden()).Returns(_context.Lijst1Gebruiker);
            var actionResult = _controller.MaakLesmoment() as ViewResult;
            var viewmodel = actionResult?.Model as LesmomentCreateViewModel;
            Assert.IsType<LesmomentCreateViewModel>(actionResult?.Model);
            Assert.Single(viewmodel.Gebruikers);
        }

        [Fact]
        public void Get_MaakLesmomentNull_Valid()
        {
            var actionResult = _controller.MaakLesmoment(null) as ViewResult;
            Assert.IsType<LesmomentCreateViewModel>(actionResult?.Model);

        }

        [Fact]
        public void Get_MaakLesmomentValid_Valid()
        {
            var actionResult = _controller.MaakLesmoment(createModel) as ViewResult;
            Assert.IsType<LesmomentCreateViewModel>(actionResult?.Model);
        }

        #endregion

    }
}
