using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9_Serialize_Deck
{
    [Serializable]
    class Card
    {
        public enum Suits
        {
            Spades,
            Clubs,
            Diamonds,
            Hearts
        }
        public enum Values
        {
            Ace = 1,
            Two= 2,
            Three= 3,
            Four= 4,
            Five= 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13
        }

        public Suits Suit;
        public Values Value;

        public string Name{
            get {
                return $"{Value.ToString()} of {Suit.ToString()}";
            }
        }
        public Card(int suit, int value)
        {
            this.Suit = (Suits)suit;
            this.Value = (Values)value;
        }
    }
}
