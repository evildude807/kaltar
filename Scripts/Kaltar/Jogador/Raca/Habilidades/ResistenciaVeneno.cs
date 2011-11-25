using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class ResistenciaVeneno : HabilidadeRacial {
		
		private static ResistenciaVeneno instance = new ResistenciaVeneno();
		public static ResistenciaVeneno Instance {
			get {return instance;}
		}

        private ResistenciaVeneno()
        {
            id = (int)IdHabilidadeRacial.resistenciaVeneno;
            nome = "Resistência Veneno";
            descricao = "Você é mais resistente a veneno.";
            preRequisito = "Raça Elfo";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is Elfo;
		}
        
        /*
         * Bonus que a habilidade da no tipo de resistência.
         */
        public override int resistenciaBonus(HabilidadeNode node, ResistanceType type)
        {
            if (ResistanceType.Poison.Equals(type))
            {
                return node.Nivel * 5;
            }
            return 0;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
        }
	}
}
