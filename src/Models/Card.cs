using UnoNoMercy.Cards;

namespace UnoNoMercy.Models;

public class Card
{
    public CardColor Color { get; set; }
    public CardValue Value { get; set; }

    public static Card GetGreenEight() =>
        new()
        {
            Value = CardValue.Eight,
            Color = CardColor.Green
        };
    
    public static Card GetBlueFour() => 
        new()
        {
            Color = CardColor.Blue,
            Value = CardValue.Four
        };
    
    public static Card[] GetGreenEights(int cards)
    {
        Card[] validCards = new Card[cards];
        for (int i = 0; i < cards; i++)
        {
            validCards[i] = GetGreenEight();
        }

        return validCards;
    }
    
    public override string ToString()
    {
        return $"{Color} {Value}";
    }
}