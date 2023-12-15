using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Printing;
using GolfGame.Classes;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Numerics;

namespace GolfGame
{
    public partial class GameForm : Form
    {
        Form menuForm;
        Bitmap _backbuffer;
        Game gamePlay;

        private bool isMouseDown = false;
        Vector2 originalSize;
        private bool wonGame = false;
        private bool isBlocked = false;
        private int score = 0;


        //mouse parameters
        Point mouseInicialPoint, mouseEndPoint, mouseCurrent;


        //botoes endGame
        private Panel endGameMenu;
        private Button returnToMenuButton;
        private Button newGameButton;

        HighScorePage highScorePage;
        public GameForm(Form _menuForm,  HighScorePage highScore)
        {
            InitializeComponent();
            menuForm = _menuForm;
            this.MinimumSize = new Size(500, 400);
            highScorePage = highScore;

            originalSize = MathFunctions.TransformSizeToVector(pictureBox1.Size);


            //Criar os botões

            endGameMenu = new Panel();
            endGameMenu.Dock = DockStyle.Bottom; 
            endGameMenu.Visible = false;

            returnToMenuButton = new Button
            {
                Text = "Return to Menu",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Bottom,
                Height = 40
            };

            newGameButton = new Button
            {
                Text = "New Game",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Bottom,
                Height = 40
            };

            returnToMenuButton.Click += ReturnToMenuButton_Click;
            newGameButton.Click += NewGameButton_Click;

            endGameMenu.Controls.Add(returnToMenuButton);
            endGameMenu.Controls.Add(newGameButton);

            this.Controls.Add(endGameMenu);

            //add events for mouse click

            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;



            //Criamos um BitMap com o tamanho da janela
            _backbuffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            gamePlay = new Game(pictureBox1);


        }

        private void NewGameButton_Click(object? sender, EventArgs e)
        {
            gamePlay = new Game(pictureBox1);
            endGameMenu.Visible = false;
        }

        private void ReturnToMenuButton_Click(object? sender, EventArgs e)
        {
            menuForm.Show();
            this.Hide();
            
        }

        private void PictureBox1_MouseDown(object? sender, MouseEventArgs e)
        {

            if (gamePlay.bola.canShoot)
            {
                isMouseDown = true;
                isBlocked = false;
                mouseInicialPoint = e.Location;
            }
            else
            {
                mouseInicialPoint = new Point(0, 0);
                isBlocked = true;
            }

        }


        private void PictureBox1_MouseUp(object? sender, MouseEventArgs e)
        {

            isMouseDown = false;

            if (gamePlay.bola.canShoot && !isBlocked)
            {
                mouseEndPoint = e.Location;
                HandleClick();
            }
            else
            {
                mouseEndPoint = new Point(0, 0);
            }

        }


        public void HandleClick()
        {

            Vector2 direction = new Vector2(mouseEndPoint.X - mouseInicialPoint.X,
                                       mouseEndPoint.Y - mouseInicialPoint.Y) * -1;

            float strenght = direction.Length();


            //Apenas se a distancia do vetor for maior que 1, para não haver pequenos delizes
            if (strenght > 1)
            {
                Vector2 normalized = MathFunctions.Normalize(direction);
                gamePlay.AddForceBall(normalized, strenght);

            }

        }

        private void HandleWinningState(Graphics g)
        {
            score = gamePlay.score;
            if (wonGame = gamePlay.CheckWinningCollision())
            {
                //limpa as graficos
                g.Clear(Color.LightGreen);
                var text = $"You won!\nscore: {score.ToString()}!";

                if(score < GameManager.Instance.optionsValues.highScore)
                {
                    GameManager.Instance.optionsValues.highScore = score;
                    highScorePage.highScoreLabel.Text = $"O melhor jogo foi com {score} tacadas";
                }

                Vector2 currentSize = MathFunctions.TransformSizeToVector(pictureBox1.Size);

                if (currentSize != originalSize)
                {

                    Vector2 scaledHalf = MathFunctions.ScaleVectorToNewSpace(MathFunctions.TransformSizeToVector(this.Size) * Vector2.One * 0.25f, originalSize, currentSize);


                    GameManager.Instance.DrawText(g, "You Won", scaledHalf, 32);


                }
                else
                {

                    var vectorPosicao = new Vector2(this.Size.Width / 4, this.Size.Height / 4);
                    

                    GameManager.Instance.DrawText(g, "You Won", vectorPosicao, 32);


                }



                endGameMenu.Visible=true;
                
                GameManager.Instance.SaveOptions();
            }
        }
        private void Update(object sender, EventArgs e)
        {
            //Update ao deltaTime
            Time.UpdateDeltaTime();



            //Aqui vamos chamar a função que irá alterar as posição da bola
            gamePlay.HandleMoviment();

            //Aqui vamos chamar a função que controla as colisoes da bola
            gamePlay.HandleCollision(MathFunctions.TransformSizeToVector(_backbuffer.Size));


            //Aqui dizemos que a imagem do picture box é a bitMap devolvida pela função da gameplay que faz o desenho do jogo


            using (Graphics g = Graphics.FromImage(_backbuffer))
            {




                Vector2 currentSize = MathFunctions.TransformSizeToVector(pictureBox1.Size);

                if (currentSize != originalSize)
                {
                    Vector2 scaledClickVector = MathFunctions.ScaleVectorToNewSpace(MathFunctions.TransformPointToVector(mouseInicialPoint), originalSize, currentSize);
                   
                    Vector2 scaledCurrentVector = MathFunctions.ScaleVectorToNewSpace(MathFunctions.TransformPointToVector(mouseCurrent),originalSize,currentSize);
                    
                    gamePlay.DrawGame(g, MathFunctions.TransformVectorToPoint(scaledClickVector), MathFunctions.TransformVectorToPoint(scaledCurrentVector), isMouseDown);


                }
                else
                {
                    gamePlay.DrawGame(g, mouseInicialPoint, mouseCurrent, isMouseDown);

                }



                //Verifico o estado de vitoria do jogo
                HandleWinningState(g);

                pictureBox1.Image = _backbuffer;

            }

        }

        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            mouseCurrent = e.Location;
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Fecha-se este form pois é o form incial
            menuForm.Close();

        }


    }
}
