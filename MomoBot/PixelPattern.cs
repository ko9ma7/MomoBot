using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomoBot
{
    public class PixelPattern
    {
        static int range = 10;
        public Coordinate coordinates;
        public int r;
        public int g;
        public int b;
        public PixelPattern(int x, int y, int r, int g, int b)
        {
            this.coordinates = new Coordinate(x, y);
            this.r = r;
            this.g = g;
            this.b = b;
        }

        override
        public string ToString()
        {
            return coordinates.x.ToString() + "." + coordinates.y.ToString() + "." + this.r.ToString() + "." + this.g.ToString() + "." + this.b.ToString();
        }
    }
}
