using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Algorithms.Source;

namespace Algorithms.Tests
{
    [TestClass()]
    public class MathTests
    {
        [TestMethod()]
        [DataRow("10", "20", "30")]
        [DataRow("000", "0000", "0")]
        [DataRow("001", "0000", "1")]
        [DataRow("99", "99", "198")]
        [DataRow("456879999995565180004238792461879298924462132498977486874265658499153568135210", "268146213019356785124263578918123548954213248132486215486246297513214568742697", "725026213014921965128502371380002847878675380631463702360511956012368136877907")]
        [DataRow("9999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999", "9999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999", "19999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999998")]
        public void AddTest(string num1, string num2, string expectedResult)
        {
            Assert.AreEqual(expectedResult, Source.Math.Add(num1, num2));
        }

        [TestMethod()]
        [DataRow("10", "20", 2)]
        [DataRow("10", "10", 0)]
        [DataRow("20", "10", 1)]
        [DataRow("99", "100", 2)]
        [DataRow("100", "99", 1)]
        [DataRow("00100", "9", 1)]
        public void CompareTest(string num1, string num2, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Source.Math.Compare(num1, num2));
        }

        [TestMethod()]
        [DataRow("22", "11", "11")]
        [DataRow("100", "50", "50")]
        [DataRow("50", "100", "-50")]
        [DataRow("0", "100", "-100")]
        [DataRow("1000", "1", "999")]
        [DataRow("999", "1000", "-1")]
        [DataRow("999", "00000", "999")]
        [DataRow("10", "1", "9")]
        [DataRow("100", "1", "99")]
        [DataRow("1", "100", "-99")]
        public void SubtractTest(string num1, string num2, string expectedResult)
        {
            Assert.AreEqual(expectedResult, Source.Math.Subtract(num1, num2));
        }

        [TestMethod()]
        [DataRow("99", 5, "00099")]
        [DataRow("99000", 5, "99000")]
        public void PadZeroesTest(string num, int desireLength, string expectedResult)
        {
            Assert.AreEqual(expectedResult, Source.Math.PadZeroes(num, desireLength));
        }

        [TestMethod()]
        [DataRow("1234",4, "12", "34")]
        [DataRow("12345",6, "012", "345")]
        public void SplitAndPadTest(string number, int n, string expectedA, string expectedB)
        {
            var (a, b) = Source.Math.PadAndSplit(number.ToCharArray(), n);
            Assert.AreEqual(expectedA, a);
            Assert.AreEqual(expectedB, b);

        }

        [TestMethod()]
        [DataRow("2", "2", "4")]
        [DataRow("12", "20", "240")]
        [DataRow("500", "66", "33000")]
        [DataRow("3141592653589793238462643383279502884197169399375105820974944592", "2718281828459045235360287471352662497757247093699959574966967627", "8539734222673567065463550869546574495034888535765114961879601127067743044893204848617875072216249073013374895871952806582723184")]
        public void MultiplyTest(string num1, string num2, string expectedResult)
        {
            Assert.AreEqual(expectedResult, Source.Math.Multiply(num1, num2));
        }

        

        [TestMethod()]
        [DataRow(3, 2, 4)]
        public void GetNTest(int length1, int length2, int expectedResult)
        {
            Assert.AreEqual(expectedResult, Source.Math.GetN(length1, length2));
        }
    }
}