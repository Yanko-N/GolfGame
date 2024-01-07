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

        const int MAX_POS_SAVED = 10;

        private Vector2[] lastPositions = new Vector2[MAX_POS_SAVED];

        public Bola(Vector2 posicao, Vector2 velocidade, Color cor, float size)
        {
            this.posicao = posicao;
            this.velocidade = velocidade;
            this.acelaracao = Vector2.Zero;
            this.cor = cor;
            this.size = size;

            for (int i = 0; i < MAX_POS_SAVED; i++)
            {
                lastPositions[i] = Vector2.Zero;
            }
        }

        //Vai faltar adicionar as colisões com os obstaculos
        /// <summary>
        /// Controla a velocidade da bola a partir das informações passadas por parametro
        /// Tem de ser apenas chamada no UPDATE com os parametros
        /// </summary>
        /// <param name="arenaSize"></param>
        public void Collisions(Vector2 arenaSize, List<Obstaculo> obstaculos)
        {
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                if (lastPositions[i] == Vector2.Zero)
                {
                    lastPositions[i] = posicao;
                    count++;
                    break;
                }
                else
                {
                    count++;
                }

            }

            //Mais importante de verificar
            OffLimit(arenaSize);

            Rectangle newRetangulo = MathFunctions.GetLineRectangle(lastPositions[0], lastPositions[count - 1]);
            foreach (var ob in obstaculos)
            {

                if (MathFunctions.RectangleIntersect(newRetangulo, ob.retangulo, out Point recIntersectionPoint))
                {
                    //Automaticamente apos a colisão resetamos as ultimas posicoes
                    for (int i = 0; i < MAX_POS_SAVED; i++)
                    {
                        lastPositions[i] = Vector2.Zero;

                    }


                    // Determine collision side
                    float diferencaX = posicao.X - ob.posicao.X;
                    float diferencaY = posicao.Y - ob.posicao.Y;


                    if (diferencaX > 0)
                    {
                        // collide da esquerda
                        //colocar a bola no sitio correto
                        posicao = MathFunctions.TransformPointToVector(recIntersectionPoint) - new Vector2(size, 0); ;
                        velocidade *= new Vector2(-1, 1);

                    }
                    else
                    {
                        // collide da direita
                        //colocar a bola no sitio correto
                        posicao = MathFunctions.TransformPointToVector(recIntersectionPoint) + new Vector2(size , 0);

                        velocidade *= new Vector2(-1, 1);
                    }

                    if (diferencaY > 0)
                    {
                        // collide na parte de cimaa
                        //colocar a bola no sitio correto
                        posicao = MathFunctions.TransformPointToVector(recIntersectionPoint) - new Vector2(0, size );

                        velocidade *= new Vector2(1, -1);
                    }
                    else
                    {
                        // collide na parte de baixo
                        //colocar a bola no sitio correto
                        posicao = MathFunctions.TransformPointToVector(recIntersectionPoint) + new Vector2(0, size );

                        velocidade *= new Vector2(1, -1);
                    }




                }

            }


            if (count == 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    lastPositions[i] = Vector2.Zero;

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

            float angle = (float)Math.Atan2(velocidade.Y, velocidade.X);
            //instanciar o GameManager para obter o valor da friction
            GameManager manager = GameManager.GetManager();


            float frictionForce = manager.optionsValues.frictionValue * speed;

            velocidade -= MathFunctions.Normalize(velocidade) * frictionForce * Time.deltaTime;

            //0.5f para servir de ajuda para a condição ativar
            if (velocidade.Length() > 0 + 8f)
            {
                canShoot = false;
            }
            else
            {
                canShoot = true;
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
