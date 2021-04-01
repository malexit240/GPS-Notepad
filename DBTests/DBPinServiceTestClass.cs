using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPSNotepad.Database;
using GPSNotepad.Model;
using GPSNotepad.Model.Entities;
using System.Linq;
using System;

namespace DBTests
{
    [TestClass]
    public class DBPinServiceTestClass
    {
        User user;
        DBPinService service;

        [TestInitialize]
        public void Initialize()
        {
            (new Context()).ClearDatabase();
            (new DBRegistratorService()).Registrate("user@mail.com", "username", "password").Wait();
            var service = (new DBAuthorizatorService()).Authorize("user@mail.com", "password");
            service.Wait();
            user = service.Result;
            this.service = new DBPinService();
        }

        [TestCleanup]
        public void Cleanup()
        {
            (new Context()).ClearDatabase();
            user = null;
        }

        private void CreateOnePin()
        {
            var pin = new Pin()
            {
                UserId = user.UserId,
                PinId = Guid.NewGuid(),
                Position = new Xamarin.Forms.GoogleMaps.Position(10, 10),
                Name = "Store",
                Description = "This is store",
                Favorite = true

            };

            service.CreatePin(pin).Wait();
        }

        [TestMethod]
        public void CreatePinTestMethod()
        {
            CreateOnePin();
        }

        [TestMethod]
        public void GetAllPinsForUserTestMethod()
        {
            foreach (var _ in Enumerable.Range(0, 10))
                CreateOnePin();

            var task = service.GetAllPinsForUser(user.UserId);
            task.Wait();
            Assert.AreEqual(task.Result.Count, 10);

            Assert.AreEqual(task.Result[3].Name, "Store");

        }

        [TestMethod]
        public void FindPinTestMethod()
        {
            Assert.Fail();
        }

    }
}
