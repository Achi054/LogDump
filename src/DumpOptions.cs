using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

// ReSharper disable once CheckNamespace
/// <summary>
/// Object dump configurations
/// </summary>
public class DumpOptions
{
    internal const int MaxDepth = 10;

    public int IndentSize { get; set; } = 2;

    public char IndentChar { get; set; } = ' ';

    public string LineBreakChar { get; set; } = Environment.NewLine;

    public int Depth { get; set; } = MaxDepth;

    public ICollection<string> ExcludeProperties { get; set; } = new HashSet<string>();

    public Expression<Func<PropertyInfo, object>> PropertyOrderBy { get; set; }

    public bool UseTypeFullName { get; set; } = false;
}
