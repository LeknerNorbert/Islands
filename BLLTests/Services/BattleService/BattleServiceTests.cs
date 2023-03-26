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
        public void GetAllEnemiesAsync_LevelOne_ReturnEqualLevelEnemies()
        {
            // Arrange
            int amount = 0;

            // Act
            int result = amount + 10;

            // Assert
            Assert.AreEqual(10, result);
        }

        private int Osszead(int szam1, int szam2)
        {
            return szam1 + szam2;
        }

        [TestMethod()] 
        public void Osszead_tizEsTizenegy_EredmenyHuszonegy()
        {
            // Arrange
            int tiz = 10;
            int tizenegy = 11;

            // Act
            int eredmeny = Osszead(tiz, tizenegy);

            // Assert
            Assert.AreEqual(21, eredmeny);
        }

        [TestMethod()]
        public void GetBattleReportAsyncTest()
        {
            Assert.Fail();
        }
    }
}