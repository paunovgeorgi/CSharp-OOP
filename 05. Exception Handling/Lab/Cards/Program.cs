using System.Diagnostics;

namespace Cards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashSet<string>faces = new HashSet<string>()
            {
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
                "J",
                "Q",
                "K",
                "A"

            };
            HashSet<string>suits = new HashSet<string>()
            {
                "S",
                "H",
                "D",
                "C"
            };

            string[] input = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
            List<Card> cardDeck = new();

            foreach (string card in input)
            {
                string[] cards = card.Split();
                try
                {
                    string face = cards[0];
                    string suit = cards[1];
                    if (!faces.Contains(face) || !suits.Contains(suit))
                    {
                        throw new ArgumentException("Invalid card!");
                    }

                    suit = Visualize(suit);
                    cardDeck.Add(new Card(face, suit));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine(string.Join(" ", cardDeck));
        }

        static string Visualize(string suit)
        {
            switch (suit)
            {
                case "S":
                    suit = "\u2660";
                    break;
                case "H":
                    suit = "\u2665";
                    break;
                case "D":
                    suit = "\u2666";
                    break;
                case "C":
                    suit = "\u2663";
                    break;
            }
            return suit;
        }
    }

    public class Card
    {
        public Card(string face, string suit)
        {
            Face = face;
            Suit = suit;
        }

        public string Face { get; private set; }
        public string Suit { get; private set; }

        public override string ToString()
        {
            return $"[{Face}{Suit}]";
        }
    }
}