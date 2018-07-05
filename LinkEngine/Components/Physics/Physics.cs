using System.Threading;

namespace LinkEngine.Components.Physics
{
    public class Physics
    {
        static ThreadStart physicsStart;
        static Thread physics;

        /// <summary>
        /// Initialize will create and start a new thread for the physics to run on
        /// </summary>
        public static void Initialize()
        {
            physicsStart = new ThreadStart(RunPhysics);
            physics = new Thread(physicsStart);
        }

        /// <summary>
        /// RunPhysics is the method actually doing the physics running
        /// </summary>
        static void RunPhysics()
        {

        }

        /// <summary>
        /// StopPhysics aborts the physics thread
        /// </summary>
        public static void StopPhysics()
        {
            physics.Abort();
        }
    }
}
