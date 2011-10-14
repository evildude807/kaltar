using System;
using Server.Mobiles;
using Server.Kaltar.Gumps;

namespace Server.Kaltar.Morte
{
    public class PedraSairSalaMorte : Item
    {
    	[Constructable]
		public PedraSairSalaMorte() : base( 0xED4 ) {
			Movable = false;
			Name = "Pedra dos mortos";		
		}

        public PedraSairSalaMorte(Serial serial)
            : base(serial)
        {
		}
		
		public override void OnDoubleClick( Mobile from ) {
            Jogador jogador = (Jogador)from;
            if (jogador.Alive)
            {
                jogador.SendMessage("Voce esta vivo, nao pode usar esta pedra, ela e para os mortos.");
            }
            else
            {
                if (jogador.getSistemaMorte().podeReviver())
                {
                    jogador.getSistemaMorte().levantarMorte();
                    jogador.SendMessage("Voce acaba de voltar a vida, tome mais cuidado.");
                }
            }

		}
		
		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
		}	
    }
}
