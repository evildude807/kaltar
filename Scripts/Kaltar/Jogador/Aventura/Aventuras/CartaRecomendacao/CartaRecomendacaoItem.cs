using Server;
using Server.Mobiles;
using Server.Items;
using System;

namespace Kaltar.aventura {

	public class CartaRecomendacaoItem : Item {
		
		[Constructable]
		public CartaRecomendacaoItem(Jogador jogador) : base(0x14EC){
			Weight = 1.0;
			Name = "Carta de recomendação para " + jogador.Name;
		}

		public CartaRecomendacaoItem( Serial serial ) : base( serial ) {
		}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
		}
	}
}
