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
        //"MapName": "Town Square",
        //"LocationIndex": 3,
        //"SpawnPoint": "29, 67",
        //"RouteName": "Town"

        public string MapName;
        public int LocationIndex;
        public Point SpawnPoint;
        public string RouteName;
    }
}
