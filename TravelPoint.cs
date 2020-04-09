using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTravel
{
    [Serializable]
    public struct TravelPoint
    {

        public string MapName;
        public int LocationIndex;
        public Point SpawnPoint;
        public string RouteName;
    }
}
