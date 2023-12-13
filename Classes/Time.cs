using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfGame.Classes
{
    //Implementar uma class que sirva como deltaTime do Unity
    public class Time
    {
        private static DateTime lastFrameTime;

        public static float deltaTime { get; private set; }

        public static void UpdateDeltaTime()
        {
            DateTime currentFrameTime = DateTime.Now;
            deltaTime = (float)(currentFrameTime - lastFrameTime).TotalSeconds;
            lastFrameTime = currentFrameTime;
        }
    }
}
