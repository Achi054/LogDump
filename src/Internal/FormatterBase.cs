using System.Text;

namespace ObjectDump.Internal
{
    public abstract class FormatterBase
    {
        private bool _isNewLine;
        protected StringBuilder StringBuilder;

        public DumpOptions DumpSetting { get; }

        protected FormatterBase(DumpOptions options) => (DumpSetting, StringBuilder, _isNewLine) = (options, new StringBuilder(), true);

        protected void Write(string name, string value, int indentLevel)
        {
            StringBuilder.Append(DumpSetting.IndentChar, indentLevel * DumpSetting.IndentSize);
            StringBuilder.Append(name is null ? (indentLevel < DumpSetting.Depth ? value : $"{value}: ...") : $"{name}: {value}");
            _isNewLine = value.EndsWith(DumpSetting.LineBreakChar);
            LineBreak();
        }

        private void LineBreak()
        {
            StringBuilder.Append(DumpSetting.LineBreakChar);
            _isNewLine = true;
        }
    }
}
