using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Cards;
using UnoNoMercy.Controllers;
using UnoNoMercy.Entities.Cards;
using UnoNoMercy.Entities.Deck;
using UnoNoMercy.Entities.DiscardPile;
using UnoNoMercy.Models;
using Enumerable = System.Linq.Enumerable;

namespace UnoNoMercy.tests;

[Test]
public partial class Tester : Node
{
    private Card _card;
    private StackedDiscardPile _stackedDiscardPile;
    private StackedDeck _stackedDeck;
    private Hand _hand;
    private CardController _controller;
    private GameModel _model;
    
    [BeforeEach]
    private void InitializeAttributes()
    {
        const int cards = 3;
        _card = Card.GetGreenEight();
        _stackedDiscardPile = StackedDiscardPile.GetPileWithCard();
        _stackedDeck = StackedDeck.GetDeckWithCards(cards);
        _hand = Hand.GetHandWithCards(cards);
        _model = new GameModel
        {
            StackedDeck = _stackedDeck,
            StackedDiscardPile = _stackedDiscardPile,
            Players = [_hand],
            TurnDirection = TurnDirection.Right,
            PlayerInTurn = _hand
        };
        _controller = new CardController(_model);
    }

    [Test]
    private void DiscardPileTest()
    {
        _stackedDiscardPile = new StackedDiscardPile(_card);
        AssertCardIsOnTopPile(_card);
        
        _card = Card.GetGreenEight();
        _stackedDiscardPile.TopCard = _card;
        AssertCardIsOnTopPile(_card);
        
        _card = Card.GetGreenEight();
        AssertIsNotEqualToTopCard(_card);
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
        _stackedDiscardPile.TopCard.Color = CardColor.Red;
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
        {
            AssertPlay(wildCard);
        }
    }

    [Test]
    private void PlayerHandTest()
    {
        Card blueFour = Card.GetBlueFour();
        Hand hand = new Hand([blueFour]);
        Assert.IsTrue(Enumerable.Contains(hand.Cards, blueFour));
    }

    [Test]
    private void DrawCardFromDeckTest()
    {
        StackedDeck stackedDeck = new StackedDeck([_card]);
        
        Card drawnCard = stackedDeck.Draw();
        
        Assert.AreEqual(_card, drawnCard);
    }
    [Test]
    private void DrawTwoCardsTest()
    {
        Card blueFour = Card.GetBlueFour();
        StackedDeck stackedDeck = new StackedDeck([_card, blueFour]);
        
        Assert.AreEqual(blueFour, stackedDeck.Draw());
        Assert.AreEqual(_card, stackedDeck.Draw());
    }

    [Test]
    private void DrawTooManyCardsTest()
    {
        StackedDeck stackedDeck = StackedDeck.GetDeckWithCards(0);
        Assert.Throws<DeckIsEmptyException>(() => stackedDeck.Draw());
    }

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
        Hand handWithCards = Hand.GetHandWithCards(2);
        _model.Players = [handWithCards, _hand];
        _model.PlayerInTurn = handWithCards;
        _controller = new CardController(_model);
        
        _controller.Play(Card.GetGreenEight());
        
        Assert.AreEqual(_model.PlayerInTurn, _hand);
    }

    [Test]
    private void PlayerHandIsInmutableTest()
    {
        Card[] hand = _hand.Cards;
        Card oldCard = hand[0];
        hand[0] = Card.GetGreenEight();
        Assert.AreEqual(oldCard, _hand.Cards[0]);
    }

    [Test]
    private void ApplyDrawTwoAndPassTurn()
    {
        Hand otherHand = Hand.GetHandWithCards(3);
        int oldSize = otherHand.Cards.Length;
        _model.Players.Add(otherHand);
        _controller = new CardController(_model);
        Card drawTwo = Card.GetDrawTwo(CardColor.Green);
        
        _controller.Play(drawTwo);
        
        Assert.AreEqual(otherHand.Cards.Length, oldSize + 2);
        Assert.AreEqual(_model.PlayerInTurn, _hand);
    }
    
    private void AssertPlay(Card card)
    {
        Assert.DoesNotThrow<UnoNoMercyException>(() => _controller.Play(card));
        AssertCardIsOnTopPile(card);
    }

    private void AssertCardIsOnTopPile(Card card)
    {
        Assert.AreEqual(_stackedDiscardPile.TopCard, card);
    }

    private void AssertIsNotEqualToTopCard(Card card)
    {
        Assert.AreNotEqual(_stackedDiscardPile.TopCard, card);
    }
}