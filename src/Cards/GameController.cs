using System.Collections.Generic;
using System.Linq;

namespace UnoNoMercy.Cards;

public class GameController
{
    private DiscardPile _discardPile;
    private List<Player> _players;
    private Deck _deck;

    public GameController(GameModel model)
    {
        _deck = model.Deck;
        _players = model.Players.ToList();
        _discardPile = model.DiscardPile;
    }

    public void Play(Card card)
    {
        if (!IsPlayable(card))
            throw new UnoNoMercyException();
        if (card.Value is CardValue.DrawTwo)
            DrawTwo();

        _discardPile.TopCard = card;
    }

    public bool IsPlayable(Card card)
    {
        var validator = new PlayableCardValidator(_discardPile.TopCard);
        return validator.IsPlayable(card);
    }

    private void DrawTwo()
    {
        Player player = _players.First();
        player.Hand.Add(_deck.Draw());
        player.Hand.Add(_deck.Draw());
    }
}