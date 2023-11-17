namespace Testing
{
    using Problem01.CircularQueue;
    using Problem02.DoublyLinkedList;

    internal class Program
    {
        static void Main(string[] args)
        {
            //CircularQueue<int> queue = new CircularQueue<int>();

            //queue.Enqueue(1);
            //queue.Enqueue(2);
            //queue.Enqueue(3);
            //queue.Enqueue(4);
            //queue.Dequeue();
            //queue.Enqueue(5);
            //queue.Enqueue(6);

            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            list.AddFirst(5);
            list.AddFirst(3);
        }
    }
}