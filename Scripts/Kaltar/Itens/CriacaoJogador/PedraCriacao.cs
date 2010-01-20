using System;
using Server.Mobiles;
using Server.Kaltar.Gumps;
	
namespace Server.Kaltar.Items {

	public class PedraCriacao : Item {
		
		[Constructable]
		public PedraCriacao() : base( 0xED4 ) {
			Movable = false;
			Name = "Pedra de criaçao";		
		}
		
		public PedraCriacao( Serial serial ) : base( serial ) {
		}
		
		public override void OnDoubleClick( Mobile from ) {
			from.SendGump( new EscolhaAtributosGump( (Jogador)from, 10, 10, 10) );
		}
		
		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
		}		
	}
}
