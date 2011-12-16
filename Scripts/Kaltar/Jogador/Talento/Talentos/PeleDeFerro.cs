using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class PeleDeFerro : HabilidadeTalento
    {
		
		private static PeleDeFerro instance = new PeleDeFerro();
		public static PeleDeFerro Instance {
			get {return instance;}
		}		
		
		private PeleDeFerro() {
            id = (int)IdHabilidadeTalento.peleDeferro;
			nome = "Pele de Ferro";
            descricao = "Você adquiriu resistência ao dano físico que diminui o poder do ataque. <br/> Você recebe bônus na resistência física. (3 - 6).";
            preRequisito = "Não tem.";
            nivelMaximo = 2;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return true;
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
