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
        public Color cor { get; set; }
        public float size { get; set; }


        public Bola(Vector2 posicao, Vector2 velocidade, Color cor, float size)
        {
            this.posicao = posicao;
            this.velocidade = velocidade;
            this.cor = cor;
            this.size = size;
        }   
    }
}
