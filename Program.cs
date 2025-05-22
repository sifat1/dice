class Program
{
    static void Main(string[] args)
    {
        string[] dices = args[1].Split(' ');
        
        if (!ParseDice.IsValidDiceSet(dices))
        {
            return;
        }
        
        var cryptoService = new CryptographicService();
        var game = new DiceGame(dices, cryptoService);
        game.StartGame();
    }


}