using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPSNotepad.Database;
using GPSNotepad.Model;
using System.Linq;
using Xamarin.Forms.GoogleMaps;

namespace DBTests
{
    [TestClass]
    public class RegExTestClass
    {

        [TestMethod]
        public void ParseStringToPositionTestMethod()
        {
            var pos = new Position();
            Assert.IsTrue(StringPositionConverter.TryGetPosition(out pos, "60N 20E"));
            Assert.IsTrue(StringPositionConverter.TryGetPosition(out pos, "60,23N 20,52E"));
            Assert.IsTrue(StringPositionConverter.TryGetPosition(out pos, "Text text 60s 20w Text text"));
            Assert.IsFalse(StringPositionConverter.TryGetPosition(out pos, "60N 20S"));
            Assert.IsFalse(StringPositionConverter.TryGetPosition(out pos, "60W 20E"));
            Assert.IsFalse(StringPositionConverter.TryGetPosition(out pos, "1260N 21230S"));
        }

    }
}
