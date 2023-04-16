using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Services.ConfigurationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.ConfigurationService.Tests
{
    [TestClass()]
    public class ConfigurationServiceTests
    {
        [TestMethod()]
        public void GetLevelByExperienceTest_14399AsExperience_ReturnLevel11()
        {
            ConfigurationService configurationService = new();
            int level = configurationService.GetLevelByExperience(14399);
            Assert.AreEqual(11, level);
        }
    }
}