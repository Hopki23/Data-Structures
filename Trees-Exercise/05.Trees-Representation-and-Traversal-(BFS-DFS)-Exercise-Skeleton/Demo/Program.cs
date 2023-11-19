namespace Demo
{
    using System;
    using System.Linq;
    using Tree;

    class Program
    {
        static void Main(string[] args)
        {
            var input = new string[] { "7 19", "7 21", "7 14", "19 1", "19 12", "19 31", "14 23", "14 6", };

            var treeFactory = new IntegerTreeFactory();

            var tree = treeFactory.CreateTreeFromStrings(input);

            Console.WriteLine(tree.AsString());
            Console.WriteLine($"Leaf Keys: {string.Join(" ", tree.GetLeafKeys())}");
            Console.WriteLine($"Internal Keys: {string.Join(" ", tree.GetInternalKeys())}");
            Console.WriteLine($"Deeptest node: {string.Join(" ", tree.GetDeepestKey())}");
            var pathsWithGivenSum = tree.GetPathsWithGivenSum(27);

            foreach (var path in pathsWithGivenSum)
            {
                Console.WriteLine(string.Join(" ", path));
            }
        }
    }
}
