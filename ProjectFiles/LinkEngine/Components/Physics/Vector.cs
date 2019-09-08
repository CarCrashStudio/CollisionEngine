using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector (int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Subtracts a target variable from this variable
        /// </summary>
        /// <param name="vectorToSubtract"></param>
        /// <returns>Returns a new subtracted vector2D</returns>
        public Vector Subtract(Vector vectorToSubtract)
        {

            if (Y != 0)
            {
                Y -= vectorToSubtract.Y;
            }
            if (X != 0)
            {
                X -= vectorToSubtract.X;
            }
            return new Vector (X, Y, Z);
        }

        /// <summary>
        /// Adds a target vector to this vector
        /// </summary>
        /// <param name="vectorToAdd"></param>
        /// <returns>Returns a new added vector2D</returns>
        public Vector Add(Vector vectorToAdd)
        {
            return new Vector (this.X + vectorToAdd.X, this.Y + vectorToAdd.Y, 0);
        }

        public Vector Mulitply (Vector vectorToMultiply)
        {
            return new Vector(this.X * vectorToMultiply.X, this.Y * vectorToMultiply.Y, this.Z * vectorToMultiply.Z);
        }
    }
}
