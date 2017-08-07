using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snakeyt
{
    public partial class Form1 : Form
    {
        private int score = 0;
        private Keys istiqamet; //snake hereket istiqameti
        private Keys ox; //klaviaturada ox istiqamet duymeleri
        private Point lastsegment;
        private Food food;
        private Snake snake;
        private Bitmap offscreenbitmap;
        private Graphics bitmapgraphics;
        private Graphics screengraphics;

        
        public Form1()
        {
            InitializeComponent();
            istiqamet = Keys.Left;
            ox = istiqamet;
           offscreenbitmap = new Bitmap(250,250);
            snake = new Snake();
            food = new Food();
           lastsegment = snake.Location[snake.Length - 1];
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           lastsegment = snake.Location[snake.Length - 1];
            if(((ox==Keys.Left) && (istiqamet != Keys.Right)) || ((ox==Keys.Right)&&(istiqamet!=Keys.Left)) || ((ox == Keys.Up) && (istiqamet != Keys.Down)) || ((ox == Keys.Down) && (istiqamet != Keys.Up)))
            {
                istiqamet = ox;
            }

            switch (istiqamet)
            {
                case Keys.Left:
                    snake.Left();
                    break;
                case Keys.Right:
                    snake.Right();
                    break;
                case Keys.Up:
                    snake.Up();
                    break;
                case Keys.Down:
                    snake.Down();
                    break;
            }
            bitmapgraphics.Clear(Color.Black);
            bitmapgraphics.FillEllipse(new SolidBrush(Color.Crimson), (food.Location.X * 10), (food.Location.Y * 10), 10, 10);
            bool gameover = false;

            for (int i = 0; i < snake.Length; i++)
            {
                bitmapgraphics.FillRectangle(new SolidBrush(Color.Green), (snake.Location[i].X * 10), (snake.Location[i].Y * 10), 10, 10);

                if ((snake.Location[i] == snake.Location[0]) && i> 0) { gameover = true; }
            }

           screengraphics.DrawImage(offscreenbitmap, 0, 0);
            Check();
            if (gameover == true)
            {
                GameOver();
            }
        }

        private void oyunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
                ox = e.KeyCode;
        }

        protected override bool IsInputKey(Keys keyData)
        {
          return true; 
        }
        private void Check()//---------
        {
            if (snake.Location[0] == food.Location)
            {
                snake.uzunluqArt();
                score += 10;
                scoreLble.Text = score.ToString();
                CreateFood();
            }
        }
        private void CreateFood()
        {
           bool yaranib; // yeni food yaranmasi

            do
            {
                food.CreateFood();
                yaranib = false;
                for (int i = 0; i < snake.Length; i++)
                {
                    if (food.Location == snake.Location[i])
                    {
                        yaranib = true;
                        break;
                    }
                }
            } while (yaranib == true);

        }   
          
        private void StartGame()
        {
            snake.Reset();
            CreateFood();
            istiqamet = Keys.Left;
            timer1.Interval = 100;
            scoreLble.Text = "0";
            score = 0;
            bitmapgraphics = Graphics.FromImage(offscreenbitmap);
            screengraphics = panel1.CreateGraphics();
            timer1.Enabled = true;
        }

        public void GameOver()//-----
        {
            timer1.Enabled = false;
            if (MessageBox.Show("end: " + score, "game over", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                StartGame();
            }
            else Application.Exit();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)//-----
        {
            StartGame();
        }

        private void endToolStripMenuItem_Click(object sender, EventArgs e)//-------
        {
           Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    class Food//--------------
    {
        private Point location;
        public Point Location
        {
            get { return location; }
        }

        public Food()
        {
            location = new Point();
        }

        public void CreateFood()
        {
            Random rnd = new Random();
            location = new Point(rnd.Next(0, 25), rnd.Next(0, 25));
        }
    }

    class Snake
    {
        private int length;
        public int Length
        {
            get { return length;}
            set { length = value; }
        }

        private Point[] location;
        public Point[] Location
        {
            get { return location; }
        }
        public Snake()
        {
            location = new Point[25 * 25];
            Reset();
        }

        public void Reset()
        {
            length = 5;
            for (int i = 0; i < length; i++)
            {
                location[i].X = 12;
                location[i].Y = 12;
            }
        }

        public void Herket()
        {
            for (int i =length-1;i>0; i--)
            {
                location[i] = location[i - 1];
            }
        }

        public void Up()//------
        {
            Herket();
            location[0].Y--;
            if (location[0].Y < 0)
            {
                location[0].Y += 25;
            }
        }

        public void Down()//------------
        {
            Herket();
            location[0].Y++;
            if (location[0].Y>24)
            {
                location[0].Y -= 25;
            }
        }

        public void Left()//-------------------
        {
            Herket();
            location[0].X--;
            if (location[0].X < 0)
            {
                location[0].X += 25;
            }
        }

        public void Right()//-----------
        {
            Herket();
            location[0].X++;
            if (location[0].X>24)
            {
                location[0].X-= 25;
            }
        }

        public void uzunluqArt() //her topu yedikde uzanmasi-----
        {
            length++;
        }
    }
   
}
