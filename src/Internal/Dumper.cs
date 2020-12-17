using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ObjectDump.Internal
{
    /// <summary>
    /// Object dumper class to convert object to tree view.
    /// </summary>
    internal class Dumper : DumpBase
    {
        private readonly ISet<int> _hashListOfFoundElements;
        private int _level;
        private readonly DumpObject _dumpObject;

        private int Level
        {
            get => _level;
            set => _level = value < 0
                ? throw new ArgumentException("Level must not be a negative number", nameof(Level))
                : value;
        }

        private bool IsMaxLevel
            => Level >= Options.Depth;

        internal Dumper(DumpOptions options)
            : base(options) => (_level, _hashListOfFoundElements, _dumpObject) = (0, new HashSet<int>(), new DumpObject(options));

        internal DumpObject Dump(object element)
        {
            _dumpObject.ObjectView = ComposeNode(element, null);
            return _dumpObject;
        }

        /// <summary>
        /// Convert object to node(s).
        /// </summary>
        /// <param name="value">Object.</param>
        /// <param name="node">Parent node.</param>
        private void SetNode(object value, Node node)
        {
            AddAlreadyTouched(value);

            var type = value.GetType();

            Level++;

            var properties = type.GetProperties().ToList();

            if (properties.Any())
            {
                if (Options.ExcludeProperties != null && Options.ExcludeProperties.Any())
                    properties = properties
                        .Where(p => !Options.ExcludeProperties.Contains(p.Name))
                        .ToList();

                if (Options.PropertyOrderBy != null)
                    properties = properties.OrderBy(Options.PropertyOrderBy.Compile()).ToList();

                properties.ForEach(property =>
                {
                    var propertyValue = property.GetValue(value);

                    if (AlreadyTouched(propertyValue))
                        return;

                    var childNode = ComposeNode(propertyValue, node);
                    if (!Equals(node, childNode))
                    {
                        childNode.Name = property.Name;
                        node.AddChild(childNode);
                    }
                });
            }

            Level--;
        }

        /// <summary>
        /// Object to node converter base on type.
        /// </summary>
        /// <param name="value">Object to convert.</param>
        /// <param name="parentNode">Parent node.</param>
        /// <returns></returns>
        private Node ComposeNode(object value, Node parentNode)
        {
            if (IsMaxLevel)
                return CreateNode(value, parentNode);

            var typeInfo = value.GetType().GetTypeInfo();
            if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                return CreateNode(value, parentNode);

            return value switch
            {
                null or bool or string or char or byte or sbyte => CreateNode(value, parentNode),
                short s => CreateNode(s, parentNode),
                ushort u => CreateNode(u, parentNode),
                int i => CreateNode(i, parentNode),
                uint ui => CreateNode(ui, parentNode),
                long l => CreateNode(l, parentNode),
                ulong ul => CreateNode(ul, parentNode),
                double d => CreateNode(d, parentNode),
                float f => CreateNode(f, parentNode),
                decimal de => CreateNode(de, parentNode),
                DateTime dt => CreateNode(dt, parentNode),
                DateTimeOffset dto => CreateNode(dto, parentNode),
                TimeSpan ts => CreateNode(ts, parentNode),
                CultureInfo ci => CreateNode(ci, parentNode),
                Guid g => CreateNode(g, parentNode),
                Type t => CreateNode(t.GetFormattedName(Options.UseTypeFullName), parentNode),
                Enum e => EnumNode(e),
                IEnumerable en => CollectionNode(en),
                ITuple t => TupleNode(t),
                _ => ObjectNode(value),
            };

            Node EnumNode(Enum e)
            {
                var enumFlags = $"{value}".Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var enumTypeName = e.GetType().GetFormattedName(Options.UseTypeFullName);
                // In case of multiple flags, we prefer short class name here
                var enumValues = string.Join(" | ", enumFlags.Select(f => $"{(enumFlags.Length > 1 ? "" : $"{enumTypeName}.")}{f.Replace(" ", "")}"));
                return CreateNode(enumValues, parentNode);
            }

            Node CollectionNode(IEnumerable collection)
            {
                var currentNode = CreateNode(value, parentNode);
                currentNode.Name = value.GetType().GetFormattedName(Options.UseTypeFullName);
                parentNode?.AddChild(currentNode);

                var enumerator = collection.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    currentNode.AddChild(ComposeNode(enumerator.Current, currentNode));
                }

                return parentNode ??= currentNode;
            }

            Node ObjectNode(object obj)
            {
                var nextNode = CreateNode(obj, parentNode);
                nextNode.Name = Options.UseTypeFullName ? obj.GetType().FullName : obj.GetType().Name;
                SetNode(obj, nextNode);
                return nextNode;
            }

            Node TupleNode(ITuple tuple)
            {
                var nextNode = CreateNode(tuple, parentNode);
                nextNode.Name = tuple.GetType().GetFormattedName(Options.UseTypeFullName);
                return nextNode;
            }
        }

        private Node CreateNode(object value, Node node)
            => new Node(value.GetType(), value, Level, node);

        private void AddAlreadyTouched(object element)
            => _hashListOfFoundElements.Add(element.GetHashCode());

        private bool AlreadyTouched(object value)
        {
            if (value == null) return false;

            var hash = value.GetHashCode();

            return _hashListOfFoundElements.Contains(hash);
        }
    }
}
