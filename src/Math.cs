using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    /// <summary>
    /// Add, Substract, Multiply large integers represented as string
    /// Multiplication uses Karatsuba algorithm
    /// </summary>
    public static class Math
    {
        private static string[,,] addTable = new string[10,10,10];
        private static string[,] multiplyTable = new string[10, 10];
        private static string[,] subtractionTable = new string[11, 10];
        

        /// <summary>
        /// Initializes tables for base cases
        /// </summary>
        static Math()
        {
            for(int i = 0; i <= 9; i++)
            {
                for(int j = 0; j <= 9; j++)
                {
                    multiplyTable[i, j] = (i * j).ToString();
                    subtractionTable[i, j] = (i - j).ToString();

                    for (int k = 0; k <= 9; k++)
                    {
                        addTable[i, j, k] = (i + j + k).ToString();
                    }
                }
            }

            for(int i = 0; i <= 9; i++)
            {
                subtractionTable[10, i] = (10 - i).ToString();
            }
        }

        public static string Add(string num1, string num2)
        {
            var digits1 = num1.ToCharArray();
            var digits2 = num2.ToCharArray();

            var length1 = digits1.Length;
            var length2 = digits2.Length;

            var resultLength = (length1 > length2 ? length1 : length2) + 1;

            var result = new char[resultLength];

            int curry = 0;

            for (int i = 0; i < resultLength; i++)
            {
                int d1 = 0;
                int d2 = 0;

                if (i < length1) d1 = digits1[length1 - i - 1] - '0';
                if (i < length2) d2 = digits2[length2 - i - 1] - '0';

                var r = addTable[d1, d2, curry].ToCharArray();

                if (r.Length == 2)
                {
                    curry = r[0] - '0';
                    result[resultLength - i - 1] = r[1];
                }
                else
                {
                    curry = 0;
                    result[resultLength - i - 1] = r[0];
                }
            }

            return RemoveLeftZeros(resultLength, result);
        }

        public static string Multiply(string num1, string num2)
        {
            var digits1 = num1.ToCharArray();
            var digits2 = num2.ToCharArray();

            var length1 = digits1.Length;
            var length2 = digits2.Length;
           
            if(num1 == "0" || num2 == "0")
            {
                return "0";
            }
            else if(length1 == 1 && length2 == 1)
            {
                return multiplyTable[digits1[0] - '0', digits2[0] - '0'];
            }
            else
            {
                var n = GetN(length1, length2);

                var (a, b) = PadAndSplit(digits1, n);
                var (c, d) = PadAndSplit(digits2, n);

                var ac = Multiply(a, c);
                var bd = Multiply(b, d);
                var m = Subtract(Subtract(Multiply(Add(a, b), Add(c, d)), ac), bd);

                return Add(Add(AddZeroes(ac, n), bd), AddZeroes(m, n / 2));
            }
        }

        public static string Subtract(string num1, string num2)
        {
            var max = Compare(num1, num2);

            if (max == 0) return "0";

            if (max == 2)
            {
                var t = num1;
                num1 = num2;
                num2 = t;
            }

            var result = new Stack<char>();

            var num1charArray = num1.ToCharArray();
            var n1l = num1.Length;
            var n2l = num2.Length;

            for (int i = 1; i <= num1.Length; i++)
            {
                if (i > n2l)
                {
                    result.Push(num1charArray[n1l - i]);
                    continue;
                }

                var r = subtractionTable[num1charArray[n1l - i] - '0', num2[n2l - i] - '0'];

                if (r[0] == '-')
                {
                    var updatedValue = Subtract(num1.Substring(0, n1l - i), "1");
                    updatedValue = PadZeroes(updatedValue, n1l - i);
                    UpdateStartingDigits(num1charArray, updatedValue);
                    result.Push(subtractionTable[10, r[1] - '0'][0]);
                }
                else
                {
                    result.Push(r[0]);
                }
            }


            return (max == 2 ? "-" : "") + (new string(result.ToArray())).TrimStart('0');
        }

        /// <summary>
        /// N is max of given two integers
        /// and it is alway even number (by adding +1 if max input is odd)
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static int GetN(int num1, int num2)
        {
            var result = num1 > num2 ? num1 : num2;
            return result % 2 == 0 ? result : result + 1;                
        }

        /// <summary>
        /// Adds zeroes at the end of string
        /// </summary>
        /// <param name="number"></param>
        /// <param name="numOfZeroes"></param>
        /// <returns></returns>
        public static string AddZeroes(string number, int numOfZeroes)
        {
            var result = new StringBuilder(number);
            for(int i = 0; i < numOfZeroes; i++)
            {
                result.Append('0');
            }
            return result.ToString(); ;
        }

        /// <summary>
        /// Adds zeroes infront of a number to make it up to <paramref name="desireLength"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="desireLength"></param>
        /// <returns></returns>
        public static string PadZeroes(string input, int desireLength)
        {
            if (input.Length >= desireLength) return input;

            var result = new char[desireLength];

            for(int i = 1; i <= desireLength; i++)
            {
                if(i <= input.Length)
                {
                    result[desireLength - i] = input[input.Length - i];
                }
                else
                {
                    result[desireLength - i] = '0';
                }
            }

            return new string(result);
        }
                
        public static (string, string) PadAndSplit(char[] inputNumber, int n)
        {
            var input = PadZeroes(new string(inputNumber), n);
            var half = n / 2;
            return (input.Substring(0, half), input.Substring(half));
        }

        /// <summary>
        /// Compare two large integers represented as string
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns>integer;
        /// <para>0 means both inputs are equal</para>
        /// <para>1 means <paramref name="num1"/> is greater </para>
        /// <para>2 means <paramref name="num2"/> is greater</para>
        /// </returns>
        public static int Compare(string num1, string num2)
        {
            num1 = num1.TrimStart('0');
            num2 = num2.TrimStart('0');

            if(num1.Length == num2.Length)
            {
                for(int i = 0; i < num1.Length; i++)
                {
                    if (num1[i] == num2[i]) continue;
                    return num1[i] > num2[i] ? 1 : 2;
                }
                return 0;
            }
            else
            {
                return num1.Length > num2.Length ? 1 : 2;
            }
        }

        private static string RemoveLeftZeros(int resultLength, char[] result)
        {
            int startIndex = 0;
            int length = resultLength;

            for (int i = 0; i < resultLength - 1; i++)
            {
                if (result[i] == '0')
                {
                    startIndex++;
                    length--;
                }
                else
                {
                    break;
                }
            }

            return new string(result, startIndex, length);
        }

        private static void UpdateStartingDigits(char[] num1charArray, string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                num1charArray[i] = value[i];
            }
        }
    }
}
