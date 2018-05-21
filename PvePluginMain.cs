using System.Linq;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Rocket.Core.Plugins;
using System.Collections.Generic;
using Rocket.API.DependencyInjection;
using Rocket.API.Eventing;
using Rocket.API.Player;
using Rocket.Core.Logging;
using Rocket.Core.Player.Events;

namespace PVEManager
{
    public class PvePluginMain : Plugin<PVEManagerConfiguration>, IEventListener<PlayerDamageEvent>
    {
        private readonly IPlayerManager _playerManager;

        public PvePluginMain(IDependencyContainer container, IEventManager eventManager, IPlayerManager playerManager) : base("PVE Manager", container)
        {
            _playerManager = playerManager;
            eventManager.AddEventListener(this, this);
        }

        protected override void OnLoad(bool isFromReload)
        {
            Logger.LogWarning("Successfully Loaded PVEManager!");
        }

        protected override void OnUnload()
        {
            Logger.LogInformation("Unload");
        }

        public void FixedUpdate()
        {
            if (ConfigurationInstance.NoEquipWeaponsWhilePVE)
            {
                Check();
            }
        }

        private void Check()
        {
            if (Provider.clients.Count > 0)
            {
                var players = _playerManager.OnlinePlayers.ToList();
                foreach (IPlayer player in players)
                {
                    if (!(player is UnturnedPlayer uPlayer))
                        continue;

                    if (IsPVE(player))
                    {
                        if (uPlayer.Player.equipment.useable is UseableGun
                            || uPlayer.Player.equipment.useable is UseableMelee
                            || uPlayer.Player.equipment.useable is UseableThrowable)
                        {
                            uPlayer.Player.equipment.dequip();
                        }
                    }
                }
            }
        }

        public override Dictionary<string, string> DefaultTranslations => new Dictionary<string, string>
        {
            { "command_already", "You are already in {0} Mode" },
            { "command_public", "{1} is now in {0} Mode" },
            { "command_self", "You are now in {0} Mode" },
        };

        public void HandleEvent(IEventEmitter emitter, PlayerConnectedEvent @event)
        {

        }

        private readonly HashSet<IPlayer> _pvePlayers = new HashSet<IPlayer>();

        public bool IsPVE(IPlayer player)
        {
            return _pvePlayers.Contains(player);
        }

        public void SetPVE(IPlayer player, bool val)
        {
            if (val)
                _pvePlayers.Remove(player);
            else
                _pvePlayers.Add(player);
        }

        public void HandleEvent(IEventEmitter emitter, PlayerDamageEvent @event)
        {
            var player = @event.Player;

            if (ConfigurationInstance.GodModeWhilePVE)
            {
                if (IsPVE(player))
                {
                    @event.IsCancelled = true;
                }
            }

            /*
            if (ConfigurationInstance.VanishWhilePVE)
            {
                if (IsPVE(player))
                {
                    player.VanishMode = true;
                }
            }
            */
        }
    }
}
