using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPSNotepad.Model;
using System.Linq;

namespace DBTests
{
    [TestClass]
    public class DBRegistratorTestClass
    {

        [TestInitialize]
        public void Initialize()
        {
            // (new Context()).ClearDatabase();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // (new Context()).ClearDatabase();
        }

        [TestMethod]
        public void CreateUserTestMethod()
        {
            // var service = new DBRegistratorService();
            // var task = service.Registrate("user@mail.com", "user", "password");
            // task.Wait();
            //
            // Assert.IsTrue(task.Result);
        }

    }
}
