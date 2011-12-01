using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class ResistenciaFisica : HabilidadeRacial {
		
		private static ResistenciaFisica instance = new ResistenciaFisica();
		public static ResistenciaFisica Instance {
			get {return instance;}
		}

        private ResistenciaFisica()
        {
            id = (int)IdHabilidadeRacial.resistenciaFisica;
            nome = "Resistência Física";
            descricao = "Você é mais resistente a pancadas.";
            preRequisito = "Raça Meio-Orc";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is MeioOrc;
		}


        /*
         * Bonus que a habilidade da no tipo de resistência.
         */
        public override int resistenciaBonus(HabilidadeNode node, ResistanceType type)
        {
            if (ResistanceType.Physical.Equals(type))
            {
                return node.Nivel * 3;
            }
            return 0;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            jogador.UpdateResistances();
        }
	}
}
