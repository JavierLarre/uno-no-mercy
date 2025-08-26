using System;
using System.Linq;
using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Cards;

namespace UnoNoMercy.tests;

[Test]
public partial class Tester : Node
{
    private Card _greenEight;
    private DiscardPile _discardPile;
    private CardPlayerController _playController;

    [BeforeEach]
    private void InitializeAttributes()
    {
        _greenEight = GetGreenEight();
        _discardPile = new DiscardPile(GetGreenEight());
        _playController = new CardPlayerController(_discardPile);

    }
    private static Card GetGreenEight()
    {
        Card greenEight = new Card
        {
            Value = CardValue.Eight,
            Color = CardColor.Green
        };
        return greenEight;
    }

    private static Card[] GetGreenEights(int sequenceLength)
    {
        Card[] validCards = new Card[sequenceLength];
        for (int i = 0; i < sequenceLength; i++)
        {
            validCards[i] = GetGreenEight();
        }

        return validCards;
    }

    [Test]
    private void DiscardPileTest()
    {
        _discardPile = new DiscardPile(_greenEight);
        AssertCardIsOnTopPile();
        _greenEight = GetGreenEight();
        _discardPile.Add(_greenEight);
        AssertCardIsOnTopPile();
        _greenEight = GetGreenEight();
        Assert.AreNotEqual(_discardPile.Peek(), _greenEight);
    }

    [Test]
    private void PlayTest()
    {
        _playController.Play(_greenEight);
        Assert.AreEqual(_greenEight, _discardPile.Peek());
    }

    [Test]
    private void PlayValidSequenceTest()
    {
        const int sequenceLength = 3;
        Card[] validCards = GetGreenEights(sequenceLength);

        foreach (Card validCard in validCards)
        {
            AssertPlay(validCard);
        }
    }

    private void AssertPlay(Card card)
    {
        _playController.Play(card);
        Assert.AreEqual(_discardPile.Peek(), card);
    }

    private void AssertCardIsOnTopPile()
    {
        Assert.AreEqual(_discardPile.Peek(), _greenEight);
    }
}