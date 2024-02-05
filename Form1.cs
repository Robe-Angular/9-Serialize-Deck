using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace _9_Serialize_Deck
{
    public partial class Form1 : Form
    {
        Random random;
        public Form1()
        {
            InitializeComponent();
            this.random = new Random();
        }

        private Deck randomDeck(int number)
        {
            Deck myDeck = new Deck(new Card[] { });
            for(int i = 0; i < number; i++)
            {
                myDeck.Add(new Card(
                    this.random.Next(4),
                    this.random.Next(1,14)
                )); 
            }

            return myDeck;
        }

        private void dealCards(Deck deckToDeal, string title)
        {
            Console.WriteLine(title);
            while(deckToDeal.Count > 0)
            {
                Card nextCard = deckToDeal.Deal(0);
                Console.WriteLine(nextCard.Name);
                
            }
            Console.WriteLine("-----------------------");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Deck deckToWrite = randomDeck(5);
            using (Stream output = File.Create("Deck1.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(output, deckToWrite);
            }
            dealCards(deckToWrite, "What i just wrote to the file");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists("Deck1.dat"))
            {
                MessageBox.Show("File doesn't exist", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (Stream input = File.OpenRead("Deck1.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Deck deckFromFile = (Deck)bf.Deserialize(input);
                dealCards(deckFromFile, "What iread from the file");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using(Stream output = File.Create("Deck1.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                for(int i = 1; i <= 5; i++)
                {
                    Deck deckToWrite = randomDeck(random.Next(1, 10));
                    bf.Serialize(output, deckToWrite);
                    dealCards(deckToWrite, "Deck #" + i + " written.");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists("Deck1.dat"))
            {
                MessageBox.Show("File doesn't exist", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using(Stream input = File.OpenRead("Deck1.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                for(int i = 1; i <= 5; i++)
                {
                    Deck deckToRead = (Deck)bf.Deserialize(input);
                    dealCards(deckToRead, "Deck #" + i +" read.");

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (Stream output = File.Create("card1.dat"))
            {
                Card card1 = new Card((int)Card.Suits.Clubs, (int)Card.Values.Three);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(output, card1);

            }

            using (Stream output = File.Create("card2.dat"))
            {
                Card card2 = new Card((int)Card.Suits.Hearts, (int)Card.Values.Six);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(output, card2);

            }

            byte[] firstFile = File.ReadAllBytes("card1.dat");
            byte[] secondFile = File.ReadAllBytes("card2.dat");
            for(int i = 0; i < firstFile.Length; i++)
            {
                if (firstFile[i] != secondFile[i])
                    Console.WriteLine("Byte #{0}: {1} versus {2}", i, firstFile[i], secondFile[i]);
            }

            byte[] thirdFile = File.ReadAllBytes("card2.dat");
            thirdFile[262] = (byte)Card.Suits.Spades;
            thirdFile[319] = (byte)Card.Values.King;
            File.WriteAllBytes("card3.dat", thirdFile);
            using(Stream output = File.OpenRead("card3.dat"))
            {
                BinaryFormatter bf3rd = new BinaryFormatter();
                Card card3 = (Card)bf3rd.Deserialize(output);
                Console.WriteLine(card3.Name);
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using(StreamReader reader = new StreamReader("input.dat"))
            using(StreamWriter writer = new StreamWriter("output.txt"))
            {
                int position = 0;
                while (!reader.EndOfStream)
                {
                    char[] buffer = new char[16];
                    int charactersRead = reader.ReadBlock(buffer, 0, 16);
                    writer.Write("{0}: ", String.Format("0:x4", position));
                    position += charactersRead;
                    for(int i = 0; i < 16; i++)
                    {
                        if(i < charactersRead)
                        {
                            string hex = String.Format("{0:x2}", (byte)buffer[i]);
                            writer.Write(hex + "");
                        }
                        else
                        {
                            writer.Write(" ");
                        }
                        if(i == 7) { writer.Write("-- "); }
                        if (buffer[i] < 32 || buffer[i] > 250) { buffer[i] = '.'; }
                    }
                    string bufferContents = new string(buffer);
                    writer.WriteLine(" " + bufferContents.Substring(0, charactersRead));
                }
            }
        }
    }
}
