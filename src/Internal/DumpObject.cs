namespace ObjectDump.Internal
{
    /// <summary>
    /// Model that represents the object converted as dump object.
    /// </summary>
    public class DumpObject : DumpBase
    {
        public DumpObject(DumpOptions options)
            : base(options) { }

        public Node ObjectView { get; internal set; }

        public override string ToString()
            => new StringFormatter(Options).ConvertToString(ObjectView);
    }
}
