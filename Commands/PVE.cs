using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace PVEManager
{
    public class PVE : IRocketCommand
    {
        public static Main Instance;

        public List<string> Aliases
        {
            get
            {
                return new List<string>() {};
            }
        }

        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Player;
            }
        }

        public string Help
        {
            get
            {
                return "";
            }
        }

        public string Name
        {
            get
            {
                return "gpve";
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "pvemanager.pve" };
            }
        }

        public string Syntax
        {
            get
            {
                return "";
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            string pve = "PVE";

            if (Main.Instance.Configuration.Instance.PVE_Players.Contains(player.Id))
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("command_already", pve));
                return;
            }
                
            if(Main.Instance.Configuration.Instance.GodModeWhilePVE == true)
            {
                player.GodMode = true;
            }
            if (Main.Instance.Configuration.Instance.VanishWhilePVE == true)
            {
                player.VanishMode = true;
            }

            Main.Instance.Configuration.Instance.PVE_Players.Add(player.Id);

            Main.Instance.Configuration.Save();

            if (Main.Instance.Configuration.Instance.AnnounceWhenPVE == true)
            {
                UnturnedChat.Say(Main.Instance.Translate("command_public", pve, player.CharacterName));
            } else
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("command_self", pve));
            }
        }
    }
}
