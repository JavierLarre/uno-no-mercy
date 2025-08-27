using UnoNoMercy.Models;

namespace UnoNoMercy.Controllers.CardEffects;

public interface ICardEffect
{
    void ApplyEffect(GameModel model);
}