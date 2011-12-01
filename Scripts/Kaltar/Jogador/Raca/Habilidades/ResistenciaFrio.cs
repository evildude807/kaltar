using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class ResistenciaFrio : HabilidadeRacial {
		
		private static ResistenciaFrio instance = new ResistenciaFrio();
		public static ResistenciaFrio Instance {
			get {return instance;}
		}

        private ResistenciaFrio()
        {
            id = (int)IdHabilidadeRacial.resistenciaFrio;
            nome = "Resistência Frio";
            descricao = "Você é mais resistente ao frio.";
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
            if (ResistanceType.Cold.Equals(type))
            {
                return node.Nivel * 5;
            }
            return 0;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            jogador.UpdateResistances();
        }
	}
}
