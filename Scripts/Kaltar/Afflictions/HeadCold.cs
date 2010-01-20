//Example Disease
//Sickness and Affliction System
//   by Bujinsho
//  www.New-Valoria.net

using System;
using Server;

namespace Server.Items
{
    public class HeadCold : Affliction
    {
        private int org_Int;

        [Constructable]
        public HeadCold()
            : base("HeadCold", 10, 15, 48)
        {
            Name = "Head Cold";
            Severity = Utility.Random(3) + 1;
        }

        public override void ApplyAffliction(Mobile affected, int severity)
        {
            affected.SendMessage("You seem to have caught a Head Cold");
            affected.SendMessage("You feel dull and slow-witted");

            org_Int = affected.RawInt;

            switch (severity)
            {
                case 1:
                    affected.RawInt -= (affected.RawInt / 10);
                    break;
                case 2:
                    affected.RawInt -= (affected.RawInt / 10) * 2;
                    break;
                case 3:
                    affected.RawInt -= (affected.RawInt / 10) * 3;
                    break;
            }
        }

        public override void DoSymptoms(Mobile victim, int severity)
        {
            switch (severity)
            {
                case 1:
                    victim.SendMessage("You have a headache");
                    break;
                case 2:
                    victim.SendMessage("You have a bad headache");
                    break;
                case 3:
                    victim.SendMessage("You have a very bad headache");
                    break;
            }
            victim.SendMessage("It it hard to think straight or concentrate properly");
        }

        public override void CancelEffects(Mobile cured)
        {
            cured.RawInt = org_Int;

            cured.SendMessage("Your Head Cold seems to have gone");
        }

        public HeadCold(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            writer.Write(org_Int);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            org_Int = reader.ReadInt();
        }
    }
}