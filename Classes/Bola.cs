using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Policy;

namespace GolfGame.Classes
{
    class Bola
    {
        public Vector2 posicao { get; set; }
        public Vector2 velocidade { get; set; }
        public Vector2 acelaracao { get; set; }
        public Color cor { get; set; }
        public float size { get; set; }

        public bool canShoot = true;


        public Vector2 startPosition { get; set; }

        public List<Vector2> intersectionPointsPositions = new List<Vector2>();
        public List<Vector2> lastPosicoes { get; set; } = new List<Vector2>();

        public Bola(Vector2 posicao, Vector2 velocidade, Color cor, float size)
        {
            this.posicao = posicao;
            this.velocidade = velocidade;
            this.acelaracao = Vector2.Zero;
            this.cor = cor;
            this.size = size;
            this.startPosition = posicao;
        }

        //Vai faltar adicionar as colisões com os obstaculos
        /// <summary>
        /// Controla a velocidade da bola a partir das informações passadas por parametro
        /// Tem de ser apenas chamada no UPDATE com os parametros
        /// </summary>
        /// <param name="arenaSize"></param>
        public void Collisions(Vector2 arenaSize, List<Obstaculo> obstaculos)
        {

            //Mais importante de verificar
            OffLimit(arenaSize);

            ObstaculosCollision(obstaculos);
           
            
        }


        public void ObstaculosCollision(List<Obstaculo> obstaculos) {
            foreach (var ob in obstaculos)
            {

                if (MathFunctions.LineRectangleIntersect(startPosition, posicao, ob.retangulo, out Vector2 recIntersectionPoint))
                {

                    intersectionPointsPositions.Add(recIntersectionPoint);

                    // Determina donde bateu cima baixo 
                    float diferencaX = posicao.X - ob.posicao.X;
                    float diferencaY = posicao.Y - ob.posicao.Y;

                    //  X-axis 
                    if (Math.Abs(diferencaX) > Math.Abs(diferencaY))
                    {
                        // collide da esquerda
                        if (diferencaX > 0)
                        {
                            posicao = new Vector2(recIntersectionPoint.X , posicao.Y);
                            velocidade = new Vector2(-velocidade.X, velocidade.Y);
                        }
                        // Collide da direita
                        else
                        {
                            posicao = new Vector2(recIntersectionPoint.X , posicao.Y);
                            velocidade = new Vector2(-velocidade.X, velocidade.Y);
                        }
                    }
                    // y-axis
                    else
                    {
                        // Collide de cima
                        if (diferencaY > 0)
                        {
                            posicao = new Vector2(posicao.X, recIntersectionPoint.Y );
                            velocidade = new Vector2(velocidade.X, -velocidade.Y);
                        }
                        // Collide de baixo
                        else
                        {
                            posicao = new Vector2(posicao.X, recIntersectionPoint.Y );
                            velocidade = new Vector2(velocidade.X, -velocidade.Y);
                        }
                    }


                    lastPosicoes.Add(recIntersectionPoint);
                }

            }

        }
        /// <summary>
        /// Verifico se ultrapassou os eixos X,Y e se sim mudo a direção e ajusto a posicao
        /// </summary>
        /// <param name="arenaSize"></param>
        public void OffLimit(Vector2 arenaSize)
        {
            if (posicao.X + size / 2 > arenaSize.X ||
                posicao.X - size / 2 < 0)
            {
                velocidade *= new Vector2(-1, 1);

                if (posicao.X + size / 2 > arenaSize.X)
                {
                    posicao = new Vector2(arenaSize.X - size / 2, posicao.Y);
                }

                if (posicao.X - size / 2 < 0)
                {
                    posicao = new Vector2(0 + size / 2, posicao.Y);
                }

                startPosition = posicao;
                lastPosicoes.Add(posicao);
                intersectionPointsPositions.Add(posicao);


            }

            if (posicao.Y + size / 2 > arenaSize.Y ||
               posicao.Y - size / 2 < 0)
            {
                velocidade *= new Vector2(1, -1);

                if (posicao.Y + size / 2 > arenaSize.Y)
                {
                    posicao = new Vector2(posicao.X, arenaSize.Y - size / 2);
                }

                if (posicao.Y - size / 2 < 0)
                {
                    posicao = new Vector2(posicao.X, 0 + size / 2);
                }

                startPosition = posicao;
                lastPosicoes.Add(posicao);

            }
        }

        /// <summary>
        /// Movimenta o player de acordo com a velocidade que lhe é aplicada por outros metodos
        /// por isso apenas tem de ser chamada no UPDATE para adicionar a sua velocidade
        /// </summary>
        public void Move()
        {
            this.velocidade += acelaracao * Time.deltaTime;
            this.posicao += velocidade * Time.deltaTime;

            //Handle Friction
            float speed = velocidade.Length();

            //instanciar o GameManager para obter o valor da friction
            GameManager manager = GameManager.GetManager();


            float frictionForce = manager.optionsValues.frictionValue * speed;

            velocidade -= MathFunctions.Normalize(velocidade) * frictionForce * Time.deltaTime;

            //8f para servir de ajuda para a condição ativar
            if (velocidade.Length() > 0 + 8f)
            {
                canShoot = false;
                cor = Color.OrangeRed;
            }
            else
            {
                canShoot = true;
                cor = Color.Red;
                lastPosicoes.Add(posicao);
                

            }

            //resetar a acelaração
            acelaracao = Vector2.Zero;

        }

        public void AddForce(Vector2 force)
        {
            //instanciar o GameManager para obter o valor do hitPower
            GameManager manager = GameManager.GetManager();
            this.acelaracao += force * manager.optionsValues.hitPower;
        }
    }
}
