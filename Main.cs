using System;
using System.Linq;
using System.Reflection;
using Rocket.API;
using Rocket.Unturned.Player;
using UnityEngine;
using SDG.Unturned;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.API.Collections;
using Rocket.Unturned.Chat;
using Rocket.Core;
using System.Collections.Generic;
using Rocket.Unturned;

namespace PVEManager
{
    public class Main : RocketPlugin<Configuration>
    {
        public static Main Instance = null;

        protected override void Load()
        {
            Instance = this;

            Rocket.Core.Logging.Logger.LogWarning("Successfully Loaded PVEManager!");

            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
        }

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.Log("Unload");
        }

        public List<UnturnedPlayer> Players()
        {
            List<UnturnedPlayer> list = new List<UnturnedPlayer>();

            for (int i = 0; i < Provider.clients.Count; i++)
            {
                SteamPlayer sp = Provider.clients[i];
                UnturnedPlayer p = UnturnedPlayer.FromSteamPlayer(sp);
                list.Add(p);
            }

            return list;
        }

        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            if (Configuration.Instance.GodModeWhilePVE)
            {
                if (Configuration.Instance.PVE_Players.Contains(player.Id))
                {
                    player.GodMode = true;
                }
            }

            if (Configuration.Instance.VanishWhilePVE)
            {
                if (Configuration.Instance.PVE_Players.Contains(player.Id))
                {
                    player.VanishMode = true;
                }
            }
        }

        public void FixedUpdate()
        {
            if (Configuration.Instance.NoEquipWeaponsWhilePVE)
            {
                Check();
            } 
        }

        private void Check()
        {
            if (Provider.clients.Count > 0)
            {
                for (int i = 0; i < Players().Count; i++)
                {
                    UnturnedPlayer player = Players()[i];

                    if (Configuration.Instance.PVE_Players.Contains(player.Id))
                    {
                        if (player.Player.equipment.useable is UseableGun || player.Player.equipment.useable is UseableMelee || player.Player.equipment.useable is UseableThrowable)
                        {
                            player.Player.equipment.dequip();
                        }
                    }
                }
            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList
                {
                    { "command_already", "You are already in {0} Mode" },
                    { "command_public", "{1} is now in {0} Mode" },
                    { "command_self", "You are now in {0} Mode" },
                };
            }
        }
    }
}
