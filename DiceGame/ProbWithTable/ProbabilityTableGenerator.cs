using System;
using ConsoleTables;

public class ProbabilityTableGenerator
{
    public static double[,] GenerateProbabilityTable(string[] diceSets)
    {
        int n = diceSets.Length;
        double[,] probabilities = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i != j)
                {
                    probabilities[i, j] = CalculateProbability.CalculateProb(diceSets[i], diceSets[j]);
                }
            }
        }

        return probabilities;
    }

    public static void DisplayProbabilityTable(string[] diceSets)
    {
        double[,] probabilities = GenerateProbabilityTable(diceSets);
        int n = diceSets.Length;
        string title = "Probability of the win fоr the user:";
        var headers = new string[n + 1];
        headers[0] = "User dice v";
        Console.WriteLine(title);
        for (int i = 0; i < n; i++)
        {
            headers[i + 1] = diceSets[i];
        }
        var table = new ConsoleTable(headers); 

        for (int i = 0; i < n; i++)
        {
            var row = new object[n + 1];
            row[0] = diceSets[i];

            for (int j = 0; j < n; j++)
            {
                if (i == j)
                    row[j + 1] = "—";
                else
                    row[j + 1] = $"{probabilities[i, j]:P1}";
            }

            table.AddRow(row);
        }

        table.Write(Format.Alternative);
    }
}
