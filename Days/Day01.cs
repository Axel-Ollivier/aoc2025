using System.Text;

namespace aoc2025.Days;

public static class Day01
{
    public static string Run()
    {
        var sb = new StringBuilder();
        var inputPath = Path.Combine("Inputs", "day01.txt");

        if (!File.Exists(inputPath))
        {
            sb.AppendLine($"Input file not found: {inputPath}");
            return sb.ToString();
        }

        var input = File.ReadAllText(inputPath);

        var part1 = SolvePart1(input);
        var part2 = SolvePart2(input);

        sb.AppendLine($"Part 1: {part1}");
        sb.AppendLine($"Part 2: {part2}");

        return sb.ToString();
    }

    private static int SolvePart1(string input)
    {
        var lines = input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        const int modulo = 100;
        var position = 50;
        var zeroCount = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var dir = line[0];
            if (!int.TryParse(line.AsSpan(1), out var distance))
            {
                throw new InvalidOperationException($"Invalid line format: \"{line}\"");
            }

            position = dir switch
            {
                'L' or 'l' => Mod(position - distance, modulo),
                'R' or 'r' => Mod(position + distance, modulo),
                _ => throw new InvalidOperationException(
                    $"Unknown rotation direction '{dir}' in line \"{line}\"")
            };

            if (position == 0)
            {
                zeroCount++;
            }
        }

        return zeroCount;
    }

    private static long SolvePart2(string input)
    {
        var lines = input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        const int modulo = 100;
        var position = 50;
        long zeroCount = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var dir = line[0];
            if (!long.TryParse(line.AsSpan(1), out var distance))
            {
                throw new InvalidOperationException($"Invalid line format: \"{line}\"");
            }

            if (distance <= 0)
                continue;

            for (long i = 0; i < distance; i++)
            {
                position = dir switch
                {
                    'L' or 'l' => Mod(position - 1, modulo),
                    'R' or 'r' => Mod(position + 1, modulo),
                    _ => throw new InvalidOperationException(
                        $"Unknown rotation direction '{dir}' in line \"{line}\"")
                };

                if (position == 0)
                {
                    zeroCount++;
                }
            }
        }

        return zeroCount;
    }

    private static int Mod(int value, int modulo)
    {
        var r = value % modulo;
        return r < 0 ? r + modulo : r;
    }
}
