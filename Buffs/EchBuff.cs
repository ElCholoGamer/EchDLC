using Terraria;
using Terraria.ModLoader;
using EchDLC.Projectiles;

namespace EchDLC.Buffs
{
    public class EchBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            //player.GetModPlayer<ExamplePlayer>().examplePet = true;

            bool echAlive = player.ownedProjectileCounts[ModContent.ProjectileType<EchPet>()] > 0;
            if (!echAlive && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<EchPet>(), 0, 0f, player.whoAmI);
            }
        }
    }
}
