using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class ManaRevigorante : HabilidadeTalento
    {
		
		private static ManaRevigorante instance = new ManaRevigorante();
		public static ManaRevigorante Instance {
			get {return instance;}
		}		
		
		private ManaRevigorante() {
			id = (int)IdHabilidadeTalento.manaRevigorante;
			nome = "Mana Revigorante";
            descricao = "Quando um ataque é aparado, você se sente mais confiante e consegue recuperar parte da sua Mana. <br/> Quando um ataque é aparado 50% do valor perícia de aparar é recuperado parte da sua Mana.";
            preRequisito = "Classe: Homem de arma ou Espiritualista, Talento: Escudo Protetor";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return true;
		}

        /**
         * Ocorre quando um ataque e aparado.
         * dano = Dano recebido se nao fosse aparado.
         */
        public override void onAparar(Mobile attacker, Jogador defensor, int dano)
        {
            int cura = (int)(dano * 0.5);

            if (defensor.Mana + cura > defensor.ManaMax)
            {
                cura = defensor.ManaMax;
            }

            defensor.Mana += cura;

            defensor.PublicOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Mana de Revigorante!!!");
        }
	}
}
