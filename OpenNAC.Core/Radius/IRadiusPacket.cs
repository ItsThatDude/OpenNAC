using System.Collections.Generic;
using System.Net;

namespace OpenNAC.Core.Radius
{
    public interface IRadiusPacket
    {
        byte Identifier
        {
            get;
        }

        byte[] Authenticator
        {
            get;
        }

        byte[] SharedSecret
        {
            get;
        }

        RadiusPacketType PacketType
        {
            get;
        }

        byte[] RequestAuthenticator
        {
            get;
        }

        IRadiusPacket CreateResponsePacket(RadiusPacketType responseType);

        T GetAttribute<T>(string name);

        List<T> GetAttributes<T>(string name);

        void AddAttribute(string name, string value);
        void AddAttribute(string name, uint value);
        void AddAttribute(string name, IPAddress value);
        void AddAttribute(string name, byte[] value);

        IDictionary<string, List<object>> Attributes
        {
            get;
        }
    }
}
