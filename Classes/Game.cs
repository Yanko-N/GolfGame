using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace GolfGame.Classes
{
    class Game
    {
        public Bola bola ;
        public Buraco buraco;
        public int score;

        //Lista de obstaculos ainda tem de se adicionar

        public Game(PictureBox box) //esta caixa serve apenas para pegarmos as medidas da area de jogo
        {
            Random random= new Random();

            //Inicializar a bola
            bola = new Bola(Vector2.Zero, Vector2.Zero, Color.Red, 20);


            //Vetor randomizado para a bola spawnar em no eixo x que pertence a 10% do tamanho da area de jogo e no eixo y entre toda a area de jogo
            Vector2 bolaPosicaoSpawn = new Vector2(random.Next(0 + (int)bola.size, (int)(box.Width * 0.1f - bola.size)), random.Next(0, box.Height - (int)bola.size));
            
            //Seto a posicao da bola para a posicao randomizada
            bola.posicao = bolaPosicaoSpawn;

            buraco = new Buraco(Vector2.Zero, Color.Black, 20);


            //Vetor randomizado para o buraco spawnar em no eixo x que pertence a 10% finais  do tamanho da area de jogo e no eixo y entre toda a area de jogo
            Vector2 buracoPosicaoSpawn = new Vector2(random.Next((int)(box.Width * 0.9f + buraco.size), box.Width - (int)buraco.size), random.Next(0, box.Height - (int)buraco.size));

            //Seto a posicao da buraco para a posicao randomizada
            buraco.posicao = buracoPosicaoSpawn;

            
            //AQUI IREMOS FAZER DEPOIS O SPAWN AS OBSTACULOS


        }


        public Bitmap DrawGame(Bitmap backbuffer)
        {
           
            


            using (Graphics g = Graphics.FromImage(backbuffer))
            {
                //Limpa o buffer
                g.Clear(Color.White);
                

                //AQUI PARA A FRENTE DESENHO O QUE QUISER
                


                g.DrawEllipse(Pens.Red, new Rectangle((int)bola.posicao.X , (int)bola.posicao.Y, (int)bola.size, (int)bola.size));
                g.DrawEllipse(Pens.Black, new Rectangle((int)buraco.posicao.X, (int)buraco.posicao.Y, (int)buraco.size , (int)buraco.size ));

            }
            return backbuffer;
        }
    }
}
