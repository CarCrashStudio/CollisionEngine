using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Components
{
    public class Transform
    {
        public Vector position { get; set; }

        public Transform (int x, int y, int z)
        {
            position.X = x;
            position.Y = y;
            position.Z = z;
        }

        /// <summary>
        /// Change the transforms X and Y coordinates
        /// </summary>
        /// <param name="x">The value to change the X coordinate</param>
        /// <param name="y">The Value to change the Y coordinate</param>
        /// <param name="z">The value to change the Z coordinate</param>
        public void Move(int x, int y, int z)
        {
            // Move the entity according to what is put in the parameters, +1,-1,0
            X += x;
            Y += y;
            Z += Z;
        }
        /// <summary>
        /// Change the transforms X and Y coordinates
        /// </summary>
        /// <param name="x">The value to change the X coordinate</param>
        /// <param name="y">The Value to change the Y coordinate</param>
        public void Move(int x, int y)
        {
            // Move the entity according to what is put in the parameters, +1,-1,0
            X += x;
            Y += y;
        }

        public void Rotate ()
        {

        }
    }
}
