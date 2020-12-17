using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ObjectDump.Internal
{
    internal class StringFormatter : FormatterBase
    {
        internal StringFormatter(DumpOptions options)
            : base(options) { }

        internal string ConvertToString(Node objectView)
        {
            if (objectView is null)
                return string.Empty;

            FormatValue(objectView);

            return StringBuilder.ToString();
        }

        private void FormatValue(Node node)
        {
            switch (node.Value)
            {
                case null:
                    Write(node.Name, "null", node.Level);
                    break;
                case bool value:
                    Write(node.Name, $"{value.ToString().ToLower()}", node.Level);
                    break;
                case string value:
                    var str = $@"{value}".Escape();
                    Write(node.Name, $"\"{str}\"", node.Level);
                    break;
                case char value:
                    var c = value.ToString().Replace("\0", "").Trim();
                    Write(node.Name, $"\'{c}\'", node.Level);
                    break;
                case byte or sbyte:
                    Write(node.Name, $"{node.Value}", node.Level);
                    break;
                case short value:
                    Write(node.Name, $"{value.ToString(CultureInfo.InvariantCulture)}", node.Level);
                    break;
                case ushort value:
                    Write(node.Name, $"{value.ToString(CultureInfo.InvariantCulture)}", node.Level);
                    break;
                case int value:
                    Write(node.Name, $"{value.ToString(CultureInfo.InvariantCulture)}", node.Level);
                    break;
                case long value:
                    Write(node.Name, $"{value.ToString(CultureInfo.InvariantCulture)}", node.Level);
                    break;
                case ulong value:
                    Write(node.Name, $"{value.ToString(CultureInfo.InvariantCulture)}", node.Level);
                    break;
                case double value:
                    Write(node.Name, $"{value.ToString(CultureInfo.InvariantCulture)}", node.Level);
                    break;
                case decimal value:
                    Write(node.Name, $"{value.ToString(CultureInfo.InvariantCulture)}", node.Level);
                    break;
                case float value:
                    Write(node.Name, $"{value.ToString(CultureInfo.InvariantCulture)}", node.Level);
                    break;
                case DateTime value:
                    Write(node.Name, $"{value}", node.Level);
                    break;
                case DateTimeOffset value:
                    Write(node.Name, $"{value:O}", node.Level);
                    break;
                case TimeSpan value:
                    Write(node.Name, $"{value:c}", node.Level);
                    break;
                case CultureInfo value:
                    Write(node.Name, $"{value}", node.Level);
                    break;
                case Enum value:
                    Write(node.Name, $"{value}", node.Level);
                    break;
                case Guid value:
                    Write(node.Name, $"{value:B}", node.Level);
                    break;
                case Type value:
                    Write(node.Name, $"{value:B}", node.Level);
                    break;
                case ITuple value:
                    Write(node.Name, $"{value}", node.Level);
                    break;
                default:
                    Write(null, node.Name, node.Level);
                    node.Children.ToList().ForEach(FormatValue);
                    break;
            }
        }
    }
}
