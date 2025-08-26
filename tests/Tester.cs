using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Cards;

namespace UnoNoMercy.tests;

[Test]
public partial class Tester : Node
{
 
    private static Card GetGreenEight()
    {
        Card greenEight = new Card
        {
            Value = CardValue.Eight,
            Color = CardColor.Green
        };
        return greenEight;
    }
    [Test]
    private void DiscardPileAddTest()
    {
        var greenEight = GetGreenEight();
        DiscardPile discardPile = new DiscardPile();
        
        discardPile.Add(greenEight);
        
        Assert.AreEqual(discardPile.Peek(), greenEight);
    }

}