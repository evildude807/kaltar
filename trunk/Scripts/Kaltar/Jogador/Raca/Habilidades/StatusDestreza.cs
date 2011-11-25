using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class StatusDestreza : HabilidadeRacial {
		
		private static StatusDestreza instance = new StatusDestreza();
		public static StatusDestreza Instance {
			get {return instance;}
		}

        private StatusDestreza()
        {
            id = (int)IdHabilidadeRacial.destreza;
            nome = "Destreza";
            descricao = "Você é mais agil.";
            preRequisito = "Raça Elfo";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is Elfo;
		}

        /*
         * Bonus que a habilidade da para a destreza.
         */
        public override int destrezaBonus(HabilidadeNode node)
        {
            return node.Nivel * 3;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.RawDex += ponto * 3;

        }
	}
}
