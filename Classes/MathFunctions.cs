using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame.Classes
{
    public static class MathFunctions
    {

        public static Vector2 ScaleVectorToNewSpace(Vector2 targetVector, Vector2 originalSize, Vector2 currentSize)
        {

            return new Vector2((targetVector.X / currentSize.X) * originalSize.X,
                                                                    (targetVector.Y / currentSize.Y) * originalSize.Y);
        }
        public static Point TransformSizeToPoint(Size size)
        {
            return new Point(size.Width, size.Height);
        }
        public static Vector2 TransformSizeToVector(Size size)
        {
            return new Vector2(size.Width, size.Height);
        }
        public static Point TransformVectorToPoint(Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }
        public static Vector2 TransformPointToVector(Point point)
        {
            return new Vector2(point.X, point.Y);
        }
        public static Vector2 GetLineVector(Vector2 point1, Vector2 point2)
        {


            return point2 - point1;
        }


        /// <summary>
        /// Esta função irá retornar verdadeiro se a linha colidir com um retangulo e se bater retorna verdaderiro
        /// e ira mandar um vetor com as coordenadas da interseção
        /// </summary>
        /// <param name="startPoint"> this simbolizes the startPoint of the ball</param>
        /// <param name="endPoint">this simbolizes the current Point of the ball</param>
        /// <param name="rec">this simbolizes the rectangle that is gonna intersect</param>
        /// <param name="intersectionPoint">this simbolizes the first intersection Point</param>
        /// <returns></returns>
        public static bool LineRectangleIntersect(Vector2 startPoint, Vector2 endPoint, Rectangle rec, out Vector2 intersectionPoint,out int side)
        {
            bool left = LineLineIntersect(startPoint, endPoint, new Vector2(rec.Left, rec.Top), new Vector2(rec.Left, rec.Bottom), out Vector2 intersectPoint1);
            bool right = LineLineIntersect(startPoint, endPoint, new Vector2(rec.Right, rec.Top), new Vector2(rec.Right, rec.Bottom), out Vector2 intersectPoint2);
            bool top = LineLineIntersect(startPoint, endPoint, new Vector2(rec.Left, rec.Top), new Vector2(rec.Right, rec.Top), out Vector2 intersectPoint3);
            bool bottom = LineLineIntersect(startPoint, endPoint, new Vector2(rec.Left, rec.Bottom), new Vector2(rec.Right, rec.Bottom), out Vector2 intersectPoint4);


            intersectionPoint = Vector2.Zero;
            side = 0;
            List<Vector2> intersectPoints = new List<Vector2>
            {
                intersectPoint1,
                intersectPoint2,
                intersectPoint3,
                intersectPoint4
            };

            float bestLenght = float.MaxValue;

            if (left || right || top || bottom)
            {
                //Houve colisao agora vamos ver qual é a interseção mais proxima do ponto inicial
                for (int i = 0; i < intersectPoints.Count ; i++)
                {
                    if (!Vector2.Equals(intersectPoints[i], Vector2.Zero))
                    {
                        Vector2 vetorDirecao = intersectPoints[i] - startPoint;

                        float distance = vetorDirecao.Length();

                        if (distance < bestLenght)
                        {
                            bestLenght = distance;
                            intersectionPoint = intersectPoints[i];
                        }
                    }

                }
                if (left)
                {
                    side = 1;
                }
                if (right)
                {
                    side = 2;
                }
                if (top)
                {
                    side = 3;
                }
                if (bottom)
                {
                    side = 4;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Retorna verdadeiro se duas linhas se intersetao e devolveo ponto de intersecao
        /// retorna falso se não há colisao e devolve o ponto de interseção Vector 0
        /// </summary>
        /// <param name="startPointA"></param>
        /// <param name="endPointA"></param>
        /// <param name="startPointB"></param>
        /// <param name="endPointB"></param>
        /// <param name="intersectionPoint"></param>
        /// <returns></returns>
        public static bool LineLineIntersect(Vector2 startPointA, Vector2 endPointA, Vector2 startPointB, Vector2 endPointB, out Vector2 intersectionPoint)
        {


            float denominador = (endPointB.X - startPointB.X) * (endPointA.Y - startPointA.Y) - (endPointB.Y - startPointB.Y) * (endPointA.X - startPointA.X);

            if (denominador == 0)
            {
                // As linhas são paralelas (ou coincidentes), não há interseção
                intersectionPoint = Vector2.Zero;
                return false;
            }


            //distancia que terao de ser iguais para colidir
            //por razões de simplificação tem de ser entre 0 e 1 ambas
            float alpha_A = ((endPointB.X - startPointB.X) * (startPointB.Y - startPointA.Y) - (endPointB.Y - startPointB.Y) * (startPointB.X - startPointA.X)) / ((endPointB.X - startPointB.X) * (endPointA.Y - startPointA.Y) - (endPointB.Y - startPointB.Y) * (endPointA.X - startPointA.X));
            float alpha_B = ((endPointA.X - startPointA.X) * (startPointB.Y - startPointA.Y) - (endPointA.Y - startPointA.Y) * (startPointB.X - startPointA.X)) / ((endPointB.X - startPointB.X) * (endPointA.Y - startPointA.Y) - (endPointB.Y - startPointB.Y) * (endPointA.X - startPointA.X));

            intersectionPoint = Vector2.Zero;
            if (alpha_A >= 0 && alpha_A <= 1 && alpha_B >= 0 && alpha_B <= 1)
            {

                intersectionPoint = new Vector2(startPointA.X + (alpha_A * (endPointA.X - startPointA.X)), startPointA.Y + (alpha_A * (endPointA.Y - startPointA.Y)));

                return true;
            }

            return false;
        }
        public static Vector2 Normalize(Vector2 vector)
        {
            float length = vector.Length();

            if (length > 0)
            {
                //Normaliza
                return new Vector2(vector.X / length, vector.Y / length);
            }
            else
            {
                //impossivel normalizar
                return Vector2.Zero;
            }
        }

        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        public static bool isCollidingRectangles(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (Rectangle.Intersect(rectangle1, rectangle2) == Rectangle.Empty)
            {
                //SEM COLISAO
                return false;
            }
            else
            {
                //COLIDINDO
                return true;
            }
        }
    }
}
