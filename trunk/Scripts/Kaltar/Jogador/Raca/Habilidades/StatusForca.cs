using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class StatusForca : HabilidadeRacial {
		
		private static StatusForca instance = new StatusForca();
		public static StatusForca Instance {
			get {return instance;}
		}

        private StatusForca()
        {
            id = (int)IdHabilidadeRacial.destreza;
            nome = "Força";
            descricao = "Você é mais forte.";
            preRequisito = "Raça Meio-Orc";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is MeioOrc;
		}

        /*
         * Bonus que a habilidade da para a destreza.
         */
        public override int forcaBonus(HabilidadeNode node)
        {
            return node.Nivel * 3;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.RawStr += ponto * 3;

        }
	}
}
