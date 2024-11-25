namespace LimitCallersBasedOnIP;

public static class StringExtensions
{
    public static IEnumerable<string> GetLines(this string str, bool removeEmptyLines = false)
    {
        using var sr = new StringReader(str);
        while (sr.ReadLine() is { } line)
        {
            if (removeEmptyLines && string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            yield return line;
        }
    }
}