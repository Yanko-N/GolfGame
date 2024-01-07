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
        public static Rectangle GetLineRectangle(Vector2 point1, Vector2 point2)
        {
            
            float minX = Math.Min(point1.X, point2.X);
            float minY = Math.Min(point1.Y, point2.Y);
            float maxX = Math.Max(point1.X, point2.X);
            float maxY = Math.Max(point1.Y, point2.Y);

           
            int width = (int)(maxX - minX);
            int height = (int)(maxY - minY);

            
            return new Rectangle((int)minX, (int)minY, width, height);
        }

        public static bool RectangleIntersect(Rectangle rec1,Rectangle rec2,out Point intersectionPoint)
        {
            
            Rectangle intersection = Rectangle.Intersect(rec1, rec2);

            if (!intersection.IsEmpty)
            {
                
                intersectionPoint = new Point(intersection.X, intersection.Y);
                return true;
            }
            else
            {
                
                intersectionPoint = Point.Empty;
                return false;
            }
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

        public static bool isColliding(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (rectangle1.X + rectangle1.Width < rectangle2.X ||
                rectangle2.X + rectangle2.Width < rectangle1.X ||
                rectangle1.Y + rectangle1.Height < rectangle2.Y ||
                rectangle2.Y + rectangle2.Height < rectangle1.Y){
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
