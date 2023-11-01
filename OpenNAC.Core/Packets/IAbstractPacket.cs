namespace OpenNAC.Core.Packets
{
    public interface IAbstractPacket
    {
        void AddInt(int i);
        void AddString(string str);
        byte[] GetPacketBytes();
        byte ReadByte(bool moveReadHead = true);
        byte[] ReadBytes(int length, bool moveReadHead = true);
        int ReadInt();
        string ReadString();
        void WriteByte(byte b, bool moveWriteHead = true);
        void WriteBytes(byte[] b, bool moveWriteHead = true);
    }
}