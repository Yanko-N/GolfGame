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
        public static Vector2 Normalize(this Vector2 vector)
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
