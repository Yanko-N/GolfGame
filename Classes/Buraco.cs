using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame.Classes
{
    class Buraco
    {
        public Vector2 posicao { get; set; }
        public Color cor { get; set; }
        public float size { get; set; }

        public bool atingido { get; set; } = false;

        public Buraco(Vector2 posicao, Color cor, float size)
        {
            this.posicao = posicao;
            this.cor = cor;
            this.size = size;
        }
    }
}
