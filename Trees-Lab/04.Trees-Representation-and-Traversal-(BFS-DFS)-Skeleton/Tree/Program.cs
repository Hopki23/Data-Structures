namespace Tree
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var tree = new Tree<int>(34,
                                    new Tree<int>(36,
                                        new Tree<int>(42,
                                            new Tree<int>(50,
                                                new Tree<int>(97))
                                            ),
                                        new Tree<int>(3,
                                            new Tree<int>(78)
                                            )
                                    ),
                                    new Tree<int>(1),
                                    new Tree<int>(103)
                                    );

            tree.Swap(42, 103);
        }
    }
}
