using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bluetooth_rSAP.Dict;

namespace Bluetooth_rSAP
{
    public static class Packets
    {
        public static Packet connectionReq = new Packet()
        {
            header = new Header()
            {
                MsgId = MessageID.CONNECT_REQ
            },
            Parameters = new List<Parameter>
            {
                new Parameter()
                {
                    ParId = ParameterID.MaxMsgSize,
                    ParValue = new byte[]{ 0x01, 0x18 }
                }
            }
        };

        public static Packet answerToReset = new Packet()
        {
            header = new Header()
            {
                MsgId = MessageID.TRANSFER_ATR_REQ
            },
            Parameters = new List<Parameter>()
        };

        public static Packet adpuSelectMF = new Packet()
        {
            header = new Header()
            {
                MsgId = MessageID.TRANSFER_APDU_REQ
            },
            Parameters = new List<Parameter>
            {
                new Parameter()
                {
                    ParId = ParameterID.CommandAPDU,
                    ParValue = Convert.FromHexString("A0A40000023F00")
                }
            }
        };
        public static Packet adpuSelectICCID = new Packet()
        {
            header = new Header()
            {
                MsgId = MessageID.TRANSFER_APDU_REQ
            },
            Parameters = new List<Parameter>
            {
                new Parameter()
                {
                    ParId = ParameterID.CommandAPDU,
                    ParValue = Convert.FromHexString("A0A40000022FE2")
                }
            }
        };
        public static Packet adpuSelectTELECOM = new Packet()
        {
            header = new Header()
            {
                MsgId = MessageID.TRANSFER_APDU_REQ
            },
            Parameters = new List<Parameter>
            {
                new Parameter()
                {
                    ParId = ParameterID.CommandAPDU,
                    ParValue = Convert.FromHexString("A0A40000027F10")
                }
            }
        };
        public static Packet adpuSelectMSISDN = new Packet()
        {
            header = new Header()
            {
                MsgId = MessageID.TRANSFER_APDU_REQ
            },
            Parameters = new List<Parameter>
            {
                new Parameter()
                {
                    ParId = ParameterID.CommandAPDU,
                    ParValue = Convert.FromHexString("A0A40000026F40")
                }
            }
        };
        public static Packet adpuSelectIMSI = new Packet()//not working
        {
            header = new Header()
            {
                MsgId = MessageID.TRANSFER_APDU_REQ
            },
            Parameters = new List<Parameter>
            {
                new Parameter()
                {
                    ParId = ParameterID.CommandAPDU,
                    ParValue = Convert.FromHexString("A0A40000026F07")
                }
            }
        };
        public static Packet adpuSelectPhoneBook = new Packet()//not working
        {
            header = new Header()
            {
                MsgId = MessageID.TRANSFER_APDU_REQ
            },
            Parameters = new List<Parameter>
            {
                new Parameter()
                {
                    ParId = ParameterID.CommandAPDU,
                    ParValue = Convert.FromHexString("A0A40000025F3A")
                }
            }
        };
        public static Packet adpuSelectEFADN = new Packet()//not working
        {
            header = new Header()
            {
                MsgId = MessageID.TRANSFER_APDU_REQ
            },
            Parameters = new List<Parameter>
            {
                new Parameter()
                {
                    ParId = ParameterID.CommandAPDU,
                    ParValue = Convert.FromHexString("A0A40000026F3A")
                }
            }
        };
    }
}
