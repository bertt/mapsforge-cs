namespace Mapsforge
{
    public sealed class Deserializer
    {
        public static int GetInt(sbyte[] buffer, int offset)
        {
            return buffer[offset] << 24 | (buffer[offset + 1] & 0xff) << 16 | (buffer[offset + 2] & 0xff) << 8 | (buffer[offset + 3] & 0xff);
        }

        public static long GetLong(sbyte[] buffer, int offset)
        {
            return (buffer[offset] & 0xffL) << 56 | (buffer[offset + 1] & 0xffL) << 48 | (buffer[offset + 2] & 0xffL) << 40 | (buffer[offset + 3] & 0xffL) << 32 | (buffer[offset + 4] & 0xffL) << 24 | (buffer[offset + 5] & 0xffL) << 16 | (buffer[offset + 6] & 0xffL) << 8 | (buffer[offset + 7] & 0xffL);
        }

        public static int GetShort(sbyte[] buffer, int offset)
        {
            return buffer[offset] << 8 | (buffer[offset + 1] & 0xff);
        }
    }
}

