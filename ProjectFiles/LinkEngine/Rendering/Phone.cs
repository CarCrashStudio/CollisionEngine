using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LinkEngine
{
    public class Phone
    {
        public Grid MapPane;

        public int Width, Height;
        public int pWidth, pHeight;

        Image[,] Tiles;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        /// <param name="_pwidth"></param>
        /// <param name="_pheight"></param>
        public Phone(int _width, int _height, int _pwidth, int _pheight)
        {
            // set values
            Width = _width;
            Height = _height;
            pWidth = _pwidth;
            pHeight = _pheight;


            // create new map pane
            MapPane = new Grid();
            


            // Initialize a new array
            Tiles = new Image[Height, Width];
            calibrate_location_and_size();
        }

        /// <summary>
        /// Takes a given bitmap and sets it as the Image for the PictureBox at the given x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="bitmap"></param>
        public void Draw(int x, int y, ImageSource bitmap)
        {
            // set the image of the tile to the given bitmap
            Tiles[y, x].Source = bitmap;
            redraw();
        }

        void calibrate_location_and_size()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    Tiles[y, x] = new Image();
                    // set the location of the current tile
                    MapPane.Children.Add(Tiles[y, x], x * pWidth, y * pHeight);
                }
        }
        void redraw()
        {
            //int i = 0;
            //foreach (PictureBox pb in Tiles)
            //{
            //    ((PictureBox)MapPane.Controls[i]).Image = pb.Image;
            //    i++;
            //}
        }
    }
}
