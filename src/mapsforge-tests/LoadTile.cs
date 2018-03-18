using Mapsforge;
using Mapsforge.Header;
using NUnit.Framework;
using System.Reflection;

namespace mapsforge_tests
{
    [TestFixture]
    public class LoadTile
    {
        [Test]
        public void GetTile()
        {
            // arrange
            var names= Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var mapsforgeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("mapsforge_tests.testdata.140.map");

            var readBuffer = new ReadBuffer(mapsforgeStream);
            var mapFileHeader = new MapFileHeader();
            mapFileHeader.ReadHeader(readBuffer, mapsforgeStream.Length);

            // assert
            Assert.IsTrue(mapsforgeStream != null);
            Assert.IsTrue(mapFileHeader.MapFileInfo.FileVersion == 3);
            Assert.IsTrue(mapFileHeader.MapFileInfo.PoiTags.Length == 4);
            Assert.IsTrue(mapFileHeader.MapFileInfo.PoiTags[0] == "place=village");
            Assert.IsTrue(mapFileHeader.MapFileInfo.WayTags.Length==18);
            Assert.IsTrue(mapFileHeader.MapFileInfo.WayTags[0]== "natural=coastline");

            // todo: read poi's and ways
        }
    }
}
