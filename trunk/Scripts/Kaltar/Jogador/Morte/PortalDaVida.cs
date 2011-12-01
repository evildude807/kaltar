using System;
using Server.Mobiles;
using Server.Kaltar.Gumps;

namespace Server.Kaltar.Morte
{
    public class PortalDaVida : Item
    {
    	[Constructable]
        public PortalDaVida() : base(0xF6C)
        {
			Movable = false;
            Hue = 0x2D1;
            Light = LightType.Circle150;
			Name = "Portal da Vida";
		}

        public PortalDaVida(Serial serial)
            : base(serial)
        {
		}

        public override bool OnMoveOver(Mobile from)
        {
            Jogador jogador = (Jogador)from;
            if (jogador.Alive)
            {
                jogador.SendMessage("Voce esta vivo, não pode usar esta pedra, ela é para os mortos.");
            }
            else
            {
                if (jogador.getSistemaMorte().podeReviver())
                {
                    jogador.getSistemaMorte().levantarMorte();
                    jogador.SendMessage("Você acaba de voltar a vida, tome mais cuidado.");
                }
            }

            return false;
        }
		
		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );
		}	
    }
}
