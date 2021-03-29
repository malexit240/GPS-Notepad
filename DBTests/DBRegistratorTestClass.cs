using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPSNotepad.DatabaseMocks.UserMocks;
using GPSNotepad.DatabaseMocks;
using System.Linq;

namespace DBTests
{
    [TestClass]
    public class DBRegistratorTestClass
    {

        [TestMethod]
        public void CreateUserTestMethod()
        {
            var service = new DBRegistratorService();
            Assert.IsNotNull(service.Create("user", "password"));
        }

    }
}
