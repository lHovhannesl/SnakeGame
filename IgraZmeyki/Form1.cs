using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private int _height , _width;
        private int sizeBox;
        private Label labelScore;
        private int rX, rY;
        private PictureBox fruit;
        private int Score;
        private PictureBox[] snakes = new PictureBox[100];
        private int dirX, dirY;
        private PictureBox pic;



        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OKP);
            _height = 800;
            _width = 800;
            sizeBox = 40;
            this.Height = _height;
            this.Width = _width;
            dirX = 1;
            dirY = 0; 
            labelScore = new Label();
            labelScore.Text = "Score: " + Score.ToString();
            labelScore.Location = new Point(720,20);
            labelScore.Size = new Size(70, 20);
            this.Controls.Add(labelScore);
            snakes[0] = new PictureBox
            {
                Location = new Point(0, 0),
                Size = new Size(sizeBox, sizeBox),
                BackColor = Color.Red
            };
            this.Controls.Add(snakes[0]);
            GenerateMap();
            GenerateFruits();
            timer1.Interval = 100;
            timer1.Start();


        }



        private void checkBorders()
        {
            if (snakes[0].Location.X < 0)
            {
                for (int i = 1; i <= Score; i++)
                {
                    this.Controls.Remove(snakes[i]);
                }
                Score = 0;
                dirX = 1;
            }
            if (snakes[0].Location.Y > _height)
            {
                for (int i = 1; i < Score; i++)
                {
                    this.Controls.Remove(snakes[i]);
                }
                Score = 0;
                dirY = -1;
            }
            if (snakes[0].Location.X > _width - 140)
            {
                for (int i = 0; i < Score; i++)
                {
                    this.Controls.Remove(snakes[i]);
                }
                Score = 0;
                dirX = -1;
            }
            if (snakes[0].Location.Y < 0)
            {
                for (int i = 0; i < Score; i++)
                {
                    this.Controls.Remove(snakes[i]);
                }
                Score = 0;
                dirY = 1;
            }
        }

        private void _moveSnake()
        {
            for (int i = Score; i >= 1; i--)
            {
                snakes[i].Location = snakes[i - 1].Location;
            }
            snakes[0].Location = new Point(snakes[0].Location.X + dirX * (sizeBox), snakes[0].Location.Y + dirY * (sizeBox));
            eatItself();
        }

        private void eatItself()
        {
            for (int i = 1; i <= Score; i++)
            {
                if (snakes[0].Location == snakes[i].Location)
                {
                    for (int j = i; j <= Score; j++)
                    {
                        this.Controls.Remove(snakes[j]);

                    }
                    Score = Score - (Score - i + 1);
                    labelScore.Text = "Score: " + Score;
                
                }
            }
        }
        private void eatFruit()
        {
            if (snakes[0].Location.X == rX * sizeBox && snakes[0].Location.Y == rY * sizeBox)
            {
                this.Controls.Remove(fruit);
                Score++;
                labelScore.Text = "Score: " + Score.ToString();
                snakes[Score] = new PictureBox();
                snakes[Score].BackColor = Color.Red;
                snakes[Score].Location = new Point(snakes[Score - 1].Location.X + 40 * dirX, snakes[Score - 1].Location.Y + 40 * dirY);
                snakes[Score].Size = new Size(sizeBox, sizeBox);
                this.Controls.Add(snakes[Score]);
                GenerateFruits();
            }
        }

        private void GenerateFruits()
        {
            Random rnd = new Random();
            rX = rnd.Next((_width - 120) / sizeBox);
            rY = rnd.Next(_height / sizeBox);
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Location = new Point(rX * sizeBox, rY * sizeBox);
            fruit.Size = snakes[0].Size;
            this.Controls.Add(fruit);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkBorders();
            eatFruit();
            _moveSnake();
        }

        private void GenerateMap()
        {
            for (int i = 0; i < _width/sizeBox; i++)
            {
                pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Size = new Size(_width - 120, 1);
                pic.Location = new Point(0,i * sizeBox);
                this.Controls.Add(pic);

            }

            for (int i = 0; i < _height/sizeBox - 2; i++)
            {
                pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Size = new Size(1, _height);
                pic.Location = new Point(i * sizeBox, 0);
                this.Controls.Add(pic);
            }

        }


        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }
        }
    }
}
