using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTravel
{
    /// <summary>The direction a player is facing.</summary>
    public enum Direction
    {
        /// <summary>The player is facing up (with their back to the camera).</summary>
        Up = 0,
        /// <summary>The player is right (with their right side to the camera).</summary>
        Right = 1,
        /// <summary>The player is down (with their face to the camera).</summary>
        Down = 2,
        /// <summary>The player is left (with their left side to the camera).</summary>
        Left = 3
    }
}
