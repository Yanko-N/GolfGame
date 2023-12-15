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

        public static Vector2 ScaleVectorToNewSpace(Vector2 targetVector,Vector2 originalSize,Vector2 currentSize) {

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
            return new Point((int)vector.X,(int) vector.Y);
        }
        public static Vector2 TransformPointToVector(Point point)
        {
            return new Vector2(point.X,point.Y);
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
    }
}
