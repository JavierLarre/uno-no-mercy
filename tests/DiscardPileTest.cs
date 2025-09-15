using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Entities.Cards;
using UnoNoMercy.Entities.DiscardPile;

namespace UnoNoMercy.tests;

[Test]
public partial class DiscardPileTest: Node
{
    [Test]
    private void StackedDiscardPileTest()
    {
        Card cardOnTop = Card.GetGreenEight();
        StackedDiscardPile discardPile = new StackedDiscardPile(cardOnTop);
        Assert.AreEqual(cardOnTop, discardPile.TopCard);
        
    }
}