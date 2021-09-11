using Terraria.ModLoader;

namespace EchDLC
{
    public class EchPetPlayer : ModPlayer
    {
        public bool echPet = false;

        public override void ResetEffects()
        {
            echPet = false;
        }
    }
}
