using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomoBot
{
    public class Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        override
        public string ToString()
        {
            return this.x.ToString() + "." + this.y.ToString();
        }
    }
}
