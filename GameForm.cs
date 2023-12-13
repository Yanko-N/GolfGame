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
        Game GamePlay;


        //mouse parameters
        Point mouseInicialPoint, mouseEndPoint;

        public GameForm(Form _menuForm)
        {
            InitializeComponent();
            menuForm = _menuForm;
            this.MinimumSize = new Size(500, 400);

            //add events for mouse click

            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;



            //Criamos um BitMap com o tamanho da janela
            _backbuffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            GamePlay = new Game(pictureBox1);


        }


        private void PictureBox1_MouseDown(object? sender, MouseEventArgs e)
        {
            mouseInicialPoint = e.Location;
        }


        private void PictureBox1_MouseUp(object? sender, MouseEventArgs e)
        {
            mouseEndPoint = e.Location;

            HandleClick();
        }


        public void HandleClick()
        {
            Vector2 direction = new Vector2(mouseEndPoint.X - mouseInicialPoint.X,
                                            mouseEndPoint.Y - mouseInicialPoint.Y) * -1;

            float strenght = direction.Length();


            //Apenas se a distancia do vetor for maior que 1, para não haver pequenos delizes
            if(strenght > 1)
            {
                Vector2 normalized = MathFunctions.Normalize(direction);
                GamePlay.AddForceBall(normalized, strenght);

            }



        }


        private void Update(object sender, EventArgs e)
        {
            //Update ao deltaTime
            Time.UpdateDeltaTime();

            //Aqui vamos chamar a função que irá alterar as posição da bola
            GamePlay.HandleMoviment();


            //Aqui dizemos que a imagem do picture box é a bitMap devolvida pela função da gameplay que faz o desenho do jogo
            pictureBox1.Image = GamePlay.DrawGame(_backbuffer);

        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Fecha-se este form pois é o form incial
            menuForm.Close();

        }
    }
}
