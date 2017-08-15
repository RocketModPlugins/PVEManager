using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace PVEManager
{
    public class PVP : IRocketCommand
    {
        public static Main Instance;

        public List<string> Aliases
        {
            get
            {
                return new List<string>() { };
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
                return "gpvp";
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

            string pvp = "PVP";

            if (!Main.Instance.Configuration.Instance.PVE_Players.Contains(player.Id))
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("command_already", pvp));
                return;
            }

            player.GodMode = false;
            player.VanishMode = false;

            Main.Instance.Configuration.Instance.PVE_Players.Remove(player.Id);

            Main.Instance.Configuration.Save();

            

            if (Main.Instance.Configuration.Instance.AnnounceWhenPVE == true)
            {
                UnturnedChat.Say(Main.Instance.Translate("command_public", pvp, player.CharacterName));
            }
            else
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("command_self", pvp));
            }
        }
    }
}
