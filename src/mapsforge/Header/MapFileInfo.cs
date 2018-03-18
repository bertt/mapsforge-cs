namespace Mapsforge.Header
{
    using Geometries;

    public class MapFileInfo
	{
		public readonly BoundingBox BoundingBox;
		public readonly string Comment;
		public readonly string CreatedBy;
		public readonly bool DebugFile;
		public readonly long FileSize;
		public readonly int FileVersion;
		public readonly string LanguagesPreference;
		public readonly long MapDate;
		public readonly sbyte NumberOfSubFiles;
		public readonly string[] PoiTags;
		public readonly string ProjectionName;
		public readonly Point StartPosition;
		public readonly sbyte? StartZoomLevel;
		public readonly int TilePixelSize;
		public readonly string[] WayTags;
		public readonly sbyte ZoomLevelMin;
		public readonly sbyte ZoomLevelMax;

		internal MapFileInfo(MapFileInfoBuilder mapFileInfoBuilder)
		{
			FileSize = mapFileInfoBuilder.fileSize;
			FileVersion = mapFileInfoBuilder.fileVersion;
			BoundingBox = mapFileInfoBuilder.boundingBox;
			MapDate = mapFileInfoBuilder.mapDate;
			NumberOfSubFiles = mapFileInfoBuilder.numberOfSubFiles;
			PoiTags = mapFileInfoBuilder.poiTags;
			ProjectionName = mapFileInfoBuilder.projectionName;
			TilePixelSize = mapFileInfoBuilder.tilePixelSize;
			WayTags = mapFileInfoBuilder.wayTags;
			ZoomLevelMax = mapFileInfoBuilder.zoomLevelMax;
			ZoomLevelMin = mapFileInfoBuilder.zoomLevelMin;
		}
	}
}