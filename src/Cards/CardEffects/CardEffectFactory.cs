namespace UnoNoMercy.Cards.CardEffects;

public class CardEffectFactory
{
    public ICardEffect GetFrom(CardValue value) => value switch 
    { 
        CardValue.DrawTwo => new DrawEffect(2), 
        _ => new NoEffect()
    };
}