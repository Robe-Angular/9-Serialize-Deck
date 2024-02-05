using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9_Serialize_Deck
{
    [Serializable]
    class Deck
    {
        private List<Card> cards;
        private Random random = new Random();
        
        public Deck()
        {
            cards = new List<Card>();
            for (int suit = 0; suit <= 3; suit++)
                for (int value = 1; value <= 13; value++)
                    cards.Add(new Card(suit, value));
        }

        public Deck(Card[] initialCards)
        {
            cards = new List<Card>(initialCards);
        }

        public int Count { get { return cards.Count; } }

        public void Add(Card cardToAdd)
        {
            cards.Add(cardToAdd);
        }

        public Card Deal(int index)
        {
            Card CardToDeal = cards[index];
            cards.RemoveAt(index);
            
            return CardToDeal;
        }

        public void Shuffle()
        {
            List<Card> NewCards = new List<Card>();
            while(cards.Count > 0)
            {
                int IndexCardToMove = random.Next(cards.Count);
                NewCards.Add(cards[IndexCardToMove]);
                cards.RemoveAt(IndexCardToMove);
            }
            cards = NewCards;
        }

        public string[] GetCardNames()
        {
            string[] cardNames = new string[cards.Count];
            int counter = 0;
            foreach(Card card in cards)
            {
                cardNames[counter]=card.Name;
                counter++;
            }


            return cardNames;
        }

        public void Sort()
        {
            cards.Sort(new CardComparerBySuit());
        }

    }
}
