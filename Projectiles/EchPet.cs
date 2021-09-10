using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EchDLC.Projectiles
{
    public class EchPet : ModProjectile
    {
        public override string Texture => "FargowiltasSoulsDLC/Base/NPCs/Echdeath";
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 50;
            projectile.penetrate = -1;
            projectile.netImportant = true;
            projectile.timeLeft *= 5;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player owner = Main.player[projectile.owner];

            #region Owner check
            if (!owner.active)
            {
                projectile.active = false;
                return;
            }

            if (owner.dead)
            {
                projectile.timeLeft = 2;
            }
            #endregion

            #region Movement
            float speed = 10f;
            float inertia = 50f;
            Vector2 maxVelocity = projectile.DirectionTo(owner.Center) * speed;

            projectile.velocity = (projectile.velocity * (inertia - 1) + maxVelocity) / inertia;
            #endregion
        }
    }
}
