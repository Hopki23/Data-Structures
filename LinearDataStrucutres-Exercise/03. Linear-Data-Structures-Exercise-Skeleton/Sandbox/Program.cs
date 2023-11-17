namespace Sandbox
{
    using System;
    using System.Linq;
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());

            var queue = new Queue<int>();

            for (int i = 1; i <= 50; i++)
            {
                if (i == 1)
                {
                    queue.Enqueue(input);
                }
                else if (i % 2 == 0)
                {
                    int curr = queue.Peek();
                    queue.Enqueue(curr + 1);
                }
                else
                {

                }
            }
        }
    }
}