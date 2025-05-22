using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

public class DiceGame
{
    private CryptographicService cryptoService;
    private Player player = new("Player");
    private Player computer = new("Computer");

    private List<Dice> diceList = new();
    private List<int> availableDiceIndices = new(); 

    public DiceGame(string[] diceSets, CryptographicService cryptoService)
    {
        this.cryptoService = cryptoService;

        foreach (var diceSet in diceSets)
        {
            var dice = new Dice(ParseDice.Parse(diceSet));
            diceList.Add(dice);
        }

        ResetAvailableDice();
    }

    private void ResetAvailableDice()
    {
        availableDiceIndices = Enumerable.Range(0, diceList.Count).ToList();
    }

    public void StartGame()
    {
        var ui = new GameUI(diceList);

        ui.DisplayMessage("Let's determine who makes the first move.");
        var (key, computerNumber, hmac) = cryptoService.GenerateCommitment(0, 1);
        ui.DisplayCommitment(hmac, 0, 1);

        int userGuess = ui.GetUserInput(0, 1);
        ui.RevealNumber(computerNumber, key);

        bool userGoesFirst = userGuess == computerNumber;
        ui.DisplayFirstMoveResult(userGoesFirst);

        int playerIndex, computerIndex;

        if (userGoesFirst)
        {
            playerIndex = SelectPlayerDice(ui);
            computerIndex = SelectComputerDice();
        }
        else
        {
            computerIndex = SelectComputerDice();
            playerIndex = SelectPlayerDice(ui);
        }

        ui.DisplayDiceChoice(diceList[playerIndex].ToString(), player.name);
        ui.DisplayDiceChoice(diceList[computerIndex].ToString(), computer.name);

        player.score = PlayRound(playerIndex, player.name, ui);
        computer.score = PlayRound(computerIndex, computer.name, ui);

        ui.DisplayGameResult(player.score, computer.score);
    }

    private int SelectPlayerDice(GameUI ui)
    {
        var options = availableDiceIndices.Select(i => diceList[i].ToString()).ToArray();
        int choice = ui.GetDiceSelection(options, player.name);
        int selectedIndex = availableDiceIndices[choice];
        availableDiceIndices.RemoveAt(choice);
        return selectedIndex;
    }

    private int SelectComputerDice()
    {
        int choice = RandomNumberGenerator.GetInt32(0, availableDiceIndices.Count);
        int selectedIndex = availableDiceIndices[choice];
        availableDiceIndices.RemoveAt(choice);
        return selectedIndex;
    }

    private int PlayRound(int diceIndex, string playerName, GameUI ui)
    {
        ui.DisplayRollPrompt(playerName);
        var values = diceList[diceIndex].diceSet;
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
