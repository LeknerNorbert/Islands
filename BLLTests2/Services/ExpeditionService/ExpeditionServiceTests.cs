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
            int intellect = 13;
            int difficulty = 2;

            // Az általad megadott számot fogja visszaadni mindig randomként
            // Random mock
            mockRandomProvider.Setup(randomProvider =>
                randomProvider.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(80);

            //Act
            ExpeditionService expeditionService = new ExpeditionService(null, null, mockRandomProvider.Object);
            int result = expeditionService.LootCalc(intellect, difficulty);

            //Assert
            Assert.AreEqual(145, result);
        }

        [TestMethod()]
        public void CoinCalcTest()
        {
            //Arrange
            int intellect = 13;
            int difficulty = 2;

            // Az általad megadott számot fogja visszaadni mindig randomként
            // Random mock
            mockRandomProvider.Setup(randomProvider =>
                randomProvider.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(150);

            //Act
            ExpeditionService expeditionService = new ExpeditionService(null, null, mockRandomProvider.Object);
            int result = expeditionService.LootCalc(intellect, difficulty);

            //Assert
            Assert.AreEqual(215, result);
        }
    }
}