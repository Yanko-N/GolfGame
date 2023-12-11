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

namespace GolfGame
{
    public partial class GameForm : Form
    {
        Form menuForm;
        Bitmap _backbuffer;
        int tick = 0;

        public GameForm(Form _menuForm)
        {
            InitializeComponent();
            menuForm = _menuForm;
            this.MinimumSize = new Size(500, 400);
            


        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            menuForm.Show();
        }
      

        private void Update(object sender, EventArgs e)
        {
            _backbuffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            tick++;
            if (tick >= pictureBox1.Width) tick = 0;
            using (Graphics g = Graphics.FromImage(_backbuffer))
            {
                //Limpa o buffer
                g.Clear(Color.White);


                //AQUI PARA A FRENTE DESENHO O QUE QUISER


                g.DrawEllipse(Pens.Black, new Rectangle(tick, 50, 100, 100));
                g.DrawString($"{tick}", new Font("Arial", 20), Brushes.Black, tick, 150);
            }
            //Desenha o buffer
            pictureBox1.Image = _backbuffer;

        }
    }
}
