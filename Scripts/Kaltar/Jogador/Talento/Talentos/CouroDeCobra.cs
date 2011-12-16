using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class CouroDeCobra : HabilidadeTalento
    {
		
		private static CouroDeCobra instance = new CouroDeCobra();
		public static CouroDeCobra Instance {
			get {return instance;}
		}		
		
		private CouroDeCobra() {
            id = (int)IdHabilidadeTalento.couroDeCobra;
            nome = "Couro de Cobra";
            descricao = "Você adquiriu resistência ao veneno, diminuindo assim os eventos dele no seu corpo. <br/> Você recebe bônus na resistência a veneno. (3 - 6).";
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
            if (ResistanceType.Poison.Equals(type))
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
