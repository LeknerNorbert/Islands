using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Services.BattleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BattleService.Tests
{
    [TestClass()]
    public class BattleServiceTests
    {

        [TestMethod()]
        public void AttackDamageTest()
        {
            //Arrange
            int strength = 13;
            int agility = 12;
            int churchLevel = 2;
            int practiceRangeLevel = 1;

            //Act
            BattleService battleService = new BattleService(null, null, null, null);
            int result = battleService.AttackDamage(strength, agility, churchLevel, practiceRangeLevel);


            //Assert
            Assert.IsTrue(result<20 & result>10);
        }

        [TestMethod()]
        public void DamageCalcTest()
        {
            //Arrange
            double multiply = 1.5;
            int strength = 13;
            int churchLevel= 2;

            //Act
            BattleService battleService = new BattleService(null, null, null, null);
            int result = battleService.DamageCalc(multiply, strength, churchLevel);

            //Assert
            Assert.IsTrue(result>=22 & result<=28);
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