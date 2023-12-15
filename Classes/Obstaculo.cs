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
        public bool isCurved { get; set; } = false;

        public int lenght { get; set; }

        public Size size { get; set; }
        public Point[] points { get; set; } = new Point[3];

        public Obstaculo(Vector2 _posicao, int _lenght)
        {

            posicao = _posicao;
            lenght = _lenght;

            Random rdn = new Random();

            if (rdn.NextSingle() >= 0.5f)
            {
                isCurved = true;
            }


            if (!isCurved)
            {

                //é um retangulo virado na horizontal
                if (rdn.NextSingle() >= 0.5f)
                {
                    size = new Size(lenght, rdn.Next(4, lenght));
                }
                else //é um retangulo virado na vertical
                {

                    size = new Size(rdn.Next(4,lenght) , lenght);
                }
            }
            else
            {
                //é uma curva

                //é um curva virado na a frente
                if (rdn.NextSingle() >= 0.5f)
                {
                    Point startPoint = MathFunctions.TransformVectorToPoint(posicao);
                    Point midTrianglePoint = new Point(startPoint.X + lenght, startPoint.Y);
                    Point endPoint = new Point(startPoint.X + lenght, startPoint.Y + lenght);

                    points.Append(startPoint);
                    points.Append(midTrianglePoint);
                    points.Append(endPoint);
                }
                else //é uma cruva  virado na atras
                {
                    Point startPoint = MathFunctions.TransformVectorToPoint(posicao);
                    Point midTrianglePoint = new Point(startPoint.X - lenght, startPoint.Y);
                    Point endPoint = new Point(startPoint.X - lenght, startPoint.Y - lenght);

                    points.Append(startPoint);
                    points.Append(midTrianglePoint);
                    points.Append(endPoint);
                }


            }

        }


        public void Draw(Graphics g)
        {
            if (!isCurved)//retangle
            {

               g.FillRectangle(Brushes.Brown, new Rectangle(MathFunctions.TransformVectorToPoint(posicao), size));

            }
            else//curved now
            {

                g.DrawClosedCurve(Pens.Brown, points);
            }
        }
    }
}
