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
        public Bola bola;
        Buraco buraco;
        public int score = 0;


        private const float maxLenghtStrenght = 150;
        private bool wonState = false;



        //Lista de obstaculos ainda tem de se adicionar
        List<Obstaculo> obstaculos=new List<Obstaculo>();


        public Game(PictureBox box) //esta caixa serve apenas para pegarmos as medidas da area de jogo
        {
            Random random = new Random();

            //Inicializar a bola
            bola = new Bola(Vector2.Zero, Vector2.Zero, Color.Red, 10);


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
            int numeroObstaculos = random.Next(3, 10);

            for(int i = 0; i < numeroObstaculos; i++)
            {

                int lenght = random.Next(5, 10);

               

                Obstaculo newObstaculo = new Obstaculo(box,lenght,obstaculos);
                obstaculos.Add(newObstaculo);
            }

        }

        public void AddForceBall(Vector2 directionNormalized, float strenght)
        {
            if (bola.canShoot)
            {
                GameManager manager = GameManager.GetManager();
                strenght = MathFunctions.Clamp(strenght, 0.1f, maxLenghtStrenght);

                //A força e a direcção normalizada vezes a forca(Distancia do rato) vezes a Força de tacada
                Vector2 force = directionNormalized * strenght * manager.optionsValues.hitPower;
                score++;
                bola.AddForce(force);

            }
        }

        public void HandleCollision(Vector2 arenaSize)
        {
            bola.Collisions(arenaSize,obstaculos);



        }

        public bool CheckWinningCollision()
        {
            if (wonState) {
                return true;
            }

            if (bola.posicao.X < (buraco.posicao.X + buraco.size / 2) && bola.posicao.X > (buraco.posicao.X - buraco.size / 2)
               && bola.posicao.Y < (buraco.posicao.Y + buraco.size / 2) && bola.posicao.Y > (buraco.posicao.Y - buraco.size / 2))
            {
                
                return wonState = true;
            }
            else
            {
                return false;
            }


        }
        public void HandleMoviment()
        {

            bola.Move();

        }

        public void DrawGame(Graphics g, Point clickPoint, Point endPoint, bool isShooting)
        {


            //Limpa o buffer
            g.Clear(Color.LightGreen);
            GameManager.Instance.DrawText(g, "canShoot:" + bola.canShoot.ToString() + "|score:" + score.ToString(), Vector2.One, 14);


            

            //AQUI PARA A FRENTE DESENHO O QUE QUISER
            if (isShooting && bola.canShoot)
            {
                Vector2 direction = new Vector2(endPoint.X - clickPoint.X,
                                            endPoint.Y - clickPoint.Y) * -1;

                float distance = MathFunctions.Clamp(direction.Length(), 0, maxLenghtStrenght);

                g.DrawRectangle(Pens.Black, new Rectangle(clickPoint.X, clickPoint.Y, 10, 10));
                DrawArrow(g, bola.posicao, direction, distance);

            }

            //Aqui vou ter que arranjar isto pq isto apenas desenha a parte de fora!

            g.FillEllipse(Brushes.Black, new Rectangle((int)buraco.posicao.X - (int)(buraco.size / 2), (int)buraco.posicao.Y - (int)(buraco.size / 2), (int)buraco.size, (int)buraco.size));

            g.FillEllipse(Brushes.Red, new Rectangle((int)bola.posicao.X - (int)(bola.size / 2), (int)bola.posicao.Y - (int)(bola.size / 2), (int)bola.size, (int)bola.size));


            foreach(var obstaculo in obstaculos)
            {
                obstaculo.Draw(g);
            }
        }

        private void DrawArrow(Graphics g, Vector2 inicialPoint, Vector2 direction, float length)
        {

            // Normalize a direção para saber a sua direção e termos controlo do tamanho
            Vector2 normalizado = MathFunctions.Normalize(direction);


            // Calcule as coordenadas finais da seta
            Vector2 end = new Vector2(inicialPoint.X + normalizado.X * (float)length, inicialPoint.Y + normalizado.Y * (float)length);

            // Desenhe a linha principal da seta
            g.DrawLine(Pens.Black, inicialPoint.X, inicialPoint.Y, end.X, end.Y);



        }
    }
}
