public class Dice
{
    public int[] diceSet;

    public Dice(int[] diceSet)
    {
        this.diceSet = diceSet;
    }

    public override string ToString()
    {
        return "[" + string.Join(", ", diceSet) + "]";
    }
}
