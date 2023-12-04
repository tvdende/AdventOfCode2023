namespace AdventOfCode;

public class Day1
{
    private IEnumerable<string> lines;

    public Day1()
    {
        lines = File.ReadLines("C:\\src\\te\\AdventOfCode\\AdventOfCode\\Day1\\input.txt");
    }

    public int Part_1()
    {
        var result = 0;
        foreach (var line in lines)
        {
            var digits = line
                .Where(e => char.IsDigit(e));
            result += int.Parse(string.Concat(digits.First(), digits.Last()));
        }
        return result;
    }
    
    public int Part_2()
    {
        var result = 0;
        foreach (var line in lines)
        {
            var curLine = line.Replace("one", "o1e", StringComparison.InvariantCultureIgnoreCase)
            .Replace("two", "t2o", StringComparison.InvariantCultureIgnoreCase)
            .Replace("three", "t3e", StringComparison.InvariantCultureIgnoreCase)
            .Replace("four", "f4r", StringComparison.InvariantCultureIgnoreCase)
            .Replace("five", "f5e", StringComparison.InvariantCultureIgnoreCase)
            .Replace("six", "s6x", StringComparison.InvariantCultureIgnoreCase)
            .Replace("seven", "s7n", StringComparison.InvariantCultureIgnoreCase)
            .Replace("eight", "e8t", StringComparison.InvariantCultureIgnoreCase)
            .Replace("nine", "n9e", StringComparison.InvariantCultureIgnoreCase);
            var digits = curLine
                .Where(e => char.IsDigit(e));
            result += int.Parse(string.Concat(digits.First(), digits.Last()));
        }
        return result;
    }
}
