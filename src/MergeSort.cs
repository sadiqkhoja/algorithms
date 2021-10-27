using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public static class MergeSort
    {
        public static void Sort(int[] input)
        {
            Sort(input, 0, input.Length);            
        }

        private static void Sort(int[] input, int startingIndex, int endingIndex)
        {
            int size = endingIndex - startingIndex;

            if (size <= 1) return;

            int halfIndex = startingIndex + (endingIndex - startingIndex) / 2;
            Sort(input, startingIndex, halfIndex);
            Sort(input, halfIndex, endingIndex);
            Merge(input, startingIndex, halfIndex, halfIndex, endingIndex);
        }

        private static void Merge(int[] input, int leftStartingIndex, int leftEndingIndex, int rightStartingIndex, int rightEndingIndex)
        {
            var resultLength = (leftEndingIndex - leftStartingIndex) + (rightEndingIndex - rightStartingIndex);

            var result = new int[resultLength];

            var i = leftStartingIndex;
            var j = rightStartingIndex;

            for(int k = 0; k < resultLength; k++)
            {
                if(i == leftEndingIndex)
                {
                    result[k] = input[j];
                    j++;
                    continue;
                }

                if(j == rightEndingIndex)
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
                    result[k] = input[j];
                    j++;
                }
            }

            for (int k = 0; k < resultLength; k++)
            {
                input[leftStartingIndex + k] = result[k];
            }
        }
    }
}
