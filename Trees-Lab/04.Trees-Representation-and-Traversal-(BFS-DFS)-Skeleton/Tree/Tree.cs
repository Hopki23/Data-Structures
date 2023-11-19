namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;
        private T Value;
        private Tree<T> parent;

        public Tree(T value)
        {
            this.Value = value;
            this.children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.parent = this;
                this.children.Add(child);
            }
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var parentNode = this.FindParentNodeUsingBfs(parentKey);

            if (parentNode == null)
            {
                throw new ArgumentNullException();
            }

            parentNode.children.Add(child);
            child.parent = parentNode;
        }

        public IEnumerable<T> OrderBfs()
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<T>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                result.Add(node.Value);

                foreach (var child in node.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            var list = new List<T>();
            this.Dfs(this, list);
            return list;
        }

        public void RemoveNode(T nodeKey)
        {
            var nodeToRemove = this.FindNodeUsingDfs(nodeKey);

            if (nodeToRemove == null)
            {
                throw new ArgumentNullException();
            }

            var parentNode = nodeToRemove.parent;

            if (parentNode is null)
            {
                throw new ArgumentException();
            }

            parentNode.children.Remove(nodeToRemove);
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindNodeUsingDfs(firstKey);
            var secondNode = this.FindNodeUsingDfs(secondKey);

            if (firstNode == null || secondNode == null)
            {
                throw new ArgumentNullException();
            }

            var firstParent = firstNode.parent;
            var secondParent = secondNode.parent;

            if (firstParent == null || secondParent == null)
            {
                throw new ArgumentException();
            }

            var indexOfFirstChild = firstParent.children.IndexOf(firstNode);
            var indexOfSecondChild = secondParent.children.IndexOf(secondNode);

            firstParent.children[indexOfFirstChild] = secondNode;
            firstNode.parent = secondParent;

            secondParent.children[indexOfSecondChild] = firstNode;
            secondNode.parent = firstParent;
        }
        private Tree<T> FindNodeUsingDfs(T nodeKey)
        {
            var result = new Stack<T>();
            var stack = new Stack<Tree<T>>();

            stack.Push(this);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node.Value.Equals(nodeKey))
                {
                    return node;
                }

                foreach (var child in node.children)
                {
                    stack.Push(child);
                }

                result.Push(node.Value);
            }

            return null;
        }

        private Tree<T> FindParentNodeUsingBfs(T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<T>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                result.Add(node.Value);

                if (node.Value.Equals(parentKey))
                {
                    return node;
                }

                foreach (var child in node.children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }
        private void Dfs(Tree<T> node, ICollection<T> result)
        {
            foreach (var child in node.children)
            {
                Dfs(child, result);
            }

            result.Add(node.Value);
        }
        private IEnumerable<T> DfsWithStack()
        {
            var result = new Stack<T>();
            var stack = new Stack<Tree<T>>();

            stack.Push(this);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                result.Push(node.Value);

                foreach (var child in node.children)
                {
                    stack.Push(child);
                }
            }

            return result;
        }
    }
}
