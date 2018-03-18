namespace Mapsforge.Header
{
    public class MapFileHeader
    {
        private const int BASE_ZOOM_LEVEL_MAX = 20;
        private const int HEADER_SIZE_MIN = 70;
        private const sbyte SIGNATURE_LENGTH_INDEX = 16;
        private const char SPACE = ' ';
        private MapFileInfo mapFileInfo;
        private SubFileParameter[] subFileParameters;
        private sbyte zoomLevelMaximum;
        private sbyte zoomLevelMinimum;


        public virtual MapFileInfo MapFileInfo
        {
            get
            {
                return mapFileInfo;
            }
        }

        public virtual void ReadHeader(ReadBuffer readBuffer, long fileSize)
        {
            RequiredFields.ReadMagicByte(readBuffer);

            RequiredFields.ReadRemainingHeader(readBuffer);

            MapFileInfoBuilder mapFileInfoBuilder = new MapFileInfoBuilder();

            RequiredFields.ReadFileVersion(readBuffer, mapFileInfoBuilder);

            RequiredFields.ReadFileSize(readBuffer, fileSize, mapFileInfoBuilder);

            RequiredFields.ReadMapDate(readBuffer, mapFileInfoBuilder);

            RequiredFields.ReadBoundingBox(readBuffer, mapFileInfoBuilder);

            RequiredFields.ReadTilePixelSize(readBuffer, mapFileInfoBuilder);

            RequiredFields.ReadProjectionName(readBuffer, mapFileInfoBuilder);

            OptionalFields.ReadOptionalFields(readBuffer, mapFileInfoBuilder);

            RequiredFields.ReadPoiTags(readBuffer, mapFileInfoBuilder);

            RequiredFields.ReadWayTags(readBuffer, mapFileInfoBuilder);

            ReadSubFileParameters(readBuffer, fileSize, mapFileInfoBuilder);

            mapFileInfo = mapFileInfoBuilder.Build();
        }

        private void ReadSubFileParameters(ReadBuffer readBuffer, long fileSize, MapFileInfoBuilder mapFileInfoBuilder)
        {
            sbyte numberOfSubFiles = readBuffer.ReadByte();
            mapFileInfoBuilder.numberOfSubFiles = numberOfSubFiles;

            var tempSubFileParameters = new SubFileParameter[numberOfSubFiles];
            zoomLevelMinimum = sbyte.MaxValue;
            zoomLevelMaximum = sbyte.MinValue;

            for (sbyte currentSubFile = 0; currentSubFile < numberOfSubFiles; ++currentSubFile)
            {
                var subFileParameterBuilder = new SubFileParameterBuilder();

                sbyte baseZoomLevel = readBuffer.ReadByte();
                subFileParameterBuilder.BaseZoomLevel = baseZoomLevel;

                sbyte zoomLevelMin = readBuffer.ReadByte();
                subFileParameterBuilder.ZoomLevelMin = zoomLevelMin;

                sbyte zoomLevelMax = readBuffer.ReadByte();
                subFileParameterBuilder.ZoomLevelMax = zoomLevelMax;

                long startAddress = readBuffer.ReadLong();
                subFileParameterBuilder.StartAddress = startAddress;

                long indexStartAddress = startAddress;
                subFileParameterBuilder.IndexStartAddress = indexStartAddress;

                long subFileSize = readBuffer.ReadLong();
                subFileParameterBuilder.SubFileSize = subFileSize;

                subFileParameterBuilder.BoundingBox = mapFileInfoBuilder.boundingBox;

                tempSubFileParameters[currentSubFile] = subFileParameterBuilder.Build();

                if (zoomLevelMinimum > tempSubFileParameters[currentSubFile].ZoomLevelMin)
                {
                    zoomLevelMinimum = tempSubFileParameters[currentSubFile].ZoomLevelMin;
                    mapFileInfoBuilder.zoomLevelMin = zoomLevelMinimum;
                }
                if (zoomLevelMaximum < tempSubFileParameters[currentSubFile].ZoomLevelMax)
                {
                    zoomLevelMaximum = tempSubFileParameters[currentSubFile].ZoomLevelMax;
                    mapFileInfoBuilder.zoomLevelMax = zoomLevelMaximum;
                }
            }

            subFileParameters = new SubFileParameter[zoomLevelMaximum + 1];
            for (int currentMapFile = 0; currentMapFile < numberOfSubFiles; ++currentMapFile)
            {
                var subFileParameter = tempSubFileParameters[currentMapFile];
                for (sbyte zoomLevel = subFileParameter.ZoomLevelMin; zoomLevel <= subFileParameter.ZoomLevelMax; ++zoomLevel)
                {
                    subFileParameters[zoomLevel] = subFileParameter;
                }
            }
        }
    }
}