namespace AdventOfCode;

public class Day2
{
    struct Dices
    {
        public int Red;
        public int Green;
        public int Blue;

        public int Power()
        {
            return Red * Green * Blue;
        }
        public override string ToString()
        {
            return $"Red {Red}, Green {Green}, Blue {Blue}";
        }
    }

    private IEnumerable<string> lines;

    public Day2()
    {
        lines = File.ReadLines("C:\\src\\te\\AdventOfCode\\AdventOfCode\\Day2\\input.txt");
    }
    
    public int Part_1()
    {
        var DiceInGame = new Dices { Red = 12, Green = 13, Blue = 14 };
        var result = 0;
        foreach (var line in lines)
        {
            var gamePosible = true;
            var game = line.Split(":");
            var gameID = int.Parse(string.Join("", game[0].Where(e => char.IsDigit(e))));
            var hands = game[1].Split(";");
            foreach (var hand in hands)
            {
                var dices = hand.Split(",");
                foreach (var dice in dices)
                {
                    switch (dice)
                    {
                        case string s when s.Contains("Red", StringComparison.InvariantCultureIgnoreCase):
                            if (int.Parse(string.Join("", s.Where(e => char.IsDigit(e)))) > DiceInGame.Red)
                            {
                                gamePosible = false;
                            }
                            break;
                        case string s when s.Contains("Green", StringComparison.InvariantCultureIgnoreCase):
                            if (int.Parse(string.Join("", s.Where(e => char.IsDigit(e)))) > DiceInGame.Green)
                            {
                                gamePosible = false;
                            }
                            break;
                        case string s when s.Contains("Blue", StringComparison.InvariantCultureIgnoreCase):
                            if (int.Parse(string.Join("", s.Where(e => char.IsDigit(e)))) > DiceInGame.Blue)
                            {
                                gamePosible = false;
                            }
                            break;
                    }
                }
            }
            result += gamePosible ? gameID : 0;
        }
        return result;
    }
    public int Part_2()
    {
        var result = 0;

        foreach (var line in lines)
        {
            Dices dicesNeeded = new Dices { Red = 0, Green = 0, Blue = 0 };
            var game = line.Split(":");
            var gameID = int.Parse(string.Join("", game[0].Where(e => char.IsDigit(e))));
            var hands = game[1].Split(";");
            foreach (var hand in hands)
            {
                var dices = hand.Split(",");
                foreach (var dice in dices)
                {
                    switch (dice)
                    {
                        case string s when s.Contains("Red", StringComparison.InvariantCultureIgnoreCase):
                            var redDiceInHand = int.Parse(string.Join("", s.Where(e => char.IsDigit(e))));
                            dicesNeeded.Red = dicesNeeded.Red < redDiceInHand ? redDiceInHand : dicesNeeded.Red;
                            break;
                        case string s when s.Contains("Green", StringComparison.InvariantCultureIgnoreCase):
                            var greenDiceInHand = int.Parse(string.Join("", s.Where(e => char.IsDigit(e))));
                            dicesNeeded.Green = dicesNeeded.Green < greenDiceInHand ? greenDiceInHand : dicesNeeded.Green;
                            break;
                        case string s when s.Contains("Blue", StringComparison.InvariantCultureIgnoreCase):
                            var blueDiceInHand = int.Parse(string.Join("", s.Where(e => char.IsDigit(e))));
                            dicesNeeded.Blue = dicesNeeded.Blue < blueDiceInHand ? blueDiceInHand : dicesNeeded.Blue;
                            break;
                    }
                }
            }
            result += dicesNeeded.Power();
        }

        return result;
    }
}
