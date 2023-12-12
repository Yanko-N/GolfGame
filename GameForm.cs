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
        Vector2 _originalSize;

        public GameForm(Form _menuForm)
        {
            InitializeComponent();
            menuForm = _menuForm;
            this.MinimumSize = new Size(500, 400);
            _originalSize = new Vector2(pictureBox1.Size.Width, pictureBox1.Size.Height);

            //Criamos um BitMap com o tamanho da janela
            _backbuffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            GamePlay = new Game(pictureBox1);


        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            menuForm.Show();
        }


        private void Update(object sender, EventArgs e)
        {




            //Aqui dizemos que a imagem do picture box é a bitMap devolvida pela função da gameplay que faz o desenho do jogo
            pictureBox1.Image = GamePlay.DrawGame(_backbuffer);

        }

    }
}
