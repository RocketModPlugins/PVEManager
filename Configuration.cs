using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PVEManager
{
    public class Configuration : IRocketPluginConfiguration
    {
        public bool GodModeWhilePVE;
        public bool VanishWhilePVE;
        public bool NoEquipWeaponsWhilePVE;
        public bool AnnounceWhenPVE;

        [XmlArray("PVE_Players")]
        [XmlArrayItem("SteamID")]
        public List<string> PVE_Players = new List<string>();

        public void LoadDefaults()
        {
            GodModeWhilePVE = true;
            VanishWhilePVE = false;
            NoEquipWeaponsWhilePVE = true;
            AnnounceWhenPVE = true;

            PVE_Players = new List<string>() { };
        }
    }
}
