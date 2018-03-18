namespace Mapsforge
{
    using System;
    using System.IO;

    public class ReadBuffer
    {
        private static object sync = new object();

        private const string CHARSET_UTF8 = "UTF-8";
        private const int DEFAULT_MAXIMUM_BUFFER_SIZE = 2500000;
        private static int maximumBufferSize = DEFAULT_MAXIMUM_BUFFER_SIZE;
        private sbyte[] bufferData;
        private int bufferPosition;
        private readonly Stream inputFile;

        public ReadBuffer(Stream inputFile)
        {
            this.inputFile = inputFile;
        }

        public virtual sbyte ReadByte()
        {
            return Convert.ToSByte(bufferData[bufferPosition++]);
        }

        public virtual bool ReadFromStream(int length)
        {
            if (bufferData == null || bufferData.Length < length)
            {
                if (length > maximumBufferSize)
                {
                    return false;
                }
                bufferData = new sbyte[length];
            }

            bufferPosition = 0;
            return inputFile.Read((byte[])(Array)bufferData, 0, length) == length;
        }

        public virtual int ReadInt()
        {
            bufferPosition += 4;
            return Deserializer.GetInt(bufferData, bufferPosition - 4);
        }

        public virtual long ReadLong()
        {
            bufferPosition += 8;
            return Deserializer.GetLong(bufferData, bufferPosition - 8);
        }

        public virtual int ReadShort()
        {
            bufferPosition += 2;
            return Deserializer.GetShort(bufferData, bufferPosition - 2);
        }

        public virtual int ReadSignedInt()
        {
            int variableByteDecode = 0;
            sbyte variableByteShift = 0;

            while ((bufferData[bufferPosition] & 0x80) != 0)
            {
                variableByteDecode |= (bufferData[bufferPosition++] & 0x7f) << variableByteShift;
                variableByteShift += 7;
            }

            if ((bufferData[bufferPosition] & 0x40) != 0)
            {
                return -(variableByteDecode | ((bufferData[bufferPosition++] & 0x3f) << variableByteShift));
            }
            return variableByteDecode | ((bufferData[bufferPosition++] & 0x3f) << variableByteShift);
        }

        public virtual int ReadUnsignedInt()
        {
            int variableByteDecode = 0;
            sbyte variableByteShift = 0;

            while ((bufferData[bufferPosition] & 0x80) != 0)
            {
                variableByteDecode |= (bufferData[bufferPosition++] & 0x7f) << variableByteShift;
                variableByteShift += 7;
            }

            return variableByteDecode | (bufferData[bufferPosition++] << variableByteShift);
        }

        public virtual string ReadUTF8EncodedString()
        {
            return ReadUTF8EncodedString(ReadUnsignedInt());
        }

        public virtual string ReadUTF8EncodedString(int stringLength)
        {
            if (stringLength > 0 && bufferPosition + stringLength <= bufferData.Length)
            {
                bufferPosition += stringLength;
                return System.Text.Encoding.GetEncoding(CHARSET_UTF8).GetString((byte[])(Array)bufferData, bufferPosition - stringLength, stringLength);
            }
            return null;
        }
    }
}