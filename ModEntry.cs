using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzTravel
{

    /// <summary>
    /// The mod entry point.
    /// </summary>
    public class ModEntry : Mod
    {
        public static ModConfig Config;
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            var menu = (Game1.activeClickableMenu as GameMenu);
            if (menu == null)
                return;

            var map = (Helper.Reflection.GetField<List<IClickableMenu>>(menu, "pages").GetValue()[3]) as MapPage;
            if (map == null)
                return;

            int x = Game1.getMouseX();
            int y = Game1.getMouseY();

            foreach (ClickableComponent point in map.points)
            {
                if (!point.containsPoint(x, y))
                    continue;

                var loc = TravelUtils.GetLocationForMapPoint(point);
                var travelPoint = TravelUtils.GetFastTravelPointForMapPoint(point);
                this.Monitor.Log($"location: {loc.Name}.", LogLevel.Debug);
                if (e.Button == SButton.MouseLeft)
                {
                    Game1.warpFarmer(travelPoint.RouteName == null ? loc.Name : travelPoint.RouteName, travelPoint.SpawnPoint.X, travelPoint.SpawnPoint.Y, false);
                    Game1.exitActiveMenu();
                }          
            }

        }
    }
}
    