using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class FolegoRevigorante : HabilidadeTalento
    {
		
		private static FolegoRevigorante instance = new FolegoRevigorante();
		public static FolegoRevigorante Instance {
			get {return instance;}
		}		
		
		private FolegoRevigorante() {
			id = (int)IdHabilidadeTalento.folegoRevigorante;
			nome = "Folego Revigorante";
            descricao = "Quando um ataque é aparado, você se sente mais confiante e consegue recuperar de ferimentos leves. <br/> Quando um ataque é aparado 50% do valor perícia de aparar é recuperado em pontos de vida.";
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

            if (defensor.Stam + cura > defensor.StamMax)
            {
                cura = defensor.StamMax;
            }

            defensor.Stam += cura;

            defensor.PublicOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Folego Revigorante!!!");
        }
	}
}
