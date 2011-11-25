using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class CapacidadeCarga : HabilidadeRacial {
		
		private static CapacidadeCarga instance = new CapacidadeCarga();
		public static CapacidadeCarga Instance {
			get {return instance;}
		}

        private CapacidadeCarga()
        {
            id = (int)IdHabilidadeRacial.capacidadeCarga;
            nome = "Capacidade de Carga";
            descricao = "Você é consegue sustentar mais peso.";
            preRequisito = "Raça Meio-Orc";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is MeioOrc;
		}

        /*
         * Bonus que a habilidade da para a inteligência.
         */
        public override int cargaBonus(HabilidadeNode node)
        {
            return getBonus(node.Nivel);
        }

        private int getBonus(int ponto)
        {
            return ponto * 15;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
        }
	}
}
