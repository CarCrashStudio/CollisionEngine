using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoLink2D.Models
{
    /// <summary>
    /// A set of inputs that can be used by the player. This class allows the controls to be rebound.
    /// </summary>
    public class Input
    {
        // Movement Controls
        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Left { get; set; }
        public Keys Right { get; set; }

        public Keys FaceUp { get; set; }
        public Keys FaceDown { get; set; }
        public Keys FaceLeft { get; set; }
        public Keys FaceRight { get; set; }
    }
}
