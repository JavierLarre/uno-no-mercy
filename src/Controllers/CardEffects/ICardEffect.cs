using UnoNoMercy.Models;

namespace UnoNoMercy.Cards.CardEffects;

public interface ICardEffect
{
    void ApplyEffect(GameModel model);
}