using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNAC.Core.Authentication.EAP
{
    public enum EapType
    {
        Invalid = 0,
        Identity = 1,
        Notification = 2,
        NAK = 3,
        MD5 = 4,
        OTP = 5,
        GTC = 6,
        // 7 Unused,
        // 8 Unused,
        RSA_PUBLIC_KEY = 9,
        DSS_UNILATERAL = 10,
        KEA = 11,
        KEA_VALIDATE = 12,
        TLS = 13,
        DEFENDER_TOKEN = 14,
        RSA_SECURID = 15,
        ARCOT_SYSTEMS = 16,
        LEAP = 17,
        SIM = 18,
        SRP_SHA1 = 19,
        // 20 Unused,
        TTLS = 21,
        REMOTE_ACCESS_SERVICE = 22,
        AKA = 23,
        H3COM = 24,
        PEAP = 25,
        MSCHAPV2 = 26,
        MAKE = 27,
        CRYPTOCARD = 28,
        CISCO_MSCHAPV2 = 29,
        DYNAMID = 30,
        ROB = 31,
        POTP = 32,
        MS_ATLV = 33,
        SENTRINET = 34,
        ACTIONTEC = 35,
        COGENT_BIOMETRIC = 36,
        AIRFORTRESS = 37,
        TNC = 38,
        SECURISUITE = 39,
        DEVICECONNECT = 40,
        SPEKE = 41,
        MOBAC = 42,
        FAST = 43,
        ZONELABS = 44,
        LINK = 45,
        PAX = 46,
        PSK = 47,
        SAKE = 48,
        IKEV2 = 49,
        AKA_PRIME = 50,
        GPSK = 51,
        PWD = 52,
        EKE = 53,
        PT = 54,
        TEAP = 55
    }
}
