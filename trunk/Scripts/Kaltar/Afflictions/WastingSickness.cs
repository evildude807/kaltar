//Example Disease
//Sickness and Affliction System
//   by Bujinsho
//  www.New-Valoria.net

using System;
using Server;

namespace Server.Items
{
    public class WastingSickness : Affliction
    {
        [Constructable]
        public WastingSickness()
            : base("WastingSickness", 40, 120, 0)
        {
            Name = "Wasting Sickness";
        }

        public override void ApplyAffliction(Mobile affected, int severity)
        {
            affected.SendMessage("You have caught The Wasting Sickness!");            
        }

        public override void DoSymptoms(Mobile victim, int severity)
        {
            victim.RawStr--;

            if (victim.RawStr < Utility.Random(10, 50))
                Delete();
        }

        public override void CancelEffects(Mobile cured)
        {
            cured.SendMessage("You have survived The Wasting Sickness");
        }

        public WastingSickness(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}