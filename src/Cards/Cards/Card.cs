namespace UnoNoMercy.Cards;

public class Card
{
    public CardColor Color { get; set; }
    public CardValue Value { get; set; }

    public override string ToString()
    {
        return $"{Color} {Value}";
    }
}