using System.Text.RegularExpressions;
public class ParseDice
{
    public static int[] Parse(string dice)
    {
        string[] parts = dice.Split(',');
        if (parts.Length != 6)
        {
            throw new ArgumentException("Each dice set must contain 6 values.");
        }

        return Array.ConvertAll(parts, int.Parse);
    }



    public static bool IsValidDiceSet(string[] args)
    {
        string checkreg = @"^(-?\d+,){5}-?\d+$";

        if (args.Length < 3)
        {
            Console.WriteLine("Please provide 3 or more dice sets.");
            return false;
        }

        foreach (string item in args)
        {
            if (string.IsNullOrWhiteSpace(item) || !Regex.IsMatch(item.Trim(), checkreg))
            {
                Console.WriteLine($"Invalid dice set: {item} Try to provide integer values and sides should be exactly 6.");
                return false;
            }
        }

        return true;
    }


}