using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bluetooth_rSAP
{
    internal class EFADN
    {
        public static Contact Decode(byte[] record)
        {
            string name = "";
            string phoneNumber = "";
            try
            {
                int X = record.Length - 14;
                // Decode the Alpha Identifier (name)
                name = DecodeAlphaIdentifier(record, 0, X);

                // Get the Length of BCD number/SSC contents
                int lengthOfBCD = record[X];

                // TON/NPI
                byte tonNpi = record[X + 1]; // X+2 byte in description

                // Decode the phone number (BCD format)
                phoneNumber = DecodePhoneNumber(record, X + 2, lengthOfBCD);
            }
            catch { }
            return new Contact(name, phoneNumber);
        }
        public static byte[] GetBytesUntilFF(byte[] data)
        {
            // Find the index of the first occurrence of 0xFF
            int index = Array.IndexOf(data, (byte)0xFF);

            // If 0xFF is found, return the bytes up to that point (excluding 0xFF)
            if (index >= 0)
            {
                byte[] result = new byte[index];
                Array.Copy(data, result, index);
                return result;
            }
            else
            {
                // If no 0xFF is found, return the entire array
                return data;
            }
        }

        private static string DecodeAlphaIdentifier(byte[] data, int start, int length)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                byte[] alphaBytes = new byte[length];
                Array.Copy(data, start, alphaBytes, 0, length);
                alphaBytes = GetBytesUntilFF(alphaBytes);
                // Use ASCII, but GSM 7-bit requires unused parts to be 'FF'
                string result = Encoding.GetEncoding("iso-8859-5").GetString(alphaBytes).TrimEnd((char)0xFF);
                result = Regex.Replace(result, @"\p{C}+", string.Empty);//removes unprintable chars
                return result;
            }
            catch
            {
                return "";
            }
            
        }

        private static string DecodePhoneNumber(byte[] data, int start, int length)
        {
            StringBuilder phoneNumber = new StringBuilder();
            try
            {
                for (int i = 0; i < length; i++)
                {
                    byte bcdByte = data[start + i];
                    // Decode each nibble (high and low)
                    char lowNibble = DecodeBcdNibble((bcdByte & 0xF0) >> 4);
                    char highNibble = DecodeBcdNibble(bcdByte & 0x0F);

                    if (highNibble != 'F') phoneNumber.Append(highNibble);
                    if (lowNibble != 'F') phoneNumber.Append(lowNibble);
                }
            }
            catch
            {
                return "";
            }
            

            return phoneNumber.ToString();
        }

        private static char DecodeBcdNibble(int nibble)
        {
            return nibble switch
            {
                0xA => '*', // Extended BCD value
                0xB => '#',
                0xF => 'F', // Padding (skip this during decode)
                _ => (char)('0' + nibble) // Digit 0-9
            };
        }
    }
}
