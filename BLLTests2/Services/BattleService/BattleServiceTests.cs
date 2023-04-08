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
            ////Arrange
            //double multiply = 1.5;
            //int strength = 13;
            //int churchLevel= 2;

            ////Act
            //BattleService battleService = new BattleService(null, null, null, null);
            //int result = battleService.DamageCalc(multiply, strength, churchLevel);

            ////Assert
            //Assert.IsTrue(result>=22 & result<=28);
        }

        [TestMethod()]
        public void LootCalcTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CoinCalcTest()
        {
            Assert.Fail();
        }
    }
}