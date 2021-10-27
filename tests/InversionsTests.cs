using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace DataStructures.Tests
{
    [TestClass()]
    public class InversionsTests
    {
        [TestMethod()]
        [DataRow("2,1,3,6,5,4", 4)]
        [DataRow("4,3,2,1", 6)]
        public void CountInversionsTest(string inputArray, int expectedResult)
        {
            var input = inputArray.Split(",").Select(n => int.Parse(n.Trim())).ToArray();
            Assert.AreEqual(expectedResult, Inversions.CountInversions(input));
        }

        [TestMethod()]
        public void CountInversionsTest_File()
        {
            var input = File.ReadAllLines(@"DataFiles\IntegerArray.txt").Select(n => int.Parse(n.Trim())).ToArray();
            Assert.AreEqual(2407905288, Inversions.CountInversions(input));
        }
    }
}