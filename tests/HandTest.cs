using System.Linq;
using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Entities.Cards;
using UnoNoMercy.Models;

namespace UnoNoMercy.tests;

[Test]
public partial class HandTest: Node
{
    [Test]
    private void HandContainsCardTest()
    {
        Card blueFour = Card.GetBlueFour();
        Hand hand = new Hand([blueFour]);
        Assert.IsTrue(hand.Cards.Contains(blueFour));
    }

    [Test]
    private void HandIsInmutableTest()
    {
        Hand hand = Hand.GetHandWithCards(2);
        Card[] handCards = hand.Cards;
        Card oldCard = handCards[0];
        handCards[0] = Card.GetGreenEight();
        
        Assert.AreNotEqual(handCards[0], hand.Cards[0]);
        Assert.AreEqual(oldCard, hand.Cards[0]);
    }
}