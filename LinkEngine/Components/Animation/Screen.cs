using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Components
{
    public class Screen
    {
        public struct Object
        {
            public System.Drawing.Bitmap Image;
            public int X;
            public int Y;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public List<Object> ObjectsOnScreen { get; set; }

        /// <summary>
        /// Creates a new Game Screen
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Screen (int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Draw can loop until canDraw is set to false.
        /// Draw will draw every object on screen according to the ObjectsOnScreen list.
        /// Recommended use inside Timer tick event.
        /// </summary>
        /// <returns></returns>
        public bool Draw (bool canDraw)
        {
            if (canDraw)
            {
                foreach (Object obj in ObjectsOnScreen)
                {
                    Rendering.ScreenObject.Draw(Width, Height, new System.Drawing.Point(obj.X, obj.Y), obj.Image);
                }
            }
            return canDraw;
        }
    }
}
