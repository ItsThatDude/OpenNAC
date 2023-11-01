﻿using System.IO;

namespace OpenNAC.Core.Radius
{
    public interface IRadiusPacketParser
    {
        byte[] GetBytes(IRadiusPacket packet);

        IRadiusPacket Parse(byte[] packetBytes, byte[] sharedSecret);

        bool TryParsePacketFromStream(Stream stream, out IRadiusPacket packet, byte[] sharedSecret);
    }
}
