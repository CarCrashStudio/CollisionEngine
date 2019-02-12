using System.Threading;

namespace LinkEngine.Components
{
    public class Physics
    {
        static ThreadStart physicsStart;
        static Thread physics;

        /// <summary>
        /// Initialize will create and start a new thread for the physics to run on
        /// </summary>
        public static void Initialize(ThreadStart ts)
        {
            physicsStart = ts;
            physics = new Thread(physicsStart);
            physics.Start();
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
