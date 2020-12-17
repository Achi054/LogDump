using System;
using ObjectDump.Internal;

namespace ObjectDump
{
    /// <summary>
    /// Extension method to render object as tree view.
    /// </summary>
    public static class ObjectDump
    {
        /// <summary>
        /// Extension method to render object as tree view.
        /// </summary>
        /// <typeparam name="T">Target object type.</typeparam>
        /// <param name="obj">Target object.</param>
        /// <param name="depth">Object level.</param>
        /// <returns></returns>
        public static DumpObject Dump<T>(this T obj, int? depth = null)
        {
            return new Dumper(new DumpOptions
            {
                Depth = depth ?? DumpOptions.MaxDepth
            }).Dump(obj);
        }

        /// <summary>
        /// Extension method to render object as tree view.
        /// </summary>
        /// <typeparam name="T">Target object type.</typeparam>
        /// <param name="obj">Target object.</param>
        /// <param name="dumpOptions">Options to render object as tree view.</param>
        /// <returns></returns>
        public static DumpObject Dump<T>(this T obj, Action<DumpOptions> dumpOptions)
        {
            var options = new DumpOptions();

            dumpOptions.Invoke(options);

            return new Dumper(options).Dump(obj);
        }
    }
}
