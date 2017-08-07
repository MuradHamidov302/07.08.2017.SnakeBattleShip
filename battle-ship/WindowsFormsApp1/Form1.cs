using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Button> allBtn = new List<Button>();
        List<int> yenigemi = new List<int>();
        int roket = 1;
        int point = 0;
        int btntext = 0;
        int i = 7;
        int say = 1; //her duzgin atis sayi
        
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                var btnleft = 200;

                for (int j = 0; j < 8; j++)
                {
                    var btntop = 50;

                    Button btn = new Button();
                    this.Controls.Add(btn);
                    btn.Width = 50;
                    btn.Height = 40;
                    btn.BackColor = Color.Transparent;
                    btn.Text = Convert.ToString((i * 8) + (j + 1));
                    btnleft += btn.Width;
                    btn.Left = btnleft;
                    btntop += btn.Height * (i + 1);
                    btn.Top = btntop;
                    btn.Click += btn_click;
                    allBtn.Add(btn);
                    btn.Enabled = false;
                    btn.Cursor = Cursors.NoMove2D;
                }
            }

        }
       
        void btn_click(object sender, EventArgs e)
        {
            roket--; label2.Text = Convert.ToString(roket);
            int a = 0;
            Button btn = sender as Button;
            btntext = Convert.ToInt32(btn.Text);

            while (i>a)
            {
                if (btntext==yenigemi[a])
                {
                    btn.Enabled = false;
                    say++;
                    label7.Text = "Excellent";
                    point += 10;
                    label4.Text = Convert.ToString(point);
                    btn.BackColor = Color.Red;
                    break;
                }
                else
                {
                    a++;
                    btn.BackColor = Color.Yellow;
                    label7.Text = "Oops!!";
                }
                if (say == 7)
                {
                    say = 1;
                    MessageBox.Show("congratulations");
                    foreach (var item in allBtn)
                    {
                        item.Enabled = false;
                    }
                }
            }

            if (roket <= 0)
            {
                if (point >= 40){ MessageBox.Show("congratulations"); }
                foreach (var item in allBtn)
                {
                   item.Enabled = false;
                }
            }
        }
 
        private void yeniO_Click(object sender, EventArgs e)
        {
                roket = 25;
                label2.Text = Convert.ToString(roket);
                Random yeni = new Random(DateTime.Now.Ticks.GetHashCode());
                yeniO.Enabled = false;

            int gemi1 = yeni.Next(2, 63);
            int gemi11 = gemi1 + 1, gemi12 = gemi1 - 1;

            int gemi2 = yeni.Next(1, 56);
            int gemi21 = gemi2 + 8;

            int gemi3 = yeni.Next(1, 64);
            int gemi4 = yeni.Next(1, 64);
            int gemi5 = yeni.Next(1, 64);

            yenigemi.Add(gemi12); yenigemi.Add(gemi1); yenigemi.Add(gemi11);
            yenigemi.Add(gemi2); yenigemi.Add(gemi21);
            yenigemi.Add(gemi3);
            yenigemi.Add(gemi4);

                foreach (var item in allBtn)
                {
                    item.Enabled = true;
                }
        }
        
        private void bitir_Click(object sender, EventArgs e)
        {
            say = 1;
            listBox2.Items.Clear();
            yeniO.Enabled = true;
            if (point >= 40) {listBox1.Items.Add("Your point: "+point+"  -WIN"); }
            else { listBox1.Items.Add("Your point: " + point); }
                
            foreach (var item in allBtn)
            {
                item.BackColor = Color.Transparent;
                item.Enabled =false;
            }
           point = 0;
           label4.Text = Convert.ToString(point);

            for (int i = yenigemi.Count - 1; i >= 0; i--)
            {
               yenigemi.RemoveAt(i);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            foreach (var item in yenigemi)
            {
                 listBox2.Items.Add(Convert.ToString(item));
            }
            
        }
    }
}
