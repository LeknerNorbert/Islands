using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Services.BattleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.RandomProvider;
using Moq;

namespace BLL.Services.BattleService.Tests
{
    [TestClass()]
    public class BattleServiceTests
    {
        // Random mock
        private readonly Mock<IRandomProvider> mockRandomProvider = new();

        [TestMethod()]
        public void AttackDamageTest()
        {
            //Arrange
            int strength = 13;
            int agility = 12;
            int churchLevel = 2;
            int practiceRangeLevel = 1;

            // Az általad megadott számot fogja visszaadni mindig randomként
            // Random mock
            mockRandomProvider.Setup(randomProvider =>
                randomProvider.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            //Act
            BattleService battleService = new(null, null, null, null, mockRandomProvider.Object);
            int result = battleService.AttackDamage(strength, agility, churchLevel, practiceRangeLevel);


            //Assert
            Assert.AreEqual(18, result);
        }

        [TestMethod()]
        public void DamageCalcTest()
        {
            //Arrange
            double multiply = 1.5;
            int strength = 13;
            int churchLevel = 2;

            // Random mock
            mockRandomProvider.Setup(randomProvider =>
                randomProvider.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            //Act
            BattleService battleService = new BattleService(null, null, null, null, mockRandomProvider.Object);
            int result = battleService.DamageCalc(multiply, strength, churchLevel);

            //Assert
            Assert.AreEqual(18, result);
        }

        [TestMethod()]
        public void LootCalcTest()
        {
            //Arrange
            int intellect = 13;

            // Random mock
            mockRandomProvider.Setup(randomProvider =>
                randomProvider.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(80);

            //Act
            BattleService battleService = new BattleService(null, null, null, null, mockRandomProvider.Object);
            int result = battleService.LootCalc(intellect);

            //Assert
            Assert.AreEqual(145, result);
        }

        [TestMethod()]
        public void CoinCalcTest()
        {
            //Arrange
            int intellect = 13;

            // Random mock
            mockRandomProvider.Setup(randomProvider =>
                randomProvider.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(150);

            //Act
            BattleService battleService = new BattleService(null, null, null, null, mockRandomProvider.Object);
            int result = battleService.CoinCalc(intellect);

            //Assert
            Assert.AreEqual(215, result);
        }
    }
}