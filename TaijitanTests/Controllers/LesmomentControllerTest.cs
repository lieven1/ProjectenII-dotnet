using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Taijitan.Controllers;
using Taijitan.Models.Domain;
using TaijitanTests.Data;
using Xunit;

namespace TaijitanTests.Controllers
{
    public class LesmomentControllerTest
    {
        private readonly LesmomentController _controller;
        private readonly DummyDBcontext _context;
        private readonly Mock<ILesmomentRepository> _lesmomentRepository;
        private readonly Mock<IGebruikerRepository> _gebruikerRepository;


        public LesmomentControllerTest()
        {

            _context = new DummyDBcontext();
            _lesmomentRepository = new Mock<ILesmomentRepository>();

            _controller = new LesmomentController(_lesmomentRepository.Object, _gebruikerRepository.Object);
        }

        #region Index
        [Fact]
        // null test, welk gedrag willen we? Standaard error view?

        public void Index_LoadGeenLesmomenten_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.GeenLesmomenten);
            var actionResult = _controller.BeheerLesmoment() as ViewResult;
            var model = actionResult?.Model as List<Lesmoment>;
            //Assert.IsType<IEnumerable<Lesmoment>>(actionResult?.Model);
            Assert.Empty(model);
        }

        [Fact]
        public void Index_LoadWelLesmomenten_Valid()
        {
            _lesmomentRepository.Setup(v => v.GetAll()).Returns(_context.Lesmomenten);
            var actionResult = _controller.BeheerLesmoment() as ViewResult;
            var model = actionResult?.Model as List<Lesmoment>;
            //Assert.IsType<List<Lesmoment>>(actionResult?.Model);
            Assert.Equal(model.Count, _context.Lesmomenten.Count);
        }

        #endregion

    }
}
