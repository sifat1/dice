using System;

public class GameUI
{

    private List<Dice> diceList = new();

    public GameUI(List<Dice> availableDice)
    {
        diceList = availableDice;
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void DisplayCommitment(string hmac, int min, int max)
    {
        Console.WriteLine($"I selected a random value in range {min}..{max} (HMAC={hmac}).");
        Console.WriteLine("Try to guess my selection:");
        for (int i = min; i <= max; i++)
            Console.WriteLine($"{i} - {i}");
        Console.WriteLine("X - exit");
        Console.WriteLine("? - help");
    }

    public int GetUserInput(int min, int max)
    {
        while (true)
        {
            Console.Write("Your selection: ");
            string input = Console.ReadLine().Trim();
            
            if (input.Equals("X", StringComparison.OrdinalIgnoreCase))
                Environment.Exit(0);
            else if (input == "?")
                ShowHelp(diceList);
            
            else if (int.TryParse(input, out int result) && result >= min && result <= max)
                return result;
            else
            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    public int GetDiceSelection(string[] diceOptions, string player)
    {
        Console.WriteLine($"\nAvailable {player} dice:");
        for (int i = 0; i < diceOptions.Length; i++)
        {
            Console.WriteLine($"{i} - [{diceOptions[i]}]");
        }
        return GetUserInput(0, diceOptions.Length - 1);
    }

    public void RevealNumber(int number, byte[] key)
    {
        Console.WriteLine($"My selection: {number} (KEY={BitConverter.ToString(key).Replace("-", "")}).");
    }

    public void DisplayFirstMoveResult(bool userGoesFirst)
    {
        Console.WriteLine(userGoesFirst 
            ? "You guessed correctly! You make the first move."
            : "You didn't guess. I make the first move.");
    }

    public void DisplayDiceChoice(string dice, string player)
    {
        Console.WriteLine($"{player} chooses [{dice}].");
    }

    public void DisplayRollPrompt(string player)
    {
        Console.WriteLine($"\nIt's time for {(player == "Player" ? "your" : "my")} roll.");
    }

    public void DisplayModularResult(int computerNumber, int userNumber, int result, int modulus)
    {
        Console.WriteLine($"The fair number generation result is {computerNumber} + {userNumber} = {result} (mod {modulus}).");
    }

    public void DisplayRollResult(int result, string player)
    {
        Console.WriteLine($"{(player == "Player" ? "Your" : "My")} roll result is {result}.");
    }

    public void DisplayGameResult(int playerScore, int computerScore)
    {   
        if (playerScore > computerScore)
            Console.WriteLine($"You win {playerScore} > {computerScore}!");
        else if (playerScore < computerScore)
            Console.WriteLine($"I win {playerScore} < {computerScore}!");
        else
            Console.WriteLine("It's a tie!");
    }

    private void ShowHelp(List<Dice> dices)
    {
        ProbabilityTableGenerator.DisplayProbabilityTable(diceList);
    }
}