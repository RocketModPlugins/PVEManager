using System;
using Rocket.API.Commands;
using Rocket.API.Plugins;
using Rocket.Core.I18N;
using Rocket.Unturned.Player;

namespace PVEManager.Commands
{
    public class PVE : ICommand
    {
        private readonly PvePluginMain _parentPlugin;

        public PVE(IPlugin plugin)
        {
            _parentPlugin = (PvePluginMain)plugin;
        }

        public bool SupportsUser(Type user)
        {
            return typeof(UnturnedUser).IsAssignableFrom(user);
        }

        public string Name => "gpve";
        public string[] Aliases => null;
        public string Summary => "Enables PVE Mode.";
        public string Description => null;
        public string Permission => null;
        public string Syntax => "";
        public IChildCommand[] ChildCommands => null;

        public void Execute(ICommandContext context)
        {
            UnturnedPlayer player = ((UnturnedUser)context.User).Player;

            string pve = "PVE";

            if (!_parentPlugin.IsPVE(player))
            {
                context.User.SendLocalizedMessage(_parentPlugin.Translations, "command_already", pve);
                return;
            }

            _parentPlugin.SetPVE(player, true);

            if (_parentPlugin.ConfigurationInstance.AnnounceWhenPVE)
            {
                context.User.UserManager.BroadcastLocalized(_parentPlugin.Translations, "command_public", pve, player.CharacterName);
                return;
            }

            context.User.SendLocalizedMessage(_parentPlugin.Translations, "command_self", pve);
        }
    }
}
