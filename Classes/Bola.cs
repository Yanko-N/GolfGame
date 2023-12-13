using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GolfGame.Classes
{
    class Bola
    {
        public Vector2 posicao { get; set; }
        public Vector2 velocidade { get; set; }
        public Vector2 acelaracao { get; set; }
        public Color cor { get; set; }
        public float size { get; set; }


        public Bola(Vector2 posicao, Vector2 velocidade, Color cor, float size)
        {
            this.posicao = posicao;
            this.velocidade = velocidade;
            this.acelaracao = Vector2.Zero;
            this.cor = cor;
            this.size = size;
        }
        public void Move()
        {
            this.velocidade += acelaracao * Time.deltaTime;
            this.posicao += velocidade * Time.deltaTime;

            //Handle Friction
            float speed = velocidade.Length();
            float angle = (float)Math.Atan2(velocidade.Y,velocidade.X);
            //instanciar o GameManager para obter o valor da friction
            GameManager manager = GameManager.GetManager();


            float frictionForce = manager.optionsValues.frictionValue * speed;

            velocidade -= MathFunctions.Normalize(velocidade) * frictionForce * Time.deltaTime;


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
