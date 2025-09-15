using System.Linq;
using UnoNoMercy.Entities.Deck;
using UnoNoMercy.Models;

namespace UnoNoMercy.Controllers.CardEffects;

public class DrawEffect: ICardEffect
{
    private int _drawTimes;

    public DrawEffect(int drawTimes) => _drawTimes = drawTimes;
    
    public void ApplyEffect(GameModel model)
    {
        var turnController = new TurnController(model);
        Hand hand = turnController.GetNextPlayer();
        Deck stackedDeck = model.Deck;
        for (int i = 0; i < _drawTimes; i++) 
            hand.AddCard(stackedDeck.Draw());
        turnController.PassTurn();
    }
}