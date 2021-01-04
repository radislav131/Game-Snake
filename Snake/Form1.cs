using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private PictureBox fruit;
        private PictureBox bonus;
        private PictureBox[] snake = new PictureBox[400];
        private Label labelScore;
        private int dirX, dirY;
        private int _width = 900;
        private int _height = 800;
        private int _sizeOfSides = 40;
        private int score = 0;
        

        public Form1()
        {
            InitializeComponent();
            this.Text = "Snake";
            this.Width = _width;
            this.Height = _height;
            dirX = 1;
            dirY = 0;
            labelScore = new Label();
            labelScore.Text = "Score = 0";
            labelScore.Location = new Point(810, 10);
            this.Controls.Add(labelScore);
            snake[0] = new PictureBox();
            snake[0].Location = new Point(200, 200);
            snake[0].Size = new Size(_sizeOfSides, _sizeOfSides);
            snake[0].BackColor = Color.Brown;
            this.Controls.Add(snake[0]);
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Size = new Size(_sizeOfSides, _sizeOfSides);
            _generateMap();
            _generateFruit();
            bonus = new PictureBox();
            bonus.BackColor = Color.Green;
            bonus.Size = new Size(_sizeOfSides * 2, _sizeOfSides * 2);
            timer.Tick += new EventHandler(_update);
            timer.Interval = 100;
            timer.Start();
            this.KeyDown += new KeyEventHandler(OKP);
        }
        
        private void _upgrade()
        {
            
                Random r = new Random();
                rI = r.Next(0, _height - _sizeOfSides);
                int tempI = rI % _sizeOfSides;
                rI -= tempI;
                rJ = r.Next(0, _height - _sizeOfSides);
                int tempJ = rJ % _sizeOfSides;
                rJ -= tempJ;

                bonus.Location = new Point(rI, rJ);
                this.Controls.Add(bonus);
            
        }
        
            private void _thelePort()
        {
            if (snake[0].Location.X <0)
                snake[0].Location = new Point(_height, snake[0].Location.Y) ;
            if (snake[0].Location.X > _height)
                snake[0].Location = new Point(0, snake[0].Location.Y);
            if (snake[0].Location.Y < 0)
                snake[0].Location = new Point(snake[0].Location.X, _width);
            if (snake[0].Location.Y > _width)
                snake[0].Location = new Point(snake[0].Location.X, 0);
        }
        private void _generateFruit()
        {
            Random r = new Random();
            rI = r.Next(0, _height - _sizeOfSides);
            int tempI = rI % _sizeOfSides;
            rI -= tempI;
            rJ = r.Next(0, _height - _sizeOfSides);
            int tempJ = rJ % _sizeOfSides;
            rJ -= tempJ;
          
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
                
        }
        private void _eatitSelf()
        {
            for(int _i = 1; _i <score; _i++)
            {
                if(snake[0].Location == snake[_i].Location)
                {
                   
                    for (int _j = score; _j >0; _j--)
                        this.Controls.Remove(snake[_j]);
                    this.Controls.Remove(snake[0]);
                    MessageBox.Show("Game over");
                    this.Close();




                }
                
            }
            
        }
        private void _eatFruit()
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelScore.Text = "Score = " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + (40) * dirX, snake[score - 1].Location.Y + (40) * dirY);
                snake[score].Size = new Size(_sizeOfSides-1, _sizeOfSides-1);
                snake[score].BackColor = Color.Red;
                this.Controls.Add(snake[score]);
                _generateFruit();
            }
        }
        private void _generateMap()
        {
            for (int i = 0; i < _width / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeOfSides * i);
                pic.Size = new Size(_width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i < _height / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeOfSides * i,0);
                pic.Size = new Size(1,_width);
                this.Controls.Add(pic);
            }
        }
        private void _moveSnake()
        {
            for(int i  = score; i >=1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * (_sizeOfSides), snake[0].Location.Y + dirY *( _sizeOfSides));
            _eatitSelf();
            _thelePort();

        }
        private void _update(object myObject,EventArgs eventArgs)
        {
            
            _eatFruit();
            _moveSnake();
            //cube.Location = new Point(cube.Location.X + dirX * _sizeOfSides, cube.Location.Y + dirY * _sizeOfSides);
        }
        private void OKP(object sender,KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    if (dirX != -1 && dirY != 0)
                    {
                        dirX = 1;
                        dirY = 0;
                    }
                    break;
                case "Left":
                    if (dirX != 1 && dirY != 0)
                    {
                        dirX = -1;
                        dirY = 0;
                    }
                    break;
                case "Up":
                    if (dirX != 0 && dirY != 1)
                    {
                        dirY = -1;
                        dirX = 0;
                    }
                    break;
                case "Down":
                    if (dirX != 0 && dirY != -1)
                    {
                        dirY = 1;
                        dirX = 0;
                    }
                    break;

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
