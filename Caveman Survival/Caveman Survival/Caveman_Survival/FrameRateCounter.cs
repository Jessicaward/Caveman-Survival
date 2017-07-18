using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caveman_Survival
{
    public class FrameRateCounter
    {
        public static int frameRate = 0;
        public static int frameCounter = 0;
        public static TimeSpan elapsedTime = TimeSpan.Zero;
    }
}