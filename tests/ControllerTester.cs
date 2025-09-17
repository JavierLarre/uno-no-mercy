using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Cards;
using UnoNoMercy.Controllers;
using UnoNoMercy.Entities.Cards;
using UnoNoMercy.Models;

namespace UnoNoMercy.tests;

[Test]
public partial class ControllerTester : Node
{
    private Card _card;
    private Hand _hand;
    private CardController _controller;
    private GameModel _model;
    private Card TopCard => _model.DiscardPile.TopCard;
    
    [BeforeEach]
    private void InitializeAttributes()
    {
        const int cards = 3;
        _card = Card.GetGreenEight();
        _hand = Hand.GetHandWithCards(cards);
        ModelBuilder builder = new ModelBuilder{ Players = [_hand]};
        _model = builder.GetMockModel();
        _controller = new CardController(_model);
    }

    [Test]
    private void PlayTest()
    {
        _controller = new CardController(_model);
        AssertPlay(_card);
    }

    [Test]
    private void PlayValidSequenceTest()
    {
        const int sequenceLength = 3;
        Card[] validCards = Card.GetGreenEights(sequenceLength);

        foreach (Card validCard in validCards) 
            AssertPlay(validCard);
    }

    [Test]
    private void IsCardPlayableTest()
    {
        Card redNine = Card.GetBlueFour();
        
        Assert.IsFalse(_controller.IsPlayable(redNine));
        Assert.IsTrue(_controller.IsPlayable(_card));
    }

    [Test]
    private void TryPlayInvalidCardTest()
    {
        Card redNine = Card.GetBlueFour();
        Assert.Throws<UnoNoMercyException>(() => _controller.Play(redNine));
        AssertIsNotEqualToTopCard(redNine);
    }

    [Test]
    private void PlaySpecialCards()
    {
        TopCard.Color = CardColor.Red;
        Card[] specialCards = 
        [
            new()
            {
                Color = CardColor.Red, Value = CardValue.Seven
            },
            new()
            {
                Color = CardColor.Red, Value = CardValue.Zero
            },
            new()
            {
                Color = CardColor.Red, Value = CardValue.DrawTwo
            },
            new()
            {
                Color = CardColor.Yellow, Value = CardValue.DrawFour
            },
            new()
            {
                Color = CardColor.Yellow, Value = CardValue.Skip
            },
            new()
            {
                Color = CardColor.Yellow, Value = CardValue.Reverse
            },
            new()
            {
                Color = CardColor.Yellow, Value = CardValue.DiscardAll
            },
            new()
            {
                Color = CardColor.Yellow, Value = CardValue.SkipEveryone
            }
        ];
        foreach (Card specialCard in specialCards)
        {
            AssertPlay(specialCard);
        }
    }

    [Test]
    private void PlayWildCards()
    {
        Card[] wildCards =
        [
            new()
            {
                Color = CardColor.Wild, Value = CardValue.ReverseDrawFour
            },
            new()
            {
                Color = CardColor.Wild, Value = CardValue.DrawSix
            },
            new()
            {
                Color = CardColor.Wild, Value = CardValue.DrawTen
            },
            new()
            {
                Color = CardColor.Wild, Value = CardValue.ColorRoulette
            }
        ];

        foreach (Card wildCard in wildCards) 
            AssertPlay(wildCard);
    }

    // todo: move to cardEffectsTest
    [Test]
    private void DrawTwoEffectTest()
    {
        int oldHandSize = _hand.Cards.Length;
        Card drawTwoCard = new Card
        {
            Color = CardColor.Green, Value = CardValue.DrawTwo
        };
        _controller.Play(drawTwoCard);
        Assert.AreEqual(_hand.Cards.Length, oldHandSize + 2);
    }

    [Test]
    private void NextTurnTest()
    {
        Hand otherHand = Hand.GetHandWithCards(2);
        ModelBuilder builder = new ModelBuilder
        {
            Players = [otherHand, _hand]
        };
        _model = builder.GetMockModel();
        _controller = new CardController(_model);
        
        Assert.AreEqual(_model.Players.CurrentPlayerHand, otherHand);
        
        _controller.Play(Card.GetGreenEight());
        
        Assert.AreEqual(_model.Players.CurrentPlayerHand, _hand);
    }

    [Test]
    private void ApplyDrawTwoAndPassTurn()
    {
        Hand otherHand = Hand.GetHandWithCards(3);
        int oldSize = otherHand.Cards.Length;
        ModelBuilder builder = new ModelBuilder
        {
            Players = [_hand, otherHand]
        };
        _model = builder.GetMockModel();
        _controller = new CardController(_model);
        Card drawTwo = Card.GetDrawTwo(CardColor.Green);
        
        _controller.Play(drawTwo);
        
        Assert.AreEqual(otherHand.Cards.Length, oldSize + 2);
        Assert.AreEqual(_model.Players.CurrentPlayerHand, _hand);
    }
    
    private void AssertPlay(Card card)
    {
        Assert.DoesNotThrow<UnoNoMercyException>(() => _controller.Play(card));
        AssertCardIsOnTopPile(card);
    }

    private void AssertCardIsOnTopPile(Card card)
    {
        Card topCard = _model.DiscardPile.TopCard;
        Assert.AreEqual(topCard, card);
    }

    private void AssertIsNotEqualToTopCard(Card card)
    {
        Card topCard = _model.DiscardPile.TopCard;
        Assert.AreNotEqual(topCard, card);
    }
}