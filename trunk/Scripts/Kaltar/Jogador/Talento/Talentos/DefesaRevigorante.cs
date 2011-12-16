using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class DefesaRevigorante : HabilidadeTalento
    {
		
		private static DefesaRevigorante instance = new DefesaRevigorante();
		public static DefesaRevigorante Instance {
			get {return instance;}
		}		
		
		private DefesaRevigorante() {
			id = (int)IdHabilidadeTalento.defesaRevigorante;
			nome = "Defesa Revigorante";
            descricao = "Quando um ataque é aparado, você se sente mais confiante e consegue recuperar de ferimentos leves. <br/> Quando um ataque é aparado 50% do valor perícia de aparar é recuperado em pontos de vida.";
            preRequisito = "Classe: Homem de arma, Talento: Escudo Protetor";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.getSistemaClasse().getClasse() is Escudeiro)
            {
                //Tiago, falta colocar o teste do Talento escudo protetor
                return true;
            }

            return false;
		}

        /**
         * Ocorre quando um ataque e aparado.
         * dano = Dano recebido se nao fosse aparado.
         */
        public override void onAparar(Mobile attacker, Jogador defensor, int dano)
        {
            int cura = (int) (dano * 0.5);
            defensor.Heal(cura, defensor, true);

            defensor.PublicOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Defesa Revigorando!!!");
        }
	}
}
