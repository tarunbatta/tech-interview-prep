/*
Deck of Cards: 

Design the data structures for a generic deck of cards. Explain how you would
subclass the data structures to implement blackjack.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c7q1 {
    public class Solution {
        public enum Suit { Club, Diamond, Heart, Spade }

        public abstract class Card {
            public int FaceValue { get; }
            public Suit CardSuit { get; }
            public bool IsAvailable { get; set; } = true;

            protected Card (int c, Suit s) {
                FaceValue = c;
                CardSuit = s;
            }

            public abstract int Value ();
        }

        public class Deck<T> where T : Card {
            private readonly List<T> _cards = new List<T> ();
            private int _dealtIndex = 0;

            public int RemainingCards => _cards.Count - _dealtIndex;

            public void SetDeckOfCards (List<T> deckOfCards) {
                _cards.Clear ();
                _cards.AddRange (deckOfCards);
                _dealtIndex = 0;
            }

            public void Shuffle () {
                Random rng = new Random ();
                int n = _cards.Count;
                while (n > 1) {
                    n--;
                    int k = rng.Next (n + 1);
                    (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
                }
            }

            public T DealCard () {
                if (RemainingCards == 0) return null;
                T card = _cards[_dealtIndex];
                card.IsAvailable = false;
                _dealtIndex++;
                return card;
            }
        }

        public class Hand<T> where T : Card {
            public List<T> Cards { get; } = new List<T> ();

            public virtual int Score () {
                int score = 0;
                foreach (T card in Cards) {
                    score += card.Value ();
                }
                return score;
            }

            public void AddCard (T card) {
                Cards.Add (card);
            }
        }

        public class BlackJackCard : Card {
            public BlackJackCard (int c, Suit s) : base (c, s) { }

            public override int Value () {
                if (IsAce ()) return 1;
                if (FaceValue >= 11 && FaceValue <= 13) return 10;
                return FaceValue;
            }

            public bool IsAce () => FaceValue == 1;
        }

        public static void Init (string[] args = null) {
            Deck<BlackJackCard> deck = new Deck<BlackJackCard> ();
            List<BlackJackCard> cards = new List<BlackJackCard> ();
            foreach (Suit suit in Enum.GetValues (typeof (Suit))) {
                for (int v = 1; v <= 13; v++) {
                    cards.Add (new BlackJackCard (v, suit));
                }
            }
            deck.SetDeckOfCards (cards);
            deck.Shuffle ();

            Hand<BlackJackCard> hand = new Hand<BlackJackCard> ();
            hand.AddCard (deck.DealCard ());
            hand.AddCard (deck.DealCard ());

            Console.WriteLine ($"Dealt 2 cards. Hand Score: {hand.Score ()}, Remaining: {deck.RemainingCards}");
        }
    }
}
