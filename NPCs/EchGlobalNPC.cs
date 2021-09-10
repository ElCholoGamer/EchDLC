using Terraria;
using Terraria.ModLoader;

namespace EchDLC.NPCs
{
    public class EchGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            Mod soulsDLC = ModLoader.GetMod("FargowiltasSoulsDLC");
            if (npc.type == soulsDLC.NPCType("Echdeath"))
            {

            }
        }
    }
}
pepper