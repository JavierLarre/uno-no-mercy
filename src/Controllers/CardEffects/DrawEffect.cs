using System.Linq;
using UnoNoMercy.Models;

namespace UnoNoMercy.Controllers.CardEffects;

public class DrawEffect: ICardEffect
{
    private int _drawTimes;

    public DrawEffect(int drawTimes) => _drawTimes = drawTimes;
    
    public void ApplyEffect(GameModel model)
    {
        Hand hand = model.Players.First();
        Deck deck = model.Deck;
        for (int i = 0; i < _drawTimes; i++) 
            hand.AddCard(deck.Draw());
    }
}