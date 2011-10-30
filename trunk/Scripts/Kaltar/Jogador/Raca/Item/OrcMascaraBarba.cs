using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Kaltar.Items {

    /**
     * Essa classe é a barba utilizada pelo jogadores da raça meio-orc. Ela é a cara do orc. 
     * E orc não podem ter barba, apenas essa.
     * 
     */
    public class OrcMascaraBarba : Beard {

        private OrcMascaraBarba() : this(0x8A4)
		{
		}
        
		public OrcMascaraBarba( int hue ) : base(0x141B, hue)
		{
		}

		public OrcMascaraBarba( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

    }
}
