using UnoNoMercy.Entities.Cards;

namespace UnoNoMercy.Entities.DiscardPile;

public interface DiscardPile
{
    Card TopCard { get; set; }
}