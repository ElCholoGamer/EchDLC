using Terraria;
using Terraria.ModLoader;
using EchDLC.Items;

namespace EchDLC.NPCs
{
    public class EchGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            Mod soulsDLC = ModLoader.GetMod("FargowiltasSoulsDLC");
            if (npc.type == soulsDLC.NPCType("Echdeath"))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<EchSummon>());
            }
        }
    }
}
