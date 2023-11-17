namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        private const int CAPACITY = 4;

        private T[] elements;

        private int startIndex;
        private int endIndex;

        public CircularQueue(int capacity = CAPACITY)
        {
            this.elements = new T[capacity];
        }

        public int Count { get; set; }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var itemToRemove = this.elements[startIndex];   
            this.Count--;

            this.startIndex = (this.startIndex + 1) % this.elements.Length;

            return itemToRemove;
        }

        public void Enqueue(T item)
        {
            if (this.Count >= this.elements.Length)
            {
                this.Grow();
            }

            this.elements[endIndex] = item;
            this.endIndex = (this.endIndex + 1) % this.elements.Length;
            this.Count++;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.elements[startIndex];
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.elements[(this.startIndex + i) % this.elements.Length];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void Grow()
        {
            this.elements = this.CopyElements();
            this.startIndex = 0;
            this.endIndex = this.Count;
        }

        private T[] CopyElements()
        {
            var newArr = new T[this.elements.Length * 2];

            for (int i = 0; i < this.Count; i++)
            {
                newArr[i] = this.elements[(this.startIndex + i) % this.elements.Length];
            }

            return newArr;
        }
    }

}
