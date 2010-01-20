//Generic Disease Template
//Sickness and Affliction System
//   by Bujinsho
//  www.New-Valoria.net


using System;
using Server;

namespace Server.Items
{
    public class GenericAffliction : Affliction
    {
        [Constructable]
        public GenericAffliction()
            : base("GenericAffliction", 0.5, 100, 1)
        {
            Name = "Generic Affliction";
            Severity = 10;
        }

        public override void ApplyAffliction(Mobile affected, int severity)
        {
            affected.Say("I have contracted the Generic Affliction");
        }

        public override void DoSymptoms(Mobile victim, int severity)
        {
            victim.Say("I am suffering the (non)effects of the Generic Affliction");
        }

        public override void CancelEffects(Mobile cured)
        {
            cured.Say("My Generic Affliction is gone");
        }

        public GenericAffliction(Serial serial)
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