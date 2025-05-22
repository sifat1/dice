using System;
using System.Linq;
using System.Collections.Generic;

public class DiceGame
{
    CryptographicService cryptoService;
    string currentPlayerDiceLabel, currentComputerDiceLabel;
    Player player, computer;

    List<Dice> diceList = new List<Dice>();            // All dice
    List<Dice> availableDice = new List<Dice>();       // Dice available to pick from
    List<string> diceLabels = new List<string>();      // For UI display

    public DiceGame(string[] diceSets, CryptographicService cryptoService)
    {
        this.cryptoService = cryptoService;
        player = new Player("Player");
        computer = new Player("Computer");

        foreach (var diceSet in diceSets)
        {
            int[] parsedDice = ParseDice.Parse(diceSet);
            diceList.Add(new Dice(parsedDice));
            diceLabels.Add(diceSet);
        }

        ResetAvailableDice();
    }

    private void ResetAvailableDice()
    {
        availableDice = new List<Dice>(diceList);
    }

    public void StartGame()
    {
        var ui = new GameUI(diceLabels.ToArray());
        ResetAvailableDice();

        // Step 1: Determine first move
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
            // Player picks first
            playerIndex = ui.GetDiceSelection(availableDice.Select(d => d.ToString()).ToArray(), player.name);
            Dice playerDice = availableDice[playerIndex];
            currentPlayerDiceLabel = diceLabels[diceList.IndexOf(playerDice)];
            availableDice.RemoveAt(playerIndex);

            // Computer picks
            computerIndex = cryptoService.GenerateUniformRandomInt(0, availableDice.Count - 1);
            Dice computerDice = availableDice[computerIndex];
            currentComputerDiceLabel = diceLabels[diceList.IndexOf(computerDice)];
        }
        else
        {
            // Computer picks first
            computerIndex = cryptoService.GenerateUniformRandomInt(0, availableDice.Count - 1);
            Dice computerDice = availableDice[computerIndex];
            currentComputerDiceLabel = diceLabels[diceList.IndexOf(computerDice)];
            availableDice.RemoveAt(computerIndex);

            // Player picks
            playerIndex = ui.GetDiceSelection(availableDice.Select(d => d.ToString()).ToArray(), player.name);
            Dice playerDice = availableDice[playerIndex];
            currentPlayerDiceLabel = diceLabels[diceList.IndexOf(playerDice)];
        }

        ui.DisplayDiceChoice(currentPlayerDiceLabel, player.name);
        ui.DisplayDiceChoice(currentComputerDiceLabel, computer.name);

        // Step 3: Play rounds
        player.score = PlayRound(currentPlayerDiceLabel, player.name, ui);
        computer.score = PlayRound(currentComputerDiceLabel, computer.name, ui);

        // Step 4: Determine winner
        ui.DisplayGameResult(player.score, computer.score);
    }

    private int PlayRound(string diceLabel, string player, GameUI ui)
    {
        ui.DisplayRollPrompt(player);
        Dice dice = diceList[diceLabels.IndexOf(diceLabel)];
        int[] values = dice.diceSet;
        int range = values.Length;

        var (key, computerNumber, hmac) = cryptoService.GenerateCommitment(0, range - 1);
        ui.DisplayCommitment(hmac, 0, range - 1);

        int userNumber = ui.GetUserInput(0, range - 1);
        ui.RevealNumber(computerNumber, key);

        int result = (computerNumber + userNumber) % range;
        ui.DisplayModularResult(computerNumber, userNumber, result, range);

        int rollResult = values[result];
        ui.DisplayRollResult(rollResult, player);

        return rollResult;
    }
}
