using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPSNotepad.DatabaseMocks.UserMocks;
using GPSNotepad.DatabaseMocks;
using GPSNotepad.Core.Entities;

namespace DBTests
{
    [TestClass]
    public class DBAuthorizatorTestClass
    {
        User user;

        [TestInitialize]
        public void Initialize()
        {
            var service = new DBRegistratorService();
            user = service.Create("user", "password");
        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        [TestMethod]
        public void Authorize()
        {
            var authorize_service = new DBAuthorizatorService();
            Assert.AreEqual(authorize_service.Authorize("user", "password").Login, user.Login);
        }

        [TestMethod]
        public void IsUserExist()
        {
            var authorize_service = new DBAuthorizatorService();
            Assert.IsTrue(authorize_service.IsUserExist("user"));
        }
    }
}
