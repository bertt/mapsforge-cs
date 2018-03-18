namespace Mapsforge.Header
{
    using Geometries;
    using Mapsforge;
    using System;
    using Utils;

    internal sealed class OptionalFields
	{
		private const int HEADER_BITMASK_COMMENT = 0x08;
		private const int HEADER_BITMASK_CREATED_BY = 0x04;
		private const int HEADER_BITMASK_DEBUG = 0x80;
		private const int HEADER_BITMASK_LANGUAGES_PREFERENCE = 0x10;
		private const int HEADER_BITMASK_START_POSITION = 0x40;
		private const int HEADER_BITMASK_START_ZOOM_LEVEL = 0x20;
		private const int START_ZOOM_LEVEL_MAX = 22;

		internal static void ReadOptionalFields(ReadBuffer readBuffer, MapFileInfoBuilder mapFileInfoBuilder)
		{
			OptionalFields optionalFields = new OptionalFields(readBuffer.ReadByte());
			mapFileInfoBuilder.optionalFields = optionalFields;
			optionalFields.ReadOptionalFields(readBuffer);
		}

		internal string Comment;
		internal string CreatedBy;
		internal readonly bool HasComment;
		internal readonly bool HasCreatedBy;
		internal readonly bool HasLanguagesPreference;
		internal readonly bool HasStartPosition;
		internal readonly bool HasStartZoomLevel;
		internal readonly bool IsDebugFile;
		internal string LanguagesPreference;
		internal Point StartPosition;
		internal sbyte? StartZoomLevel;

		private OptionalFields(sbyte flags)
		{
			IsDebugFile = (flags & HEADER_BITMASK_DEBUG) != 0;
			HasStartPosition = (flags & HEADER_BITMASK_START_POSITION) != 0;
			HasStartZoomLevel = (flags & HEADER_BITMASK_START_ZOOM_LEVEL) != 0;
			HasLanguagesPreference = (flags & HEADER_BITMASK_LANGUAGES_PREFERENCE) != 0;
			HasComment = (flags & HEADER_BITMASK_COMMENT) != 0;
			HasCreatedBy = (flags & HEADER_BITMASK_CREATED_BY) != 0;
		}

		private void ReadLanguagesPreference(ReadBuffer readBuffer)
		{
			if (HasLanguagesPreference)
			{
				LanguagesPreference = readBuffer.ReadUTF8EncodedString();
			}
		}

		private void ReadMapStartPosition(ReadBuffer readBuffer)
		{
			if (HasStartPosition)
			{
				double mapStartLatitude = PointUtils.MicrodegreesToDegrees(readBuffer.ReadInt());
				double mapStartLongitude = PointUtils.MicrodegreesToDegrees(readBuffer.ReadInt());
                StartPosition = new Point(mapStartLongitude, mapStartLatitude);
			}
		}

		private void ReadMapStartZoomLevel(ReadBuffer readBuffer)
		{
			if (HasStartZoomLevel)
			{
				// get and check the start zoom level (1 byte)
				sbyte mapStartZoomLevel = readBuffer.ReadByte();
				StartZoomLevel = Convert.ToSByte(mapStartZoomLevel);
			}
		}

		private void ReadOptionalFields(ReadBuffer readBuffer)
		{
			ReadMapStartPosition(readBuffer);

			ReadMapStartZoomLevel(readBuffer);

			ReadLanguagesPreference(readBuffer);

			if (HasComment)
			{
				Comment = readBuffer.ReadUTF8EncodedString();
			}

			if (HasCreatedBy)
			{
				CreatedBy = readBuffer.ReadUTF8EncodedString();
			}
		}
	}
}