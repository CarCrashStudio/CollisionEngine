using System.Drawing;
using System.Windows.Forms;

namespace LinkEngine.Rendering
{
    public class Screen
    {
        public Form GameForm;
        public Panel MapPane;
        public PictureBox
            CharacterImage,
            Health,
            EXP;

        public int Width, Height;
        public int pWidth, pHeight;
        public int CharacterWidth, CharacterHeight;
        public int HealthWidth, HealthHeight, EXPWidth, EXPHeight;

        public int Padding = 5;

        PictureBox[,] Tiles;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        /// <param name="_pwidth"></param>
        /// <param name="_pheight"></param>
        public Screen (int _width, int _height, int  _pwidth, int _pheight, int _cwidth, int _cheight, int _hwidth, int _hheight, int _ewidth, int _eheight)
        {
            // set values
            Width = _width;
            Height = _height;
            pWidth = _pwidth;
            pHeight = _pheight;
            CharacterWidth = _cwidth;
            CharacterHeight = _cheight;
            HealthWidth = _hwidth;
            HealthHeight = _hheight;
            EXPWidth = _ewidth;
            EXPHeight = _eheight;

            // create a new game form
            GameForm = new Form
            {
                Size = new Size(400, 400),
                FormBorderStyle = FormBorderStyle.None
            };

            // create new map pane
            MapPane = new Panel
            {
                Size = new Size(Width * pWidth, Height * pHeight)
            };
            MapPane.Location = new Point((GameForm.Width / 2) - (MapPane.Width / 2), (GameForm.Height / 2) - (MapPane.Height / 2));

            // create a new character image box
            CharacterImage = new PictureBox
            {
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(CharacterWidth, CharacterHeight)
            };

            // create the health and experience bars
            Health = new PictureBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(CharacterImage.Location.X + CharacterImage.Width + Padding, CharacterImage.Location.Y),
                Size = new Size(HealthWidth, HealthHeight)
            };
            EXP = new PictureBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(CharacterImage.Location.X + CharacterImage.Width + Padding, CharacterImage.Location.Y + Health.Height + Padding),
                Size = new Size(EXPWidth, EXPHeight)
            };

            // add the map pane to the game form
            GameForm.Controls.Add(CharacterImage);
            GameForm.Controls.Add(Health);
            GameForm.Controls.Add(EXP);
            GameForm.Controls.Add(MapPane);

            // Initialize a new array
            Tiles = new PictureBox[Height, Width];
            calibrate_location_and_size();
        }

        /// <summary>
        /// Takes a given bitmap and sets it as the Image for the PictureBox at the given x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="bitmap"></param>
        public void Draw (int x, int y, Bitmap bitmap)
        {
            // set the image of the tile to the given bitmap
            Tiles[y, x].Image = bitmap;
            redraw();
            //comment
        }

        void calibrate_location_and_size()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    Tiles[y, x] = new PictureBox();
                    // set the location of the current tile
                    Tiles[y, x].Location = new Point(x * pWidth, y * pHeight);
                    // set the size of the current tile
                    Tiles[y, x].Size = new Size(pWidth, pHeight);

                    // add the tile to the GameForm controls
                    MapPane.Controls.Add(Tiles[y, x]);
                }
        }
        void redraw ()
        {
            int i = 0;
            foreach (PictureBox pb in Tiles)
            {
                ((PictureBox)MapPane.Controls[i]).Image = pb.Image;
                i++;
            }
        }
    }
}
