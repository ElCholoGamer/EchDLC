using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EchDLC.Projectiles
{
    public class EchPet : ModProjectile
    {
        public override string Texture => "FargowiltasSoulsDLC/Base/NPCs/Echdeath";

        private Vector2 relativePosition = Vector2.UnitX * Main.rand.NextFloat(200f, 300f);

        public override void SetStaticDefaults()
        {
            int echType = ModLoader.GetMod("FargowiltasSoulsDLC").NPCType("Echdeath");
            Main.projFrames[projectile.type] = Main.npcFrameCount[echType];
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = projectile.height = 80;
            projectile.penetrate = -1;
            projectile.netImportant = true;
            projectile.timeLeft *= 5;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            EchPetPlayer modPlayer = player.GetModPlayer<EchPetPlayer>();

            #region Owner check
            if (!player.active)
            {
                projectile.active = false;
                return;
            }

            if (player.dead)
            {
                modPlayer.echPet = false;
            }

            if (modPlayer.echPet)
            {
                projectile.timeLeft = 2;
            }
            #endregion

            #region Animation and visuals
            if (++projectile.frameCounter > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % Main.projFrames[projectile.type];
            }

            Vector2 targetPosition = player.Center + relativePosition;
            int newDirection = Math.Sign(targetPosition.X - projectile.Center.X);
            if (newDirection != 0)
                projectile.spriteDirection = projectile.direction = newDirection;

            projectile.scale = 1f + modPlayer.echTime * 0.0005f; // yes
            #endregion

            #region Movement
            if (relativePosition == null)
                relativePosition = player.Center;

            float distanceLeft = projectile.Distance(targetPosition);

            if (Main.myPlayer == projectile.owner && distanceLeft < 400f && modPlayer.echTime % 60 == 0)
            {
                // Change relative position to player
                float rotate = Main.rand.NextFloat(MathHelper.PiOver2, MathHelper.Pi) * (Main.rand.NextBool() ? 1f : -1f);
                Vector2 direction = Vector2.Normalize(relativePosition).RotatedBy(rotate);

                float multiply = 1f + modPlayer.echTime * 0.000002f;
                float distance = Main.rand.NextFloat(200f * multiply, 300f * multiply);
                relativePosition = direction * distance;

                projectile.netUpdate = true;
            }

            if (distanceLeft > 1500f)
            {
                projectile.position = targetPosition;
            } else
            {
                float speed = 15f;
                float inertia = 80f;
                Vector2 direction = Vector2.Normalize(targetPosition - projectile.Center);

                projectile.velocity = (projectile.velocity * (inertia - 1) + direction * speed) / inertia;
            }
            #endregion

            modPlayer.echTime++;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];

            int frameHeight = texture.Height / Main.projFrames[projectile.type];
            Vector2 origin = new Vector2(texture.Width, frameHeight) / 2f;

            Rectangle sourceRectangle = new Rectangle(0, projectile.frame * frameHeight, texture.Width, frameHeight);
            SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(
                texture,
                projectile.Center - Main.screenPosition,
                sourceRectangle,
                lightColor,
                projectile.rotation,
                origin,
                projectile.scale,
                effects,
                0f);

            return false;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.WritePackedVector2(relativePosition);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            relativePosition = reader.ReadPackedVector2();
        }
    }
}
