using System;

namespace Server.ACC.CSS.Systems.Ancient
{
    public class AncientArmageddonScroll : CSpellScroll
    {
        [Constructable]
        public AncientArmageddonScroll()
            : this(1)
        {
        }

        [Constructable]
        public AncientArmageddonScroll(int amount)
            : base(typeof(AncientArmageddonSpell), 0x1F51, amount)
        {
            Name = "Armageddon Scroll";
            Hue = 1355;
        }

        public AncientArmageddonScroll(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
