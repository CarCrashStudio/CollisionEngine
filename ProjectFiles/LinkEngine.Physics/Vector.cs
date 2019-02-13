using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Components
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

        #region Vector Operators
        public static Vector operator -(Vector A, Vector B)
        {

            if (A.Y != 0)
            {
                A.Y -= B.Y;
            }
            if (A.X != 0)
            {
                A.X -= B.X;
            }

            return new Vector (A.X, A.Y, A.Z);
        }
        public static Vector operator +(Vector A, Vector B)
        {
            return new Vector (A.X + B.X, A.Y + B.Y, 0);
        }
        public static Vector operator *(Vector A, Vector B)
        {
            return new Vector(A.X * B.X, A.Y * B.Y, A.Z * B.Z);
        }
        #endregion
    }
}
