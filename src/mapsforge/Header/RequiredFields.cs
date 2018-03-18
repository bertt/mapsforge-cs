namespace Mapsforge.Header
{
    using Geometries;
    using Mapsforge;
    using Mapsforge.Utils;

    internal sealed class RequiredFields
	{
		private const string BINARY_OSM_MAGIC_BYTE = "mapsforge binary OSM";
		private const int HEADER_SIZE_MAX = 1000000;
		private const int HEADER_SIZE_MIN = 70;
		private const string MERCATOR = "Mercator";
		private const int SUPPORTED_FILE_VERSION_MIN = 3;
		private const int SUPPORTED_FILE_VERSION_MAX = 4;

		internal static void ReadBoundingBox(ReadBuffer readBuffer, MapFileInfoBuilder mapFileInfoBuilder)
		{
			double minLatitude = PointUtils.MicrodegreesToDegrees(readBuffer.ReadInt());
			double minLongitude = PointUtils.MicrodegreesToDegrees(readBuffer.ReadInt());
			double maxLatitude = PointUtils.MicrodegreesToDegrees(readBuffer.ReadInt());
			double maxLongitude = PointUtils.MicrodegreesToDegrees(readBuffer.ReadInt());

			mapFileInfoBuilder.boundingBox = new BoundingBox(minLongitude, minLatitude, maxLongitude, maxLatitude);
		}

		internal static void ReadFileSize(ReadBuffer readBuffer, long fileSize, MapFileInfoBuilder mapFileInfoBuilder)
		{
			long headerFileSize = readBuffer.ReadLong();
			mapFileInfoBuilder.fileSize = fileSize;
		}

		internal static void ReadFileVersion(ReadBuffer readBuffer, MapFileInfoBuilder mapFileInfoBuilder)
		{
			int fileVersion = readBuffer.ReadInt();
			mapFileInfoBuilder.fileVersion = fileVersion;
		}

		internal static void ReadMagicByte(ReadBuffer readBuffer)
		{
			int magicByteLength = BINARY_OSM_MAGIC_BYTE.Length;
            readBuffer.ReadFromStream(magicByteLength + 4);
			string magicByte = readBuffer.ReadUTF8EncodedString(magicByteLength);
		}

		internal static void ReadMapDate(ReadBuffer readBuffer, MapFileInfoBuilder mapFileInfoBuilder)
		{
			long mapDate = readBuffer.ReadLong();
			mapFileInfoBuilder.mapDate = mapDate;
		}

		internal static void ReadPoiTags(ReadBuffer readBuffer, MapFileInfoBuilder mapFileInfoBuilder)
		{
			int numberOfPoiTags = readBuffer.ReadShort();
			var poiTags = new string[numberOfPoiTags];
			for (int currentTagId = 0; currentTagId < numberOfPoiTags; ++currentTagId)
			{
				string tag = readBuffer.ReadUTF8EncodedString();
				poiTags[currentTagId] = tag;
			}
			mapFileInfoBuilder.poiTags = poiTags;
		}

		internal static void ReadProjectionName(ReadBuffer readBuffer, MapFileInfoBuilder mapFileInfoBuilder)
		{
			string projectionName = readBuffer.ReadUTF8EncodedString();
			mapFileInfoBuilder.projectionName = projectionName;
		}

		internal static void ReadRemainingHeader(ReadBuffer readBuffer)
		{
			int remainingHeaderSize = readBuffer.ReadInt();
            readBuffer.ReadFromStream(remainingHeaderSize);
		}

		internal static void ReadTilePixelSize(ReadBuffer readBuffer, MapFileInfoBuilder mapFileInfoBuilder)
		{
			int tilePixelSize = readBuffer.ReadShort();
			mapFileInfoBuilder.tilePixelSize = tilePixelSize;
		}

		internal static void ReadWayTags(ReadBuffer readBuffer, MapFileInfoBuilder mapFileInfoBuilder)
		{
			int numberOfWayTags = readBuffer.ReadShort();

			var wayTags = new string[numberOfWayTags];

			for (int currentTagId = 0; currentTagId < numberOfWayTags; ++currentTagId)
			{
				string tag = readBuffer.ReadUTF8EncodedString();
				wayTags[currentTagId] = tag;
			}
			mapFileInfoBuilder.wayTags = wayTags;
		}
	}
}