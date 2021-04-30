using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPSNotepad.Model;

namespace DBTests
{
    [TestClass]
    public class RegExTestClass
    {

        [TestMethod]
        public void ParseStringToPositionTestMethod()
        {
            //Assert.IsNotNull(StringToPositionConverter.GetPosition("60N 20E"));
            //Assert.IsNotNull(StringToPositionConverter.GetPosition("60,23N 20,52E"));
            //Assert.IsNotNull(StringToPositionConverter.GetPosition("Text text 60s 20w Text text"));
            //Assert.IsNull(StringToPositionConverter.GetPosition("60N 20S"));
            //Assert.IsNull(StringToPositionConverter.GetPosition("60W 20E"));
            //Assert.IsNull(StringToPositionConverter.GetPosition("1260N 21230S"));
        }

    }
}
