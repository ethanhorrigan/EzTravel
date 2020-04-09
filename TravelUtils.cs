using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTravel
{
    public class TravelUtils
    {
        /// <summary>
        /// Checks if a point exists within the config.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool PointExistsInConfig(ClickableComponent point)
        {
            return ModEntry.Config.TravelPoints.Any(t => point.name.StartsWith(t.MapName.Replace("{0}", Game1.player.farmName.Value)));
        }

        /// <summary>
        /// Gets a GameLocation for a corresponding Point(from the Map)
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static GameLocation GetLocationForMapPoint(ClickableComponent point)
        {
            string pointName = point.name;
            return Game1.locations[ModEntry.Config.TravelPoints.First(t => pointName.StartsWith(t.MapName.Replace("{0}", Game1.player.farmName.Value))).LocationIndex];
        }

        /// <summary>
        /// Gets a FastTravelPoint(struct) for a corresponding Point(from the map)
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static TravelPoint GetFastTravelPointForMapPoint(ClickableComponent point)
        {
            string pointName = point.name.Replace("{0}", Game1.player.farmName.Value);
            return ModEntry.Config.TravelPoints.First(t => pointName.StartsWith(t.MapName.Replace("{0}", Game1.player.farmName.Value)));
        }
    }
}
