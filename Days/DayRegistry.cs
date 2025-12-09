using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode.Days;

internal sealed record DayDefinition(string Label, Func<string> Run);

internal static class DayRegistry
{
    public static IReadOnlyList<DayDefinition> Days { get; } = DiscoverDays();

    private static IReadOnlyList<DayDefinition> DiscoverDays()
    {
        var assembly = typeof(DayRegistry).Assembly;

        var dayDefinitions = new List<DayDefinition>();

        foreach (var type in assembly.GetTypes())
        {
            if (type is not { IsClass: true, IsAbstract: true, IsSealed: true })
                continue;

            if (!string.Equals(type.Namespace, "AdventOfCode.Days", StringComparison.Ordinal))
                continue;

            if (!type.Name.StartsWith("Day", StringComparison.OrdinalIgnoreCase))
                continue;

            var runMethod = type.GetMethod(
                "Run",
                BindingFlags.Public | BindingFlags.Static,
                binder: null,
                types: Type.EmptyTypes,
                modifiers: null
            );

            if (runMethod is null || runMethod.ReturnType != typeof(string))
                continue;

            var runDelegate = (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), runMethod);

            var label = BuildLabelFromTypeName(type.Name);

            dayDefinitions.Add(new DayDefinition(label, runDelegate));
        }

        return dayDefinitions
            .OrderBy(d => d.Label, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static string BuildLabelFromTypeName(string typeName)
    {
        if (typeName.Length <= 3)
            return typeName;

        var suffix = typeName.Substring(3);
        return $"Day {suffix}";
    }
}
