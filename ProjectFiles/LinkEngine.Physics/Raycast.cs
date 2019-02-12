namespace LinkEngine.Components
{
    public class Raycast
    {
        public int Range { get; set; }
        public object Hit { get; set; }
        public Vector Ray { get; set; }

        public Raycast(Vector ray, Collider2D[] allColliders)
        {
            Ray = ray;
            HitTarget(allColliders);
        }

        void HitTarget(Collider2D[] allColliders)
        {
            for (int x = 0; x < Range; x++)
            {
                for (int y = 0; y < Range; y++)
                {
                    foreach(Collider2D collider in allColliders)
                    {
                        if (x > collider.Transform.Position.X || x < collider.Transform.Position.X + collider.Transform.Size.Width)
                        {
                            if (y > collider.Transform.Position.Y || y < collider.Transform.Position.Y + collider.Transform.Size.Height)
                            {
                                Hit = collider.Parent;
                            }
                        }
                    }
                }
            }
        }
    }
}
