using System;
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
    private void DiscardPileTest()
    {
        Card greenEight = GetGreenEight();
        DiscardPile discardPile = new DiscardPile(greenEight);
        
        Assert.AreEqual(discardPile.Peek(), greenEight);
    }

    [Test]
    private void PlayTest()
    {
        Card greenEight1 = GetGreenEight();
        Card greenEight2 = GetGreenEight();
        DiscardPile discardPile = new DiscardPile(greenEight1);
        CardPlayerController playController = new CardPlayerController(discardPile);

        playController.Play(greenEight2);

        Assert.AreEqual(greenEight2, discardPile.Peek());
    }

}