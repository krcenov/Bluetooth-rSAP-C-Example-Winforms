using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Bluetooth_rSAP.Dict;
using static Bluetooth_rSAP.HelperFuncs;

namespace Bluetooth_rSAP
{
    public static class Packeter
    {
        public static byte[] Pack(Packet packet)
        {
            List<byte> result = new List<byte>();
            List<byte> PayloadBinary = new List<byte>();
            foreach (Parameter parameter in packet.Parameters)
            {
                List<byte> currentParameterBinary = new List<byte>();
                currentParameterBinary.Add((byte)parameter.ParId);
                currentParameterBinary.Add(parameter.Reserved);
                currentParameterBinary.AddRange(parameter.ParLen);
                currentParameterBinary.AddRange(parameter.ParValue);
                for (int i = 0; i < parameter.Padding; i++)
                {
                    currentParameterBinary.Add(0x00);
                }
                PayloadBinary.AddRange(currentParameterBinary);
            }
            packet.header.NumParameters = (byte)packet.Parameters.Count;

            result.Add((byte)packet.header.MsgId);
            result.Add(packet.header.NumParameters);
            result.AddRange(packet.header.Reserved);
            result.AddRange(PayloadBinary);
            return result.ToArray();
        }
        public static List<Parameter> GetParameters(byte[] response)
        {
            List<Parameter> toReturn = new List<Parameter>();
            List<byte> bytes = new List<byte>();
            bytes.AddRange(response);
            MessageID currentMessage = (MessageID)PopFirst(ref bytes);
            int parametersCount = (int)PopFirst(ref bytes);
            bytes.RemoveAt(0);//reserved
            bytes.RemoveAt(0);//reserved
            for (int a = 0; a < parametersCount; a++)
            {
                Parameter current = new Parameter
                {
                    ParId = (ParameterID)PopFirst(ref bytes),
                };
                bytes.RemoveAt(0);//reserved
                byte[] lenb = new byte[] { PopFirst(ref bytes), PopFirst(ref bytes) };
                Array.Reverse(lenb);
                ushort len = BitConverter.ToUInt16(lenb, 0);
                List<byte> ParValueCurrent = new List<byte>();
                //if (current.ParId == ParameterID.ResponseAPDU) 
                    //len = (ushort)(len - (ushort)2);
                for (int i = 0; i < len; i++)
                {
                    ParValueCurrent.Add(PopFirst(ref bytes));
                }
                current.ParValue = ParValueCurrent.ToArray();
                //if(current.ParId != ParameterID.ResultCode)
                    //ReverseBitsInBytes(current.ParValue);
                for (int i = 0; i < current.Padding; i++)
                {
                    bytes.RemoveAt(0);//padding
                }
                toReturn.Add(current);
            }
            return toReturn;
        }
        public static Parameter GetResponseAPDUParameter(List<Parameter> parameters)
        {
            foreach (Parameter parameter in parameters)
            {
                if(parameter.ParId == ParameterID.ResponseAPDU) return parameter;
            }
            return null;
        }
        public static string GetResponseCodeFromAPDUParameter(Parameter APDUparam)
        {
            return ByteArrToHexString(GetLastTwoBytes(APDUparam.ParValue));
        }
        public static byte[] GetValueOfAPDUParameter(Parameter APDUparam)
        {
            return RemoveLastBytes(APDUparam.ParValue, 2);
        }
        public static byte[] readRecord(int recId,int len)
        {
            //Debug.WriteLine("Read"+ len + "from  record.");
            Packet adpuSelectICCID = new Packet()
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
                        ParValue = Convert.FromHexString("A0B20104")
                    }
                }
            };
            List<byte> currentParValue = new List<byte>();
            currentParValue.AddRange(Convert.FromHexString("A0B2"));
            currentParValue.Add((byte)recId);
            currentParValue.Add(0x04);
            //currentParValue.AddRange(adpuSelectICCID.Parameters[0].ParValue);
            currentParValue.Add((byte)len);
            adpuSelectICCID.Parameters[0].ParValue = currentParValue.ToArray();
            return Pack(adpuSelectICCID);
        }
        public static byte[] readBinary(int len)
        {
            Debug.WriteLine("Read binary..");
            Packet adpuSelectICCID = new Packet()
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
                        ParValue = Convert.FromHexString("A0B00000")
                    }
                }
            };
            List<byte> currentParValue = new List<byte>();
            currentParValue.AddRange(adpuSelectICCID.Parameters[0].ParValue);
            currentParValue.Add((byte)len);
            adpuSelectICCID.Parameters[0].ParValue = currentParValue.ToArray();
            return Pack(adpuSelectICCID);
        }
        public static byte[] verifyPIN(string pin)
        {
            int pinlen = pin.Length;
            byte[] pinBlock = new byte[8];
            for (int i = 0; i < pinlen; i++)
            {
                pinBlock[i]= ASCIIEncoding.ASCII.GetBytes(pin.Substring(i, 1))[0];
            }
            for (int i = pinlen; i < pinBlock.Count(); i++)
            {
                pinBlock[i] = 0xFF;
            }
            List<byte> ParValue = new List<byte>();
            ParValue.AddRange(Convert.FromHexString("A020000108"));
            ParValue.AddRange(pinBlock);
            Debug.WriteLine("Verify PIN..");
            Packet adpuVerifyPin = new Packet()
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
                        ParValue = ParValue.ToArray()
                    }
                }
            };
            return Pack(adpuVerifyPin);
        }
    }
}
