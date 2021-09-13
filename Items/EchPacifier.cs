using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EchDLC.Items
{
    public class EchPacifier : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Catnip");
            Tooltip.SetDefault("'Cat Crack'");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 34;
            item.useAnimation = item.useTime = 45;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item57;
            item.consumable = true;
            item.maxStack = 99;
            item.value = 1;
            item.rare = ItemRarityID.Purple;
        }

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<EchDLCPlayer>().echTime = 0;
            Main.NewText("Echdeath has been pleased.", Color.LightGreen, false);

            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            Mod soulsMod = ModLoader.GetMod("FargowiltasSouls");
            Mod fargoMod = ModLoader.GetMod("Fargowiltas");

            recipe.AddIngredient(soulsMod, "EternitySoul");
            recipe.AddIngredient(soulsMod, "Sadism", 15);
            recipe.AddIngredient(soulsMod, "MutantScale", 15);
            recipe.AddIngredient(soulsMod, "DeviatingEnergy", 15);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddTile(fargoMod, "CrucibleCosmosSheet");
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}
