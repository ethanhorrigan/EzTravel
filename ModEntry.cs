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
    public class ModEntry : Mod
    {

        public static Config Config;
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<Config>();


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
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            var menu = (Game1.activeClickableMenu as GameMenu);
            // If not on the map tab.
            if (menu == null || menu.currentTab != GameMenu.mapTab)
                return;

            var mapPage = (Helper.Reflection.GetField<List<IClickableMenu>>(menu, "pages").GetValue()[3]) as MapPage;
            if (mapPage == null)
                return;

            int x = Game1.getMouseX();
            int y = Game1.getMouseY();
            foreach (ClickableComponent point in mapPage.points)
            {
                if (!point.containsPoint(x, y))
                    continue;

                // Make sure the location is valid
                if (!TravelUtils.PointExistsInConfig(point))
                {
                    Monitor.Log($"Failed to find a warp for point [{point.name}]!", LogLevel.Warn);

                    // Right now this closes the map and opens the players bag and doesn't give
                    // the player any information in game about what just happened
                    // so we tell them a warp point wasnt found and close the menu.
                    Game1.showGlobalMessage($"No warp point found.");
                    Game1.exitActiveMenu();
                    continue;
                }

                var location = TravelUtils.GetLocationForMapPoint(point);
                var travelPoint = TravelUtils.GetFastTravelPointForMapPoint(point);

                Game1.warpFarmer(travelPoint.RouteName == null ? location.Name : travelPoint.RouteName, travelPoint.SpawnPoint.X, travelPoint.SpawnPoint.Y, false);
                // Game1.exitActiveMenu();
                this.Monitor.Log($"{Game1.player.Name} x {travelPoint.SpawnPoint.X}.", LogLevel.Debug);
                this.Monitor.Log($"{Game1.player.Name} y {travelPoint.SpawnPoint.Y}.", LogLevel.Debug);

                var locationNames = new String[] { travelPoint.RouteName, location.Name };
                var t1 = new Thread(new ParameterizedThreadStart(CheckIfWarped));
                t1.Start(locationNames);
            }
                // print button presses to the console window
                //this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }

        private void CheckIfWarped(object locationNames)
        {
            var locNames = (string[])locationNames;

            // We need to wait atleast 1.5 seconds to let the location change be complet before checking for it.
            Thread.Sleep(1500);

            // If RerouteName is null we want the LocationName instead.
            // 0 = RerouteName, 1 = LocationName
            var tmpLocName = locNames[0] ?? locNames[1];

            // Check if we are at the new location and if its a festival day.
            if (Game1.currentLocation.Name != tmpLocName && Utility.isFestivalDay(Game1.dayOfMonth, Game1.currentSeason))
                // If there is a festival and we werent able to warp let the player know.
                Game1.showGlobalMessage($"Today's festival is being set up. Try going later.");
            else
                // Finally, if we managed to warp log that we were warped.
                this.Monitor.Log($"Warping player to " + tmpLocName);
        }


        /// <summary>The method invoked when the player warps into a new location.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        //private void OnWarped(object sender, WarpedEventArgs e)
        //{
        //    if (this.GameDebugMode && e.IsLocalPlayer)
        //        this.CorrectEntryPosition(e.NewLocation, Game1.player);
        //}

        /// <summary>Correct the player's position when they warp into an area.</summary>
        /// <param name="location">The location the player entered.</param>
        /// <param name="player">The player who just warped.</param>
        //private void CorrectEntryPosition(GameLocation location, SFarmer player)
        //{
        //    switch (location.Name)
        //    {
        //        // desert (move from inside wall to natural entry point)
        //        case "SandyHouse":
        //            this.MovePlayerFrom(player, new Vector2(16, 3), new Vector2(4, 9), PlayerDirection.Up);
        //            break;

        //        // mountain (move down a bit to natural entry point)
        //        case "Mountain":
        //            this.MovePlayerFrom(player, new Vector2(15, 35), new Vector2(15, 40), PlayerDirection.Up);
        //            break;

        //        // town (move from middle of field near community center to path between town and community center)
        //        case "Town":
        //            this.MovePlayerFrom(player, new Vector2(35, 35), new Vector2(48, 43), PlayerDirection.Up);
        //            break;
        //    }
        //}
    }
}
