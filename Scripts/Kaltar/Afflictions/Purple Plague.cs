//Example Disease
//Sickness and Affliction System
//   by Bujinsho
//  www.New-Valoria.net

using System;
using Server;

namespace Server.Items
{
    public class PurplePlague : Affliction
    {
        private int org_Hue;
        private int Stage;

        [Constructable]
        public PurplePlague()
            : base("PurplePlague", 70, 45, 180)
        {
            Name = "Purple Plague";
        }

        public override void ApplyAffliction(Mobile affected, int severity)
        {
            affected.SendMessage("You have caught The Purple Plague!!");

            org_Hue = affected.Hue;
            affected.Hue = 23;
            Stage = 1;

            
        }

        public override void DoSymptoms(Mobile victim, int severity)
        {
            switch (Stage)
            {
                case 1:
                    victim.Poison = Poison.Lesser;
                    break;
                case 2:
                    victim.Poison = Poison.Regular;
                    break;
                case 3:
                    victim.Poison = Poison.Greater;
                    break;
                case 4:
                    victim.Poison = Poison.Deadly;
                    break;
                case 5:
                    victim.Poison = Poison.Lethal;
                    break;
            }
            victim.SendMessage("The Purple Plague is slowly but surely killing you!!");

            if (Utility.RandomDouble() < 0.5)
                Stage++;

            if (Stage > 5)
                Stage = 5;
        }

        public override void CancelEffects(Mobile cured)
        {
            cured.Hue = org_Hue;

            cured.SendMessage("You have survived The Purple Plague");
        }

        public PurplePlague(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            writer.Write(org_Hue);
            writer.Write(Stage);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            org_Hue = reader.ReadInt();
            Stage = reader.ReadInt();
        }
    }
}