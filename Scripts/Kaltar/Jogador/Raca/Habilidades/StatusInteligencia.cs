using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class StatusInteligencia : HabilidadeRacial {

        private static StatusInteligencia instance = new StatusInteligencia();
		public static StatusInteligencia Instance {
			get {return instance;}
		}

        private StatusInteligencia()
        {
            id = (int)IdHabilidadeRacial.inteligencia;
            nome = "Inteligência";
            descricao = "Você é mais inteligente.";
            preRequisito = "Raça Elfo Negro";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is ElfoNegro;
		}

        /*
         * Bonus que a habilidade da para a destreza.
         */
        public override int inteligenciaBonus(HabilidadeNode node)
        {
            return node.Nivel * 3;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.RawInt += ponto * 3;

        }
	}
}
