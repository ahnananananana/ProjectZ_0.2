using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV
{
    public class MathUtil
    {
        public static float Remap(float value, float newMin, float newMax, float oldMin = 0f, float oldMax = 1f)
        {
            return newMin + (newMax - newMin) * (value - oldMin) / (oldMax - oldMin);
        }
    }
}
