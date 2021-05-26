using System;
using System.IO;
using System.Linq;
using System.Text;

namespace TI4
{
    static class Program
    {
        private static void WriteToFile(byte[] text)
        {
            try
            {
              File.WriteAllBytes("../../../text.txt", text);
            }
            catch
            {
                // ignored
            }
        }

        private static byte[] GetFile()
        {
            byte[] fileData = null;
            try
            {
               fileData = File.ReadAllBytes("../../../text.txt");
            }
            catch
            {
                // ignored
            }

            return fileData;
        }

        private static byte[] Encryption(byte[] text, byte[] key)
       {
            var charArray = new byte[text.Length];
            for(var i = 0; i < text.Length; i++)
            {

                charArray[i] = (byte)(text[i] ^ key[i]);
            }
            return charArray;
       }

        private static byte[] BitsToBytes(string bitString)
        {
            return Enumerable.Range(0, bitString.Length / 8).
                Select(pos => Convert.ToByte(
                    bitString.Substring(pos * 8, 8),
                    2)
                ).ToArray();
        }

        private static string GetSecretKey()
        {
            string bits;
            do
            {
                Console.WriteLine("Input number:");
                var secretKey = Convert.ToInt32(Console.ReadLine());
                bits = Convert.ToString(secretKey, 2);
            } while (bits.Length != 24);
            return bits;
        }
        public static void Main(string[] args)
        {
            try
            {
                var text = GetFile();

                var bits = GetSecretKey();
                var length = text.Length;

                var lfsr = new Lfsr(24, bits);
                var key = lfsr.GetKey(length);

                var cipherText = Encryption(text, BitsToBytes(new string(key)));

                WriteToFile(cipherText);

                text = GetFile();

                var normalText = Encryption(text, BitsToBytes(new string(key)));
                WriteToFile(normalText);
                Console.WriteLine(Encoding.Default.GetString(cipherText));
                Console.WriteLine(Encoding.Default.GetString(normalText));
            }
            catch
            {
                // ignored
            }

            Console.ReadLine();
        }
    }
}