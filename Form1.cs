using System.Collections;
using System.Security.Cryptography.X509Certificates;
using static Memory_Game.Form1;

namespace Memory_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Card> cards = new List<Card>();
        bool playerTurn = false;
        string cardChoice1 = "";
        int positionChoice1 = 0;

        string labelChoice1 = "";
        string cardChoice2 = "";
        int positionChoice2 = 0;

        string labelChoice2 = "";

        int points = 0;


        public class Card
        {
            public string CardName;
            public int Position;

            public Card(string name, int position)
            {
                CardName = name;
                Position = position;
            }
        }

        private void start_Click(object sender, EventArgs e)
        {

            start.Visible = false;

            foreach (Control c in Controls)
                if ((string)c.Tag == "card") c.Visible = true;


            //Create 52 Cards

            string[] deck = new string[52];

            string[] suits = { "H", "D", "C", "S" };
            string[] names = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "A", "J", "K", "Q" };

            int listIndex = 0;

            foreach (string suit in suits)
            {
                foreach (string name in names)
                {
                    deck[listIndex] = name + suit;
                    listIndex++;
                }
            }

            //Create 20 Positions

            int[] positions = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };


            //Create 20 Card Objects



            for (int i = 0; i < 10; i++)
            {
                string name = "";

                while (name == "")
                {
                    Random random = new Random();
                    int index = random.Next(deck.Length);
                    name = deck[index];
                    deck[index] = "";
                }

                int position1 = 0;

                while (position1 == 0)
                {
                    Random random = new Random();
                    int index = random.Next(positions.Length);
                    position1 = positions[index];
                    positions[index] = 0;
                }

                int position2 = 0;

                while (position2 == 0)
                {
                    Random random = new Random();
                    int index = random.Next(positions.Length);
                    position2 = positions[index];
                    positions[index] = 0;
                }

                cards.Add(new Card(name, position1));
                cards.Add(new Card(name, position2));

                label1.Text = "";

                foreach (Card card in cards)
                {
                    label1.Text += $"Name: {card.CardName}, Position: {card.Position}\n";
                }

            }




        }




        private async void card_Click(object sender, EventArgs e)
        {
            if (playerTurn == false)
            {
                if (sender is Control c && (string)c.Tag == "card")
                {
                    PictureBox label = (PictureBox)sender;

                    string attribute = label.Name;
                    int number = int.Parse(attribute.Replace("pictureBox", ""));

                    foreach (Card card in cards)
                    {
                        if (card.Position == number)
                        {
                            label.Image = Image.FromFile(@"Cards\Cards\" + card.CardName + ".png");
                            cardChoice1 = card.CardName;
                            positionChoice1 = card.Position;
                            labelChoice1 = attribute;
                            playerTurn = true;
                            break;
                        }
                    }

                }
            }
            else if (playerTurn == true)
            {
                if (sender is Control c && (string)c.Tag == "card")
                {
                    PictureBox label = (PictureBox)sender;
                    this.Enabled = false;

                    string attribute = label.Name;
                    int number = int.Parse(attribute.Replace("pictureBox", ""));

                    foreach (Card card in cards)
                    {
                        if (card.Position == number)
                        {
                            label.Image = Image.FromFile(@"Cards\Cards\" + card.CardName + ".png");
                            cardChoice2 = card.CardName;
                            positionChoice2 = card.Position;
                            labelChoice2 = attribute;
                            playerTurn = false;

                        }
                    }

                    if ((cardChoice1 == cardChoice2) && (positionChoice1 != positionChoice2))
                    {
                        await Task.Delay(2000);
                        Controls.Find(labelChoice1, true)[0].Hide();
                        Controls.Find(labelChoice2, true)[0].Hide();
                        this.Enabled = true;
                        points += 2;

                        if (points == 20)
                        {
                            MessageBox.Show("You win!");
                        }
                    }
                    else
                    {
                        await Task.Delay(2000);
                        foreach (PictureBox p in Controls.OfType<PictureBox>().Where(p => (string)p.Tag == "card"))
                            p.Image = Image.FromFile(@"Cards\Cards\green_back.png");
                        this.Enabled = true;

                    }



                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Visible = true;
        }
    }
}
