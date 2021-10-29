using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Source
{
    public class QuickSort
    {
        public enum PivotSelection
        {
            FirstElement,
            LastElement,
            Median
        }

        private PivotSelection pivotSelectionMechanism;
        private int numberOfComparison = 0;

        public QuickSort()
        {
            pivotSelectionMechanism = PivotSelection.FirstElement;
        }

        public QuickSort(PivotSelection pivotSelection)
        {
            pivotSelectionMechanism = pivotSelection;
        }

        public void Sort(int[] input)
        {
            numberOfComparison = 0;
            Sort(input, 0, input.Length);
        }

        public int GetNumberOfComparisons()
        {
            return numberOfComparison;
        }

        private void Sort(int[] input, int startIndex, int length)
        {
            if (length <= 1) return;

            numberOfComparison += length - 1;

            int pivot = SelectPivot(input, startIndex, length);
            pivot = Partition(input, startIndex, length, pivot);

            var startIndexLeftSubarray = startIndex;
            var lengthLeftSubarray = pivot - startIndex;
            Sort(input, startIndexLeftSubarray, lengthLeftSubarray);

            var startIndexRightSubarray = pivot + 1;
            var lengthRightSubarray = startIndex + length - pivot - 1;
            Sort(input, startIndexRightSubarray, lengthRightSubarray);
        }

        private int Partition(int[] input, int startIndex, int length, int pivot)
        {
            if (length <= 1) return pivot;

            Swap(input, startIndex, pivot);
            pivot = startIndex;

            int i = startIndex + 1;
            int j = startIndex + 1;

            while(j < length + startIndex)
            {
                if(input[j] < input[pivot])
                {
                    Swap(input, i, j);
                    i++;
                }                
                j++;
            }

            Swap(input, --i, pivot);

            return i;
        }

        private void Swap(int[] input, int i, int j)
        {
            if (i == j) return;

            int temp = input[i];
            input[i] = input[j];
            input[j] = temp;

        }

        private int SelectPivot(int[] input, int startIndex, int length)
        {
            switch (pivotSelectionMechanism)
            {
                case PivotSelection.FirstElement:
                    return startIndex;
                case PivotSelection.LastElement:
                    return startIndex + length - 1;
                case PivotSelection.Median:
                    return FindMedian(input, startIndex, length);
                default:
                    return startIndex;
            }            
        }

        private int FindMedian(int[] input, int startIndex, int length)
        {
            var middleIndex = startIndex + ( (length-1) / 2);
            var lastIndex = startIndex + length - 1;

            var firstElement = input[startIndex];
            var middleElement = input[middleIndex];
            var lastElement = input[lastIndex];

            if((firstElement >= middleElement && firstElement <= lastElement) || (firstElement <= middleElement && firstElement >= lastElement))
            {
                return startIndex;
            }
            else if ((middleElement >= firstElement && middleElement <= lastElement) || (middleElement <= firstElement && middleElement >= lastElement))
            {
                return middleIndex;
            }
            else
            {
                return lastIndex;
            }
            
        }
    }
}
