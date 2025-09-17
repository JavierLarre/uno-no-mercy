
namespace UnoNoMercy.Entities.Cards;

public class Card
{
    public CardColor Color { get; set; }
    public CardValue Value { get; set; }
    
    // todo: eliminar este constructor
    public Card() { }

    public Card(CardColor color, CardValue value)
    {
        Color = color;
        Value = value;
    }

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

    public static Card GetDrawTwo(CardColor color) =>
        new()
        {
            Value = CardValue.DrawTwo,
            Color = color
        };
    
    public override string ToString()
    {
        return $"{Color} {Value}";
    }
}