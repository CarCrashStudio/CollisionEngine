using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Utils
{
    public class Camera
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public Matrix Transform { get; private set; }
        public void Follow (Vector2 target)
        {
            Transform = Matrix.CreateTranslation(-target.X, -target.Y, 0) 
                      * Matrix.CreateTranslation(ScreenWidth / 2, ScreenHeight / 2, 0);
        }
    }
}
