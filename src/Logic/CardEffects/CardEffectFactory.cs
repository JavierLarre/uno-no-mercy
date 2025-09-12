using UnoNoMercy.Cards;
using UnoNoMercy.Cards.CardEffects;
using UnoNoMercy.Entities.Cards;

namespace UnoNoMercy.Controllers.CardEffects;

public class CardEffectFactory
{
    public ICardEffect GetFrom(CardValue value) => value switch 
    { 
        CardValue.DrawTwo => new DrawEffect(2), 
        _ => new NoEffect()
    };
}