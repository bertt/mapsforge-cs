namespace Mapsforge.Header
{
    using Mapsforge.Geometries;

    internal class SubFileParameterBuilder
	{
		internal sbyte BaseZoomLevel;
		internal BoundingBox BoundingBox;
		internal long IndexStartAddress;
		internal long StartAddress;
		internal long SubFileSize;
		internal sbyte ZoomLevelMax;
		internal sbyte ZoomLevelMin;

		internal virtual SubFileParameter Build()
		{
			return new SubFileParameter(this);
		}
	}
}