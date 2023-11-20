namespace _03.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T> where T : IComparable<T>
    {
        private List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);
            this.HeapifyUp(this.elements.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = (index - 1) / 2;

            while (index > 0 && IsGreater(index, parentIndex))
            {
                this.Swap(index, parentIndex);

                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private bool IsGreater(int index, int parentIndex)
        {
            return this.elements[index].CompareTo(this.elements[parentIndex]) > 0;
        }

        private void Swap(int index, int parentIndex)
        {
            var temp = this.elements[index];
            this.elements[index] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
        }

        public T ExtractMax()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }

            var element = this.elements[0];
            this.Swap(0, this.elements.Count - 1);
            this.elements.RemoveAt(this.elements.Count - 1);
            this.HeapifyDown(0);

            return element;
        }

        private void HeapifyDown(int index)
        {
            var biggerChildIndex = this.GetBiggerChildIndex(index);

            while (biggerChildIndex < this.elements.Count && biggerChildIndex > 0 && this.IsGreater(biggerChildIndex, index))
            {
                this.Swap(index, biggerChildIndex);
                index = biggerChildIndex;
                biggerChildIndex = GetBiggerChildIndex(index);
            }
        }

        private int GetBiggerChildIndex(int index)
        {
            int leftChildIndex = (2 * index) + 1;
            int rightChildIndex = (2 * index) + 2;

            // Check if either child index exceeds the elements count
            if (leftChildIndex >= this.elements.Count)
            {
                return -1; // No child exists
            }

            // If only left child exists
            if (rightChildIndex >= this.elements.Count)
            {
                return leftChildIndex;
            }

            // Compare left and right children to find the bigger one
            return IsGreater(leftChildIndex, rightChildIndex) ? leftChildIndex : rightChildIndex;
        }

        public T Peek()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }

            return this.elements[0];
        }
    }
}
