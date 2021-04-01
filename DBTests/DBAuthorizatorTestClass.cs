using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPSNotepad.Database;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model;

namespace DBTests
{
    [TestClass]
    public class DBAuthorizatorTestClass
    {
        string email = "user@mail.com";
        string username = "user";
        string password = "password";

        [TestInitialize]
        public void Initialize()
        {
            (new Context()).ClearDatabase();
            var service = new DBRegistratorService();
            service.Registrate(email, username, password).Wait();
        }

        [TestCleanup]
        public void Cleanup()
        {
            //(new Context()).ClearDatabase();
        }

        [TestMethod]
        public void Authorize()
        {
            var authorize_service = new DBAuthorizatorService();
            var u = authorize_service.Authorize(email, password);
            u.Wait();
            Assert.AreEqual(u.Result.Login, username);
        }

        [TestMethod]
        public void IsUserExist()
        {
            var authorize_service = new DBAuthorizatorService();
            var task = authorize_service.IsUserExist(email);
            task.Wait();
            Assert.IsTrue(task.Result);
        }

        [TestMethod]
        public void ContinueSessionTestMethod()
        {
            var authorize_service = new DBAuthorizatorService();
            var u = authorize_service.Authorize(email, password);
            u.Wait();
            var token = u.Result.SessionToken;
            var user = authorize_service.ContinueSession(token);

            Assert.AreEqual(user.Login, username);
        }
    }
}
