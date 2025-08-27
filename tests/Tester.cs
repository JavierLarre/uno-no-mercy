using System.Linq;
using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Cards;

namespace UnoNoMercy.tests;

[Test]
public partial class Tester : Node
{
    private Card _card;
    private DiscardPile _discardPile;
    private Deck _deck;
    private Player _player;
    private GameController _controller;
    private GameModel _model;
    
    private static Card GetGreenEight() =>
        new()
        {
            Value = CardValue.Eight,
            Color = CardColor.Green
        };

    private static Card[] GetGreenEights(int sequenceLength)
    {
        Card[] validCards = new Card[sequenceLength];
        for (int i = 0; i < sequenceLength; i++)
        {
            validCards[i] = GetGreenEight();
        }

        return validCards;
    }

    private static Card GetBlueFour() =>
        new()
        {
            Color = CardColor.Blue,
            Value = CardValue.Four
        };

    private static Player GetNewPlayer(int handSize)
    {
        return new Player(GetGreenEights(handSize));
    }

    private static Deck GetNewDeck(int cards)
    {
        return new Deck(GetGreenEights(cards));
    }

    private static DiscardPile GetNewDiscardPile()
    {
        return new DiscardPile(GetGreenEight());
    }

    [BeforeEach]
    private void InitializeAttributes()
    {
        const int cards = 3;
        _card = GetGreenEight();
        _discardPile = GetNewDiscardPile();
        _deck = GetNewDeck(cards);
        _player = GetNewPlayer(cards);
        _model = new GameModel
        {
            Deck = _deck,
            DiscardPile = _discardPile,
            Players = [_player],
            TurnDirection = TurnDirection.Right
        };
        _controller = new GameController(_model);
    }

    [Test]
    private void DiscardPileTest()
    {
        _discardPile = new DiscardPile(_card);
        AssertCardIsOnTopPile(_card);
        
        _card = GetGreenEight();
        _discardPile.TopCard = _card;
        AssertCardIsOnTopPile(_card);
        
        _card = GetGreenEight();
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
        Card[] validCards = GetGreenEights(sequenceLength);

        foreach (Card validCard in validCards) 
            AssertPlay(validCard);
    }

    [Test]
    private void IsCardPlayableTest()
    {
        Card redNine = GetBlueFour();
        
        Assert.IsFalse(_controller.IsPlayable(redNine));
        Assert.IsTrue(_controller.IsPlayable(_card));
    }

    [Test]
    private void TryPlayInvalidCardTest()
    {
        Card redNine = GetBlueFour();
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
        Card blueFour = GetBlueFour();
        Player player = new Player([blueFour]);
        Assert.IsTrue(player.Hand.Contains(blueFour));
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
        Card blueFour = GetBlueFour();
        Deck deck = new Deck([_card, blueFour]);
        
        Assert.AreEqual(blueFour, deck.Draw());
        Assert.AreEqual(_card, deck.Draw());
    }

    [Test]
    private void DrawTooManyCardsTest()
    {
        Deck deck = GetNewDeck(0);
        Assert.Throws<DeckIsEmptyException>(() => deck.Draw());
    }

    [Test]
    private void DrawTwoEffectTest()
    {
        int oldHandSize = _player.Hand.Count;
        Card drawTwoCard = new Card
        {
            Color = CardColor.Green, Value = CardValue.DrawTwo
        };
        _controller.Play(drawTwoCard);
        Assert.AreEqual(_player.Hand.Count, oldHandSize + 2);
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