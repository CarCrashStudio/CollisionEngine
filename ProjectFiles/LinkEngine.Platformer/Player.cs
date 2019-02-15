namespace LinkEngine.Platformer
{
    public class Player : Entities.Player
    {
        public Components.Collider2D Collider { get; set; }
        public Player (int id, string name, int health, int maxHealth, int loc_x, int loc_y, int height, int width) :
            base (id, name, health, maxHealth)
        {
            Collider = new Components.Collider2D(loc_x, loc_y, 0, height, width);
        }

        public void Jump (int force)
        {
            // push the player into the air
            Collider.Push(8, 0, -1);

            // after Push ends, the player velocity should be at 0, let gravity pull the player back down.
            Collider.Pull(8, 0, 1);
        }

        public void Run (int force, int x, Components.Collider2D below)
        {
            Collider.Push(force, x, 0, below);
        }
    }
}
