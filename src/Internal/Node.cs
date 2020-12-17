using System;
using System.Collections.Generic;

namespace ObjectDump.Internal
{
    public class Node
    {
        public Node(Type type, object value, int level, Node parent) => (Type, Value, Level, Parent) = (type, value, level, parent);

        public string Name { get; internal set; }

        public Type Type { get; }

        public object Value { get; }

        public Node Parent { get; }

        public int Level { get; }

        public ICollection<Node> Children { get; } = new HashSet<Node>();

        public void AddChild(Node node) => Children.Add(node);
    }
}
