using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bluetooth_rSAP.Dict;

namespace Bluetooth_rSAP
{
    public static class Dict
    {
        public enum ParameterID : byte
        {
            MaxMsgSize = 0x00,
            ConnectionStatus = 0x01,
            ResultCode = 0x02,
            DisconnectionType = 0x03,
            CommandAPDU = 0x04,
            CommandAPDU7816 = 0x10,
            ResponseAPDU = 0x05,
            ATR = 0x06,
            CardReaderStatus = 0x07,
            StatusChange = 0x08,
            Transport_Protocol = 0x09,
        }
        public enum MessageID : byte
        {
            CONNECT_REQ = 0x00,
            CONNECT_RESP = 0x01,
            DISCONNECT_REQ = 0x02,
            DISCONNECT_RESP = 0x03,
            DISCONNECT_IND = 0x04,
            TRANSFER_APDU_REQ = 0x05,
            TRANSFER_APDU_RESP = 0x06,
            TRANSFER_ATR_REQ = 0x07,
            TRANSFER_ATR_RESP = 0x08,
            POWER_SIM_OFF_REQ = 0x09,
            POWER_SIM_OFF_RESP = 0x0A,
            POWER_SIM_ON_REQ = 0x0B,
            POWER_SIM_ON_RESP = 0x0C,
            RESET_SIM_REQ = 0x0D,
            RESET_SIM_RESP = 0x0E,
            TRANSFER_CARD_READER_STATUS_REQ = 0x0F,
            TRANSFER_CARD_READER_STATUS_RESP = 0x10,
            STATUS_IND = 0x11,
            ERROR_RESP = 0x12,
            SET_TRANSPORT_PROTOCOL_REQ = 0x13,
            SET_TRANSPORT_PROTOCOL_RESP = 0x14
        }
    }
    public class Parameter
{
    public ParameterID ParId { get; set; }  // 1 byte
    public byte Reserved { get; set; } = 0x00;  // 1 byte

    // Calculate ParLen (2 bytes) based on the length of ParValue
    public byte[] ParLen
    {
        get
        {
                return BitConverter.GetBytes((ushort)(ParValue?.Length ?? 0)).Reverse().ToArray();
        }
    }

    // ParValue (variable length based on ParLen)
    public byte[] ParValue { get; set; }

    // Calculate the required padding to ensure total size is a multiple of 4
    public int Padding
    {
        get
        {
            int totalLength = 1 + 1 + 2 + (ParValue?.Length ?? 0);
            int paddingRequired = 0;

            // Add padding to make total length a multiple of 4
            while ((totalLength + paddingRequired) % 4 != 0)
            {
                paddingRequired++;  // Add 1 byte of padding at a time
            }

            return paddingRequired;
        }
    }
}

    public class Header
    {
        public MessageID MsgId { get; set; }            // 1 byte for Message ID
        public byte NumParameters { get; set; }    // 1 byte for the number of parameters
        public byte[] Reserved = new byte[] { 0x00, 0x00 };      // 2 bytes for reserved space

    }
    public class Packet
    {
        public Header header {  get; set; }
        public List<Parameter> Parameters { get; set; }        // Variable length for payload
    }
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
    //A0 A4 00 00 02 3F00 -- get master file
    //A0 A4 00 00 02 7F10 -- get TELECOM
    //A0 A4 00 00 02 6F40 -- get MSISDN

    //A0 A4 00 00 02 2FE2
    //A0 A4 00 00 02 2F05 -- get LANG FILE
    //A0 A4 00 00 02 2F08 -- get MAX CONSUMPTION


    //IF OK GET DATA
    //A0 C0 00 00 00 READBINARY
    //A0 B0 00 00 00
    //PIN VERIFY 00 20 00 01 08 31 32 33 34 35 36 37 38
    //PIN VERIFY 00 20 00 01 08 30 30 30 30 FF FF FF FF


}
