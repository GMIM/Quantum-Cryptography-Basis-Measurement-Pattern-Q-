using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasisMeasurementPattern
{
    class Client
    {
        //Variable holding the message.
        public string message = "";
        public string messageInBits = "";
        public string retrievedMessage = "";
        public string encodedMessage = "";
        public string decodedMessage = "";
        //array to hold characters
        public int[] characterBitValue = { 0, 0, 0, 0, 0, 0, 0, 0 };
        public bool [] basisMeassurementPattern = { false, false, false, false, false, false, false, false};
        public void convertAnIntToBitArray(char character)
        {
            string s = Convert.ToString((int)character, 2); //Convert to binary in a string
            characterBitValue = s.PadLeft(8, '0') // Add 0's from left
                         .Select(c => int.Parse(c.ToString())) // convert each char to int
                         .ToArray(); // Convert IEnumerable from select to Array

        }
        public int ConvertToInt(string str1)
        {
            if (str1 == "")
                throw new Exception("Invalid input");
            int val = 0, res = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                try
                {
                    val = Int32.Parse(str1[i].ToString());
                    if (val == 1)
                        res += (int)Math.Pow(2, str1.Length - 1 - i);
                    else if (val > 1)
                        throw new Exception("Invalid!");
                }
                catch
                {
                    throw new Exception("Invalid!");
                }
            }
            return res;
        }

        public String convertcharacterBitArrayToBitString()
        {
            String result = "";
            for (int i = 0; i < characterBitValue.Length; ++i)
            {
                if (characterBitValue.ElementAt(i) == 1) result += '1';
                else result += '0';
            }
            return result;
        }

        public void convertMessageToBitString()
        {

            foreach (char character in message)
            {
                //convert each character to a bit array
                convertAnIntToBitArray(character);
                //now characterBitValue array has a value convert it and add it to the bit string 
                messageInBits += convertcharacterBitArrayToBitString();
            }
        }
        public void representPattern()
        {

            for (int i = 0; i < basisMeassurementPattern.Length; i++)
            {
                if (basisMeassurementPattern[i])
                {
                    Console.Write("1-Xbasis ");

                }
                else
                {
                    Console.Write("0-Zbasis ");
                }
            }
            Console.WriteLine();



        }
        public void encode() {
            for (int i = 0; i < messageInBits.Length;)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (basisMeassurementPattern[j])
                    {
                        if (messageInBits[j + i] == '0')
                            encodedMessage += '1';
                        else
                        {
                            //this means that the current character is 1
                            encodedMessage += '0';
                        }
                    }

                    else
                    {
                        encodedMessage += messageInBits[j + i];

                    }

                }
                i += 8;
            }
        }
        public void decode(string encoded) {

            for (int i = 0; i < encoded.Length; )
            {
                for (int j = 0; j < 8; j++)
                {
                    if (basisMeassurementPattern[j])
                    {
                        if (encoded[j + i] == '0')
                            retrievedMessage += '1';
                        else
                        {
                            //this means that the current character is 1
                            retrievedMessage += '0';
                        }
                    }

                    else
                    {
                        retrievedMessage += encoded[i + j];

                    }

                }
                i += 8;
            }

            int charValue = 0;
            string charStringBits = "";
            string converted = "";
            for (int i = 0; i < (retrievedMessage.Length);)
            {

                charStringBits = "";
                for (int j = 0; j < 8; j++)
                {
                    charStringBits += retrievedMessage.ElementAt(i + j);
                }
                i += 8;

                converted += (char)ConvertToInt(charStringBits);

            }
            decodedMessage = converted;

        }

    }

   
}

