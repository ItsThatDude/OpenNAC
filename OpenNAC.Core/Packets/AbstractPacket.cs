using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace OpenNAC.Core.Packets
{
    public class AbstractPacket : IAbstractPacket
    {
        private List<byte> buffer = new List<byte>();
        private int bufferReadPosition = 0;
        private int bufferWritePosition = 0;

        public AbstractPacket()
        {

        }

        public AbstractPacket(byte[] buffer)
        {
            this.buffer.AddRange(buffer);
            bufferWritePosition += buffer.Length;
        }

        public void WriteByte(byte b, bool moveWriteHead = true)
        {
            WriteBytes(new byte[] { b }, moveWriteHead);
        }

        public void WriteBytes(byte[] b, bool moveWriteHead = true)
        {
            buffer.InsertRange(bufferWritePosition, b);

            if (moveWriteHead)
                bufferWritePosition += b.Length;
        }

        public byte ReadByte(bool moveReadHead = true)
        {
            return ReadBytes(1, moveReadHead)
                .FirstOrDefault();
        }

        public byte[] ReadBytes(int length, bool moveReadHead = true)
        {
            var returnValue = buffer.Skip(bufferReadPosition)
                .Take(length)
                    .ToArray();

            if (moveReadHead)
                bufferReadPosition += length;

            return returnValue;
        }

        public void AddInt(int i)
        {
            WriteBytes(BitConverter.GetBytes(i));
        }

        public void AddString(string str)
        {
            AddInt(str.Length);
            WriteBytes(Encoding.ASCII.GetBytes(str));
        }

        public int ReadInt()
        {
            return BitConverter.ToInt32(ReadBytes(4));
        }

        public string ReadString()
        {
            int strLength = ReadInt();
            return Encoding.ASCII.GetString(ReadBytes(strLength));
        }

        public byte[] GetPacketBytes()
        {
            return buffer.ToArray();
        }
    }
}
