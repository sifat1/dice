public class CalculateProbability
{

    public static double CalculateProb(Dice die1, Dice die2)
    {
        int[] d1 = die1.diceSet; 
        int[] d2 = die2.diceSet; 

        int win = 0, total = 0;
        for (int i = 0; i < d1.Length; i++)
        {
            for (int j = 0; j < d2.Length; j++)
            {
                total++;
                if (d1[i] > d2[j])
                {
                    win++;
                }
            }
        }

        return (double)win / total;
    }
}