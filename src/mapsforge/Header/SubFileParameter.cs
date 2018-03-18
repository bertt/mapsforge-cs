using Tiles.Tools;

namespace Mapsforge.Header
{
	public class SubFileParameter
	{
		public const sbyte BYTES_PER_INDEX_ENTRY = 5;
		public readonly sbyte BaseZoomLevel;
		public readonly long BlocksHeight;
		public readonly long BlocksWidth;
		public readonly long BoundaryTileBottom;
		public readonly long BoundaryTileLeft;
		public readonly long BoundaryTileRight;
		public readonly long BoundaryTileTop;
		public readonly long IndexEndAddress;
		public readonly long IndexStartAddress;
		public readonly long NumberOfBlocks;
		public readonly long StartAddress;
		public readonly long SubFileSize;
		public readonly sbyte ZoomLevelMax;
		public readonly sbyte ZoomLevelMin;
		private readonly int HashCodeValue;

		internal SubFileParameter(SubFileParameterBuilder subFileParameterBuilder)
		{
			StartAddress = subFileParameterBuilder.StartAddress;
			IndexStartAddress = subFileParameterBuilder.IndexStartAddress;
			SubFileSize = subFileParameterBuilder.SubFileSize;
			BaseZoomLevel = subFileParameterBuilder.BaseZoomLevel;
			ZoomLevelMin = subFileParameterBuilder.ZoomLevelMin;
			ZoomLevelMax = subFileParameterBuilder.ZoomLevelMax;
			HashCodeValue = CalculateHashCode();

            // calculate the XY numbers of the boundary tiles in this sub-file
            var tmin = Tilebelt.PointToTile(subFileParameterBuilder.BoundingBox.MinX, subFileParameterBuilder.BoundingBox.MinY,BaseZoomLevel);
            BoundaryTileBottom = tmin.Y;
            BoundaryTileLeft = tmin.X;

            var tmax = Tilebelt.PointToTile(subFileParameterBuilder.BoundingBox.MaxX, subFileParameterBuilder.BoundingBox.MaxY, BaseZoomLevel);
            BoundaryTileTop = tmax.Y;
            BoundaryTileRight = tmax.X;

			// calculate the horizontal and vertical amount of blocks in this sub-file
			BlocksWidth = BoundaryTileRight - BoundaryTileLeft + 1;
			BlocksHeight = BoundaryTileBottom - BoundaryTileTop + 1;

			// calculate the total amount of blocks in this sub-file
			NumberOfBlocks = BlocksWidth * BlocksHeight;

			IndexEndAddress = IndexStartAddress + NumberOfBlocks * BYTES_PER_INDEX_ENTRY;
		}

        private int CalculateHashCode()
		{
			int result = 7;
			result = 31 * result + (int)(StartAddress ^ ((long)((ulong)StartAddress >> 32)));
			result = 31 * result + (int)(SubFileSize ^ ((long)((ulong)SubFileSize >> 32)));
			result = 31 * result + BaseZoomLevel;
			return result;
		}
	}
}