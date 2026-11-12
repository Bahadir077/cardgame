using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace card
{
    public partial class Form1 : Form
    {
        Button[] kartlar;
        Color[] rengler = {
            Color.Red, Color.Red,
            Color.Blue, Color.Blue,
            Color.Yellow, Color.Yellow,
            Color.Green, Color.Green
        };

        Button ilkSecim = null;
        Button ikinciSecim = null;
        int xal = 0;
        int tapilan = 0;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kartlar = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8 };
            OyunuBaslat();
        }

        void OyunuBaslat()
        {
            xal = 0;
            tapilan = 0;
            ilkSecim = null;
            ikinciSecim = null;
            label1.Text = "Xal: 0";
            label2.Text = "";

            for (int i = 0; i < rengler.Length; i++)
            {
                int r = rnd.Next(rengler.Length);
                Color temp = rengler[i];
                rengler[i] = rengler[r];
                rengler[r] = temp;
            }

            for (int i = 0; i < kartlar.Length; i++)
            {
                kartlar[i].BackColor = Color.LightGray;
                kartlar[i].Tag = rengler[i];
                kartlar[i].Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true) return;

            Button kliklenen = (Button)sender;

            if (kliklenen == ilkSecim) return;

            kliklenen.BackColor = (Color)kliklenen.Tag;

            if (ilkSecim == null)
            {
                ilkSecim = kliklenen;
                return;
            }

            ikinciSecim = kliklenen;

            if (ilkSecim.BackColor == ikinciSecim.BackColor)
            {
                xal += 10;
                label1.Text = "Xal: " + xal;
                label2.ForeColor = Color.Green;
                label2.Text = "✅";

                ilkSecim.Enabled = false;
                ikinciSecim.Enabled = false;
                ilkSecim = null;
                ikinciSecim = null;
                tapilan++;

                if (tapilan == 4)
                {
                    MessageBox.Show("Təbriklər, oyun bitdi!\nToplam xalınız: " + xal, "Oyun Bitdi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Text = "❌";
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            ilkSecim.BackColor = Color.LightGray;
            ikinciSecim.BackColor = Color.LightGray;
            ilkSecim = null;
            ikinciSecim = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            OyunuBaslat();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}