namespace LinkEngine.Components
{
    public class Transform
    {
        public Vector Position { get; set; }

        public Transform (int x, int y, int z)
        {
            Position.X = x;
            Position.Y = y;
            Position.Z = z;
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
            Position.X += x;
            Position.Y += y;
            Position.Z += z;
        }
        /// <summary>
        /// Change the transforms X and Y coordinates
        /// </summary>
        /// <param name="x">The value to change the X coordinate</param>
        /// <param name="y">The Value to change the Y coordinate</param>
        public void Move(int x, int y)
        {
            // Move the entity according to what is put in the parameters, +1,-1,0
            Position.X += x;
            Position.Y += y;
        }

        /// <summary>
        /// Moves based on a vector
        /// </summary>
        /// <param name="vector"></param>
        public void Move(Vector vector)
        {
            // Move the entity according to what is put in the parameters, +1,-1,0
            Position.X += vector.X;
            Position.Y += vector.Y;
        }

        public void Rotate ()
        {

        }
    }
}
