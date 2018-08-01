using System.Windows.Forms;

namespace LinkEngine.Components
{
    /// <summary>
    /// The PlayerContoller Class is used to easily translate keyboard WASD input into character movement.
    /// Keys can be rebound in editor
    /// </summary>
    public class PlayerController : Component
    {
        Entities.Player PlayerAttached;

        public string UpKey { get; set; }
        public string DownKey { get; set; }
        public string LeftKey { get; set; }
        public string RightKey { get; set; }

        public PlayerController (Entities.Player playerToControl)
        {
            PlayerAttached = playerToControl;
        }
        public void Move(KeyEventArgs e)
        {
            if(e.KeyCode.ToString() == UpKey)
                    ((Collider2D)PlayerAttached.Components[0]).Transform.Move(0, 1);

            if (e.KeyCode.ToString() == LeftKey)
                ((Collider2D)PlayerAttached.Components[0]).Transform.Move(-1, 0);

            if (e.KeyCode.ToString() == DownKey)
                ((Collider2D)PlayerAttached.Components[0]).Transform.Move(0, -1);

            if (e.KeyCode.ToString() == RightKey)
                ((Collider2D)PlayerAttached.Components[0]).Transform.Move(1, 0);
        }
    }
}
