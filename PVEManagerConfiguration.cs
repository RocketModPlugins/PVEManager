using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using Rocket.Core.Configuration;

namespace PVEManager
{
    public class PVEManagerConfiguration
    {
        public bool GodModeWhilePVE { get; set; } = true;
        //public bool VanishWhilePVE { get; set; } = false;
        public bool NoEquipWeaponsWhilePVE { get; set; } = true;
        public bool AnnounceWhenPVE { get; set; } = true;
    }
}
