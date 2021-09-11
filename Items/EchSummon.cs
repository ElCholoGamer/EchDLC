using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using EchDLC.Projectiles;
using EchDLC.Buffs;

namespace EchDLC.Items
{
    public class EchSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Ech");
            Tooltip.SetDefault("ech.");
        }

        public override void SetDefaults()
        {
            item.width = item.height = 40;
            item.noMelee = true;
            item.useAnimation = item.useTime = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<EchPet>();
            item.buffType = ModContent.BuffType<EchBuff>();
            item.value = 1;
            item.rare = ItemRarityID.Purple;

        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
                Main.NewText("Add buff");
            }
        }
    }
}
