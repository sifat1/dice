using System;
using System.Linq;
using System.Collections.Generic;

public class DiceGame
{
    private CryptographicService cryptoService;
    private Player player = new("Player");
    private Player computer = new("Computer");

    private List<Dice> diceList = new();
    private List<Dice> availableDice = new();
    private Dictionary<Dice, string> diceLabels = new();

    public DiceGame(string[] diceSets, CryptographicService cryptoService)
    {
        this.cryptoService = cryptoService;

        foreach (var diceSet in diceSets)
        {
            var dice = new Dice(ParseDice.Parse(diceSet));
            diceList.Add(dice);
            diceLabels[dice] = diceSet;
        }

        ResetAvailableDice();
    }

    private void ResetAvailableDice() => availableDice = new List<Dice>(diceList);

    public void StartGame()
    {
        var ui = new GameUI(diceLabels.Values.ToArray());

        ui.DisplayMessage("Let's determine who makes the first move.");
        var (key, computerNumber, hmac) = cryptoService.GenerateCommitment(0, 1);
        ui.DisplayCommitment(hmac, 0, 1);

        int userGuess = ui.GetUserInput(0, 1);
        ui.RevealNumber(computerNumber, key);

        bool userGoesFirst = userGuess == computerNumber;
        ui.DisplayFirstMoveResult(userGoesFirst);

        Dice playerDice, computerDice;

        if (userGoesFirst)
        {
            playerDice = SelectPlayerDice(ui);
            computerDice = SelectComputerDice();
        }
        else
        {
            computerDice = SelectComputerDice();
            playerDice = SelectPlayerDice(ui);
        }

        ui.DisplayDiceChoice(diceLabels[playerDice], player.name);
        ui.DisplayDiceChoice(diceLabels[computerDice], computer.name);

        player.score = PlayRound(playerDice, player.name, ui);
        computer.score = PlayRound(computerDice, computer.name, ui);

        ui.DisplayGameResult(player.score, computer.score);
    }

    private Dice SelectPlayerDice(GameUI ui)
    {
        var displayOptions = availableDice.Select(d => d.ToString()).ToArray();
        int choice = ui.GetDiceSelection(displayOptions, player.name);
        var selected = availableDice[choice];
        availableDice.RemoveAt(choice);
        return selected;
    }

    private Dice SelectComputerDice()
    {
        int choice = cryptoService.GenerateUniformRandomInt(0, availableDice.Count - 1);
        var selected = availableDice[choice];
        availableDice.RemoveAt(choice);
        return selected;
    }

    private int PlayRound(Dice dice, string playerName, GameUI ui)
    {
        ui.DisplayRollPrompt(playerName);
        int[] values = dice.diceSet;
        int range = values.Length;

        var (key, computerNumber, hmac) = cryptoService.GenerateCommitment(0, range - 1);
        ui.DisplayCommitment(hmac, 0, range - 1);

        int userNumber = ui.GetUserInput(0, range - 1);
        ui.RevealNumber(computerNumber, key);

        int result = (computerNumber + userNumber) % range;
        ui.DisplayModularResult(computerNumber, userNumber, result, range);

        int roll = values[result];
        ui.DisplayRollResult(roll, playerName);
        return roll;
    }
}
