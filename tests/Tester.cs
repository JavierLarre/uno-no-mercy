using System;
using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Cards;
using UnoNoMercy.Controllers;
using UnoNoMercy.Models;
using Enumerable = System.Linq.Enumerable;

namespace UnoNoMercy.tests;

[Test]
public partial class Tester : Node
{
    private Card _card;
    private DiscardPile _discardPile;
    private Deck _deck;
    private Hand _hand;
    private GameController _controller;
    private GameModel _model;
    
    [BeforeEach]
    private void InitializeAttributes()
    {
        const int cards = 3;
        _card = Card.GetGreenEight();
        _discardPile = DiscardPile.GetPileWithCard();
        _deck = Deck.GetDeckWithCards(cards);
        _hand = Hand.GetHandWithCards(cards);
        _model = new GameModel
        {
            Deck = _deck,
            DiscardPile = _discardPile,
            Players = [_hand],
            TurnDirection = TurnDirection.Right,
            PlayerInTurn = _hand
        };
        _controller = new GameController(_model);
    }

    [Test]
    private void DiscardPileTest()
    {
        _discardPile = new DiscardPile(_card);
        AssertCardIsOnTopPile(_card);
        
        _card = Card.GetGreenEight();
        _discardPile.TopCard = _card;
        AssertCardIsOnTopPile(_card);
        
        _card = Card.GetGreenEight();
        AssertIsNotEqualToTopCard(_card);
    }

    [Test]
    private void PlayTest()
    {
        _controller = new GameController(_model);
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
        _discardPile.TopCard.Color = CardColor.Red;
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
        Deck deck = new Deck([_card]);
        
        Card drawnCard = deck.Draw();
        
        Assert.AreEqual(_card, drawnCard);
    }
    [Test]
    private void DrawTwoCardsTest()
    {
        Card blueFour = Card.GetBlueFour();
        Deck deck = new Deck([_card, blueFour]);
        
        Assert.AreEqual(blueFour, deck.Draw());
        Assert.AreEqual(_card, deck.Draw());
    }

    [Test]
    private void DrawTooManyCardsTest()
    {
        Deck deck = Deck.GetDeckWithCards(0);
        Assert.Throws<DeckIsEmptyException>(() => deck.Draw());
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
        _controller = new GameController(_model);
        
        _controller.Play(Card.GetGreenEight());
        
        Assert.AreEqual(_model.PlayerInTurn, _hand);
    }

    [Test]
    private void PlayerHandIsInmutableTest()
    {
        Card[] hand = _hand.Cards;
        Card oldCard = hand[0];
        hand[0] = Card.GetGreenEight();
        Assert.IsTrue(oldCard == _hand.Cards[0]);
    }

    private void AssertPlay(Card card)
    {
        Assert.DoesNotThrow<UnoNoMercyException>(() => _controller.Play(card));
        AssertCardIsOnTopPile(card);
    }

    private void AssertCardIsOnTopPile(Card card)
    {
        Assert.AreEqual(_discardPile.TopCard, card);
    }

    private void AssertIsNotEqualToTopCard(Card card)
    {
        Assert.AreNotEqual(_discardPile.TopCard, card);
    }
}