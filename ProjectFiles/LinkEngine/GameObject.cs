using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine
{
    /// <summary>
    /// GameObjects are the main screen object of the LinkEngine
    /// They are used for everything that can hold components
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// All components attached to this GameObject
        /// </summary>
        public List<Component> Components { get; set; }
        /// <summary>
        /// The name to reference the GameObject by
        /// </summary>
        public string Name { get; set; }

        public Vector ScreenPosition { get; set; }
        
        public string ImageURL { get; set; }

        /// <summary>
        /// Creates a new blank GameObject
        /// </summary>
        public GameObject ()
        {
            Name = "Untitled GameObject";
            Components = new List<Component>();
        }
        /// <summary>
        /// Creates a new GameObject with the given name
        /// </summary>
        /// <param name="name">The name to reference the GameObject by</param>
        public GameObject (string name)
        {
            Name = name;
            Components = new List<Component>();
        }
        /// <summary>
        /// Creates a named GameObject with components already on it
        /// </summary>
        /// <param name="name">The name to reference the GameObject by</param>
        /// <param name="components">The components to start out with as an array</param>
        public GameObject (string name, params Component[] components)
        {
            Name = name;
            Components = new List<Component>();
            Components.AddRange(components);
        }
    }
}
