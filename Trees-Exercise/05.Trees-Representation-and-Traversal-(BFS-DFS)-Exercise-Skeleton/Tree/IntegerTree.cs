namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            var result = new List<List<int>>();

            var currentPath = new List<int>();
            currentPath.Add(this.Key);

            int currentSum = this.Key;

            this.Dfs(this, result, currentPath, ref currentSum, sum);

            return result;
        }

        private void Dfs(Tree<int> node, List<List<int>> result, List<int> currentPath, ref int currentSum, int wantedSum)
        {
            foreach (var child in node.Children)
            {
                currentSum += child.Key;
                currentPath.Add(child.Key);
                this.Dfs(child, result, currentPath, ref currentSum, wantedSum);
            }

            if (currentSum == wantedSum)
            {
                result.Add(new List<int>(currentPath));
            }

            currentSum -= node.Key;
            currentPath.Remove(node.Key);
            //currentPath.RemoveAt(currentPath.Count - 1);
        }

        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }
    }
}
