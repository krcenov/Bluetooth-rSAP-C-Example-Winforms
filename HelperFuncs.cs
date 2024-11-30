using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluetooth_rSAP
{
    public static class HelperFuncs
    {
        public static byte PopFirst(ref List<byte> list)
        {
            if (list.Count == 0)
                throw new InvalidOperationException("The list is empty.");

            byte firstElement = list[0]; // Get the first element
            list.RemoveAt(0);            // Remove the first element
            return firstElement;         // Return the removed element
        }
        public static void ReverseBitsInBytes(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = ReverseNibbles(array[i]);
            }
        }

        // Function to reverse the bits in a single byte
        public static byte ReverseNibbles(byte b)
        {
            return (byte)((b >> 4) | (b << 4));
        }
        public static byte[] GetLastTwoBytes(byte[] byteArray)
        {
            // Ensure the array has at least two bytes
            if (byteArray.Length < 2)
            {
                throw new ArgumentException("Array must have at least two bytes.");
            }

            // Create a new byte array to hold the last two bytes
            byte[] lastTwo = new byte[2];

            // Copy the last two bytes into the new array
            Array.Copy(byteArray, byteArray.Length - 2, lastTwo, 0, 2);

            return lastTwo;
        }
        public static string ByteArrToHexString(byte[] arr)
        {
            return BitConverter.ToString(arr).Replace("-", "");
        }
        public static byte[] RemoveLastBytes(byte[] byteArray, int bytesToRemove)
        {
            if (bytesToRemove < 0 || bytesToRemove > byteArray.Length)
            {
                throw new ArgumentException("Invalid number of bytes to remove.");
            }

            // Create a new array with the desired length (original length - bytesToRemove)
            byte[] result = new byte[byteArray.Length - bytesToRemove];

            // Copy the first (original length - bytesToRemove) bytes to the result array
            Array.Copy(byteArray, result, result.Length);

            return result;
        }
    }
}
