namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this.children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string AsString()
        {
            var sb = new StringBuilder();

            this.DfsAsString(sb, this, 0);

            return sb.ToString().Trim();
        }

        public IEnumerable<T> GetInternalKeys()
        {
            return this.BfsSearch(x => x.children.Count > 0 && x.Parent != null)
                 .Select(x => x.Key);
        }

        public IEnumerable<T> GetLeafKeys()
        {
            return this.BfsSearch(x => x.children.Count == 0)
                 .Select(x => x.Key);
        }

        public T GetDeepestKey()
        {
            var leafs = this.BfsSearch(x => x.children.Count == 0);
            return FindDeepestNode(leafs).Key;
        }

        public IEnumerable<T> GetLongestPath()
        {
            var leafs = this.BfsSearch(x => x.children.Count == 0);
            var deepestNode = this.FindDeepestNode(leafs);

            var result = new Stack<T>();

            while (deepestNode.Parent != null)
            {
                result.Push(deepestNode.Key);

                deepestNode = deepestNode.Parent;
            }

            result.Push(deepestNode.Key);

            return result;
        }

        private void DfsAsString(StringBuilder sb, Tree<T> tree, int depth)
        {
            sb.Append(' ', depth)
              .AppendLine(tree.Key.ToString());

            foreach (var child in tree.children)
            {
                DfsAsString(sb, child, depth + 2);
            }
        }

        private IEnumerable<Tree<T>> BfsSearch(Predicate<Tree<T>> predicate)
        {
            var result = new List<Tree<T>>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                if (predicate.Invoke(currentNode))
                {
                    result.Add(currentNode);
                }

                foreach (var child in currentNode.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        private Tree<T> FindDeepestNode(IEnumerable<Tree<T>> leafs)
        {
            Tree<T> deepestNode = null;
            int maxDepth = 0;

            foreach (var leaf in leafs)
            {
                int depth = this.GetCurrentDepth(leaf);

                if (depth > maxDepth)
                {
                    maxDepth = depth;
                    deepestNode = leaf;
                }
            }

            return deepestNode;
        }

        private int GetCurrentDepth(Tree<T> leaf)
        {
            int depth = 0;
            var tree = leaf;

            while (tree.Parent != null)
            {
                depth++;
                tree = tree.Parent;
            }

            return depth;
        }
    }
}
