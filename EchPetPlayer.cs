using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace EchDLC
{
    public class EchPetPlayer : ModPlayer
    {
        public bool echPet = false;
        public int echTime = 0;

        public override void ResetEffects()
        {
            echPet = false;
        }

        public override void clientClone(ModPlayer clientClone)
        {
            EchPetPlayer modClone = clientClone as EchPetPlayer;
            modClone.echTime = echTime;
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                ["echTime"] = echTime
            };
        }

        public override void Load(TagCompound tag)
        {
            echTime = tag.GetInt("echTime");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            echTime = reader.ReadInt32();
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write(echTime);
            packet.Send(toWho, fromWho);
        }

        
    }
}
