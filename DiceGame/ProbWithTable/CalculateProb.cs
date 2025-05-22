public class CalculateProbability
{

    public static double CalculateProb(string die1, string die2)
    {
        int[] d1 = Array.ConvertAll(die1.Split(','), int.Parse);
        int[] d2 = Array.ConvertAll(die2.Split(','), int.Parse);

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