using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarKrash.Collision.Utils
{
    public interface ITransition
    {
        IRoom TargetRoom { get; }
    }
}
