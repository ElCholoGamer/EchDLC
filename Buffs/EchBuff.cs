using Terraria;
using Terraria.ModLoader;
using EchDLC.Projectiles;

namespace EchDLC.Buffs
{
    public class EchBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ech");
            Description.SetDefault("ech.");

            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<EchPetPlayer>().echPet = true;

            int projType = ModContent.ProjectileType<EchPet>();

            bool echAlive = player.ownedProjectileCounts[projType] > 0;
            if (!echAlive && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, projType, 0, 0f, player.whoAmI);
            }
        }
    }
}
