using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace LinkEngine.Rendering
{
    /// <summary>
    /// Screen is the object in charge of game rendering. Screen will populate a list of objects to draw and draw them using a threaded infinite loop.
    /// </summary>
    public class Screen
    {
        /// <summary>
        /// Object contains the Image and Pointers to x and y coordinates of the object to draw
        /// </summary>
        public unsafe struct Object
        {
            // The image to draw
            public Bitmap Image;

            // Pointers are used to keep the current version of coordinates that will most likely change 
            // Pointer to object's X Coordinate
            public int *X;
            // Pointer to object's Y Coordinate 
            public int *Y;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public Bitmap Image { get; set; }

        ThreadStart renderStart;
        Thread render;

        /// <summary>
        /// ObjectsOnScreen is a list of Object images and coordinates to draw on screen
        /// </summary>
        public List<Object> ObjectsOnScreen { get; set; }

        /// <summary>
        /// Creates a new Game Screen
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Screen (int width, int height, ref Bitmap image)
        {
            Width = width;
            Height = height;

            Image = image;

            Initialize();
        }

        /// <summary>
        /// Draw is a threaded function the runs infinetly and draws all the objects inside of ObjectsOnScreen. The coordinates to draw the images are pointer variables to the original location of the variable. This prevents the user from manually updating the list with new coordinates
        /// </summary>
        unsafe void Draw ()
        {
            while(true)
            {
                foreach(Object obj in ObjectsOnScreen)
                {
                    var graphics = Graphics.FromImage(Image);
                    graphics.DrawImage(obj.Image, new PointF(int.Parse(obj.X->ToString()), int.Parse(obj.Y->ToString())));
                }
            }
        }

        /// <summary>
        /// Initialize will start a new thread for the Draw function.
        /// </summary>
        public void Initialize()
        {
            renderStart = new ThreadStart(Draw);
            render = new Thread(renderStart);
            render.Start();
        }

        /// <summary>
        /// StopDraw will abort the render thread
        /// </summary>
        public void StopDraw()
        {
            render.Abort();
        }
    }
}
