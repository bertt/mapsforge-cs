namespace Mapsforge.Header
{
    using Geometries;

    internal class MapFileInfoBuilder
	{
		internal BoundingBox boundingBox;
		internal long fileSize;
		internal int fileVersion;
		internal long mapDate;
		internal sbyte numberOfSubFiles;
		internal OptionalFields optionalFields;
		internal string[] poiTags;
		internal string projectionName;
		internal int tilePixelSize;
		internal string[] wayTags;
		internal sbyte zoomLevelMin;
		internal sbyte zoomLevelMax;

		internal virtual MapFileInfo Build()
		{
			return new MapFileInfo(this);
		}
	}
}