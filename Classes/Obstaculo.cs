using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame.Classes
{
    class Obstaculo
    {
        public Vector2 posicao { get; set; }


        public Size size { get; set; }
        public Rectangle retangulo { get; set; }

        public Color color { get; set; } = Color.SandyBrown;

        public Obstaculo(PictureBox box, int _lenght, List<Obstaculo> obstaculos)
        {
            Random random = new Random();

            posicao = new Vector2(random.Next(0 + (int)(box.Width * 0.1f) + (_lenght ), box.Width - (int)(box.Width * 0.1f) - (_lenght )),
                                         random.Next(_lenght, box.Height - _lenght ));


            int maxTentativas = 10;
            int counter = 0;

            //é um retangulo virado na horizontal
            if (random.NextSingle() >= 0.5f)
            {
                size = new Size(_lenght * 10, random.Next(6, _lenght*5));
            }
            else //é um retangulo virado na vertical
            {

                size = new Size(random.Next(6, _lenght*10), _lenght * 5);
            }

            foreach (var ob in obstaculos)
            {
                while (MathFunctions.isCollidingRectangles(new Rectangle(MathFunctions.TransformVectorToPoint(posicao), size),
                                              new Rectangle(MathFunctions.TransformVectorToPoint(ob.posicao), ob.size)) )
                {
                    if (counter >= maxTentativas) break;

                    //Aqui Dara verdadeiro se a posição estiver a colidir com um obstaculo já
                    posicao = new Vector2(random.Next(0 + (int)(box.Width * 0.1f) + (_lenght ), box.Width - (int)(box.Width * 0.1f) - (_lenght)),
                                      random.Next(_lenght, box.Height - _lenght));
                    counter++;
                }


            }


            retangulo = new Rectangle(MathFunctions.TransformVectorToPoint(posicao), size);
           

        }


        public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(color);
            g.FillRectangle(brush, new Rectangle(MathFunctions.TransformVectorToPoint(posicao), size));

        }
    }
}
