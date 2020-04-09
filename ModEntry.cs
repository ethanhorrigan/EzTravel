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
            helper.Events.Player.Warped += this.OnWarped;
            helper.Events.Input.CursorMoved += this.OnCursorMoved;
        }

        private void OnCursorMoved(object sender, CursorMovedEventArgs e) 
        {
            if (!Context.IsWorldReady)
                return;

            var menu = (Game1.activeClickableMenu as GameMenu);
            if (menu == null)
                return;

            var map = (Helper.Reflection.GetField<List<IClickableMenu>>(menu, "pages").GetValue()[3]) as MapPage;
            if (map == null)
                return;


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
            if (menu == null || menu.currentTab != GameMenu.mapTab) // Also make sure it's on the right tab(Map)
                return;

            var map_page = (Helper.Reflection.GetField<List<IClickableMenu>>(menu, "pages").GetValue()[3]) as MapPage;
            if (map_page == null)
                return;

            int x = Game1.getMouseX();
            int y = Game1.getMouseY();

            foreach(ClickableComponent point in map_page.points)
            {
                if (!point.containsPoint(x, y))
                    continue;
            }
            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
            //if(e.Button == SButton.LeftShift + SButton.B)
        }

        private void OnWarped(object sender, WarpedEventArgs e)
        {
            this.Monitor.Log($"{Game1.player.Name} warped to {e.NewLocation}.", LogLevel.Debug);
        }
    }
}
    