using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day4
{
    struct Card
    {
        public int CardId;
        public IEnumerable<int> WinningNumbers;
        public IEnumerable<int> CardNumbers;
    }

    private IEnumerable<Card> cards;

    public Day4()
    {
        cards = File.ReadLines("C:\\src\\te\\AdventOfCode\\AdventOfCode\\Day4\\input.txt")
        .Select(e =>
        {
            var card = e.Split(":");
            var numbers = card[1].Split("|");

            return new Card
            {
                CardId = int.Parse(string.Join("", card[0].Where(e => char.IsDigit(e)))),
                WinningNumbers = numbers[0].Split(" ")
                                           .Where(e => Regex.IsMatch(e, @"\d"))
                                           .Select(e => int.Parse(e))
                                           .ToArray(),
                CardNumbers = numbers[1].Split(" ")
                                        .Where(e => Regex.IsMatch(e, @"\d"))
                                        .Select(e => int.Parse(e))
                                        .ToArray(),
            };
        });
    }
    public int Part_1()
    {
        var result = 0;
        result = cards.Select(e =>
        {
            int score = 0;
            var numberRange = e.CardNumbers.Intersect(e.WinningNumbers);
            foreach (var n in numberRange)
            {
                if (score == 0)
                    score = 1;
                else
                    score = score * 2;
            }
            return score;
        }).Sum();

        return result;
    }
}
