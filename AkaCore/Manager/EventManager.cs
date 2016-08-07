using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkaCore.Features.Activator;
using EloBuddy;
using EloBuddy.SDK;

namespace AkaCore.Manager
{
    class EventManager
    {
        public static void Load()
        {
            AInitialize.OnGameLoad();
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            AInitialize.OnUpdate();
        }
    }
}
