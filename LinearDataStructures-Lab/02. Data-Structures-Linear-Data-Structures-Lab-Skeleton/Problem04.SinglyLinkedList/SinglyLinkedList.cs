namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public T Element { get; set; }
            public Node Next { get; set; }

            public Node(T element, Node next)
            {
                this.Element = element;
                this.Next = next;
            }
            public Node(T element)
            {
                this.Element = element;
            }
        }

        private Node head;

        public int Count{ get; private set; }

        public void AddFirst(T item)
        {
            var node = new Node(item, this.head);
            this.head = node;
            Count++;
        }

        public void AddLast(T item)
        {
            if (this.head == null)
            {
                this.head = new Node(item, this.head);
            }
            else
            {
                var node = this.head;

                while (node.Next != null)
                {
                    node = node.Next;
                }

                node.Next = new Node(item);
            }

            Count++;
        }
        public T GetFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            
            return this.head.Element;
        }

        public T GetLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            var node = this.head;

            while (node.Next != null)
            {
                node = node.Next;
            }

            return node.Element;
        }

        public T RemoveFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            var newHead = this.head;
            this.head = newHead.Next;

            Count--;

            return newHead.Element;
        }

        public T RemoveLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            
            var oldTop = this.head;
            Node previous = default;

            while (oldTop.Next != null)
            {
                previous = oldTop;
                oldTop = oldTop.Next;
            }


            this.head = previous;

            Count--;

            return oldTop.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;

            while (node != null)
            {
                yield return node.Element;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}