using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Source
{

    /// <summary>
    /// Use merge sort technique, but also count when right side's element is greater than left's
    /// </summary>
    public class Inversions
    {
        public static long CountInversions(int[] input)
        {
            return Sort(input);
        }

        private static long Sort(int[] input)
        {
            return Sort(input, 0, input.Length);
        }

        private static long Sort(int[] input, int startingIndex, int endingIndex)
        {
            long inversions = 0;

            int size = endingIndex - startingIndex;

            if (size <= 1) return inversions;

            int halfIndex = startingIndex + (endingIndex - startingIndex) / 2;
            inversions += Sort(input, startingIndex, halfIndex);
            inversions += Sort(input, halfIndex, endingIndex);
            inversions += Merge(input, startingIndex, halfIndex, halfIndex, endingIndex);

            return inversions;
        }

        private static long Merge(int[] input, int leftStartingIndex, int leftEndingIndex, int rightStartingIndex, int rightEndingIndex)
        {
            long inversions = 0;

            var resultLength = (leftEndingIndex - leftStartingIndex) + (rightEndingIndex - rightStartingIndex);

            var result = new int[resultLength];

            var i = leftStartingIndex;
            var j = rightStartingIndex;

            for (int k = 0; k < resultLength; k++)
            {
                if (i == leftEndingIndex)
                {
                    result[k] = input[j];
                    j++;
                    continue;
                }

                if (j == rightEndingIndex)
                {
                    result[k] = input[i];
                    i++;
                    continue;
                }

                if (input[i] < input[j])
                {
                    result[k] = input[i];
                    i++;
                }
                else
                {
                    inversions += leftEndingIndex - i;
                    result[k] = input[j];
                    j++;
                }
            }

            // update original array.
            // other approach is to avoid using another array 'result' and
            // just shift array to make space
            for (int k = 0; k < resultLength; k++)
            {
                input[leftStartingIndex + k] = result[k];
            }

            return inversions;
        }
    }
}
