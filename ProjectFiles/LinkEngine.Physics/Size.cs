using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine
{
    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Size (int h, int w)
        {
            Width = w;
            Height = h;
        }
    }
}
