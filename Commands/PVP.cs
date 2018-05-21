using System;
using Rocket.API.Commands;
using Rocket.API.Plugins;
using Rocket.Core.I18N;
using Rocket.Unturned.Player;

namespace PVEManager.Commands
{
    public class PVP : ICommand
    {
        private readonly PvePluginMain _parentPlugin;

        public PVP(IPlugin plugin)
        {
            _parentPlugin = (PvePluginMain) plugin;
        }

        public bool SupportsUser(Type user)
        {
            return typeof(UnturnedUser).IsAssignableFrom(user);
        }

        public string Name => "gpvp";
        public string[] Aliases => null;
        public string Summary => "Enables PVP Mode.";
        public string Description => null;
        public string Permission => null;
        public string Syntax => "";
        public IChildCommand[] ChildCommands => null;

        public void Execute(ICommandContext context)
        {
            UnturnedPlayer player = ((UnturnedUser)context.User).UnturnedPlayer;

            string pvp = "PVP";

            if (!_parentPlugin.IsPVE(player))
            {
                context.User.SendLocalizedMessage(_parentPlugin.Translations, "command_already", pvp);
                return;
            }

            _parentPlugin.SetPVE(player, false);

            if (_parentPlugin.ConfigurationInstance.AnnounceWhenPVE)
            {
                context.User.UserManager.BroadcastLocalized(_parentPlugin.Translations, "command_public", pvp, player.CharacterName);
                return;
            }

            context.User.SendLocalizedMessage(_parentPlugin.Translations, "command_self", pvp);
        }
    }
}
