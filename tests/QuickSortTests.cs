using Algorithms.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Text;

namespace Algorithms.Tests
{
    [TestClass()]
    public class QuickSortTests
    {
        [TestMethod()]
        [DataRow("5,4,8,3,2,1", "1,2,3,4,5,8")]
        [DataRow("1", "1")]
        [DataRow("1,2,3,4,5,6", "1,2,3,4,5,6")]
        [DataRow("6,5,4,3,2,1", "1,2,3,4,5,6")]
        public void SortTest(string input, string expectedResult)
        {
            var inputArray = input.Split(",").Select(i => int.Parse(i)).ToArray();

            QuickSort qs = new QuickSort(QuickSort.PivotSelection.Median);
            qs.Sort(inputArray);

            var actualResult = string.Join(",", inputArray);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        [DataRow(QuickSort.PivotSelection.FirstElement, 162085)]
        [DataRow(QuickSort.PivotSelection.LastElement, 164123)]
        [DataRow(QuickSort.PivotSelection.Median, 138382)]
        public void SortTestFile(QuickSort.PivotSelection pivotSelectMechanism, int expectedNumberOfComparisons)
        {
            var inputArray = File.ReadAllLines(@"DataFiles\QuickSort.txt").Select(i => int.Parse(i)).ToArray();
            QuickSort qs = new QuickSort(pivotSelectMechanism);
            qs.Sort(inputArray);

            for (int i = 0; i < 10000; i++)
            {
                Assert.AreEqual(i + 1, inputArray[i]);
            }

            Assert.AreEqual(expectedNumberOfComparisons, qs.GetNumberOfComparisons());
        }

        [TestMethod()]
        [DataRow("01_5")]
        [DataRow("02_5")]
        [DataRow("03_5")]
        [DataRow("04_5")]
        [DataRow("05_5")]
        [DataRow("06_10")]
        [DataRow("07_10")]
        [DataRow("08_10")]
        [DataRow("09_10")]
        [DataRow("10_10")]
        [DataRow("11_20")]
        [DataRow("12_20")]
        [DataRow("13_20")]
        [DataRow("14_20")]
        [DataRow("15_20")]
        [DataRow("16_100000")]
        [DataRow("17_100000")]
        [DataRow("18_100000")]
        [DataRow("19_1000000")]
        [DataRow("20_1000000")]
        public void SortTestMedian(string file)
        {
            QuickSort qs = new QuickSort(QuickSort.PivotSelection.Median);
            int[] inputArray;

            inputArray = File.ReadAllLines(@$"DataFiles\quicksort\input_dgrcode_{file}.txt").Select(i => int.Parse(i)).ToArray();

            qs.Sort(inputArray);

            var parts = file.Split("_");
            var count = int.Parse(parts[1]);

            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(i + 1, inputArray[i], $"Sorting failed");
            }

            var outputLines = File.ReadAllLines(@$"DataFiles\quicksort\output_dgrcode_{file}.txt");

            Assert.AreEqual(int.Parse(outputLines[2]), qs.GetNumberOfComparisons(), $"Comparison count wrong");
        }
    }
}