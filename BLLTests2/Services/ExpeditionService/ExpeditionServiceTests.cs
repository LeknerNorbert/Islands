using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Services.ExpeditionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.RandomProvider;
using Moq;

namespace BLL.Services.ExpeditionService.Tests
{
    [TestClass()]
    public class ExpeditionServiceTests
    {
        // Random mock
        private readonly Mock<IRandomProvider> mockRandomProvider = new();

        [TestMethod()]
        public void LootCalcTest()
        {
            //Arrange

            // Az általad megadott számot fogja visszaadni mindig randomként
            // Random mock
            mockRandomProvider.Setup(randomProvider =>
                randomProvider.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            //Act

            //Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void CoinCalcTest()
        {
            //Arrange

            // Az általad megadott számot fogja visszaadni mindig randomként
            // Random mock
            mockRandomProvider.Setup(randomProvider =>
                randomProvider.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            //Act

            //Assert
            Assert.Fail();
        }
    }
}