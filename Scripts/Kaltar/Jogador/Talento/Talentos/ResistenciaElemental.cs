using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class ResistenciaElemental : HabilidadeTalento
    {
		
		private static ResistenciaElemental instance = new ResistenciaElemental();
		public static ResistenciaElemental Instance {
			get {return instance;}
		}		
		
		private ResistenciaElemental() {
            id = (int)IdHabilidadeTalento.resistenciaElemental;
            nome = "Resistência Elemental";
            descricao = "Sua sintonia com os elemento e grande, fornecendo ao seu corpo resistência a eles. <br/> Você recebe bônus na resistência aos elementos fogo, energia e frio. (3 - 6).";
            preRequisito = "Classes: Aprendiz e Espiritualista";
            nivelMaximo = 2;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.getSistemaClasse().getClasse() is Aprendiz || jogador.getSistemaClasse().getClasse() is Seminarista)
            {
                return true;
            }

            return false;
		}

        /*
         * Bonus que a habilidade da no tipo de resistência.
         */
        public override int resistenciaBonus(HabilidadeNode node, ResistanceType type)
        {
            if (ResistanceType.Fire.Equals(type) || ResistanceType.Cold.Equals(type) || ResistanceType.Energy.Equals(type))
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
