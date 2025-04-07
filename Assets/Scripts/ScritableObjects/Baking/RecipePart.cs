using System;

[Serializable]
public class RecipePart
{
    public int amount;
    //corsponds to the name of a Ingredient scritable objects
    public string type;
    public RecipePart(string type, int amount)
    {
        this.type = type;
        this.amount = amount;
    }
    public override string ToString()
    {
        return $"{type} x {amount}";
    }
}