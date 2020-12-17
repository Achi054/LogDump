namespace ObjectDump.Internal
{
    public abstract class DumpBase
    {
        protected DumpBase(DumpOptions options) => Options = options;

        public DumpOptions Options { get; internal set; }
    }
}
