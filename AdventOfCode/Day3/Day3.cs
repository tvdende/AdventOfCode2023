namespace AdventOfCode;

public class Day3
{
    struct Coordinate
    {
        public int x;
        public int y;

        public bool IsNextTo(Coordinate b)
        {
            return x - b.x == -1
                || x - b.x == 1;
        }
    }

    private char[][] lines;

    public Day3()
    {
        lines = File.ReadLines("C:\\src\\te\\AdventOfCode\\AdventOfCode\\Day3\\input.txt")
            .Select(e => e.ToCharArray())
            .ToArray();
    }

    public int Part_1()
    {
        var result = 0;
        var allSymbols = lines
            .SelectMany(e => e)
            .Where(e => !char.IsLetterOrDigit(e))
            .Where(e => e != '.')
            .Distinct();

        List<Coordinate?> posibleCoordinates = new List<Coordinate?>();
        for (int y = 0; y < lines.Count(); y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (allSymbols.Contains(lines[y][x]))
                {
                    // left right
                    posibleCoordinates.Add(LookIfDigit(x - 1, y));
                    posibleCoordinates.Add(LookIfDigit(1 + x, y));
                    // up left right
                    posibleCoordinates.Add(LookIfDigit(x, y + 1));
                    posibleCoordinates.Add(LookIfDigit(x - 1, y + 1));
                    posibleCoordinates.Add(LookIfDigit(x + 1, y + 1));

                    // Down left right
                    posibleCoordinates.Add(LookIfDigit(x, y - 1));
                    posibleCoordinates.Add(LookIfDigit(x - 1, y - 1));
                    posibleCoordinates.Add(LookIfDigit(x + 1, y - 1));
                }
            }
        }
        
        var coordinates = posibleCoordinates.Where(e => e.HasValue)
                                            .Select(e => e.Value)
                                            .ToList();
        var more = new List<Coordinate>();
        foreach (var coordinate in coordinates)
        {
            var numberRange = new List<Coordinate>();
            var x = coordinate.x;

            more.Add(new Coordinate { x = x, y = coordinate.y });
            while (LookIfDigit(--x, coordinate.y) != null)
            {
                more.Add(new Coordinate { x = x, y = coordinate.y });
            }
            x = coordinate.x;
            while (LookIfDigit(++x, coordinate.y) != null)
            {
                more.Add(new Coordinate { x = x, y = coordinate.y });
            }

        }
        string b = null;
        Coordinate? previous = null;
        var orderdList = more.Distinct()
                              .OrderBy(e => e.y)
                              .ThenBy(e => e.x);
        foreach (var a in orderdList)
        {
            if (previous.HasValue)
            {
                if (previous.Value.IsNextTo(a))
                {
                    b += lines[a.y][a.x].ToString();
                    if (orderdList.Last().y == a.y && orderdList.Last().x == a.x)
                        result += int.Parse(b);
                }
                else
                {
                    if (b != null)
                    {
                        result += int.Parse(b);
                    }
                    b = lines[a.y][a.x].ToString();
                }
            }
            else
                b = lines[a.y][a.x].ToString();
            previous = a;
        }
        return result;
    }

    private Coordinate? LookIfDigit(int x, int y)
    {
        if (y >= 0
         && y < lines.Length
         && x >= 0
         && x < lines[y].Length)
        {
            var a = lines[y][x];
            if (char.IsDigit(a))
            {
                return new Coordinate { x = x, y = y };
            }
        }
        return null;
    }
}
