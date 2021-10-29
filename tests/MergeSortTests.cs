using Algorithms.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Algorithms.Tests
{
    [TestClass()]
    public class MergeSortTests
    {
        [TestMethod()]
        [DataRow("5, 2, 3, 1", "1, 2, 3, 5")]
        [DataRow("1", "1")]
        [DataRow("0", "0")]
        [DataRow("99,4,89,45,31,-1,0,-9,42,99", "-9,-1,0,4,31,42,45,89,99,99")]
        public void SortTest(string inputStr, string expectedStr)
        {
            var expected = expectedStr.Split(",").Select(n => int.Parse(n.Trim())).ToArray();
            var input = inputStr.Split(",").Select(n => int.Parse(n.Trim())).ToArray();

            MergeSort.Sort(input);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], input[i]);
            }

            Assert.IsTrue(true);

        }
    }
}