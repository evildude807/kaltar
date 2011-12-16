using System;
using System.Collections.Generic;
using System.Text;
using Kaltar.Habilidades;
using Kaltar.Raca;
using Kaltar.Talentos;

namespace Kaltar.Util {
    public sealed class RegeneracaoUtil {

		private static RegeneracaoUtil instance = new RegeneracaoUtil();
		
		public static RegeneracaoUtil Instance {
			get {return instance;}
		}

        private RegeneracaoUtil()
        {
		}

        /**
         * Bonus de pontos para diminuir o tempo de regeneracao.
         * 
         * Esse valor nao pode ser muito mal, pois assim ele irá regenerar muito rapido. O recomendavel seria
         * um valor 2 ou 3.
         * 
         */ 
        public int pontosDeVidaRegenBonus(Server.Mobiles.Jogador jogador)
        {
            int bonus = 0;

            //Trocar para o talento correto
            if (jogador.getSistemaTalento().possuiHabilidadeTalento(IdHabilidadeTalento.regeneracaoAprimorada))
            {
                bonus += 2;
            }

            return bonus;
        }

        /**
         * Bonus de pontos para diminuir o tempo de regeneracao.
         * 
         * Esse valor nao pode ser muito mal, pois assim ele irá regenerar muito rapido. O recomendavel seria
         * um valor 2 ou 3.
         * 
         */ 
        public int folegoRegenBonus(Server.Mobiles.Jogador jogador)
        {
            int bonus = 0;

            //Trocar para o talento correto
            if (jogador.getSistemaTalento().possuiHabilidadeTalento(IdHabilidadeTalento.folegoAprimorado))
            {
                bonus += 2;
            }

            return bonus;
        }

        /**
         * Bonus de pontos para diminuir o tempo de regeneracao.
         * 
         * Esse valor nao pode ser muito mal, pois assim ele irá regenerar muito rapido. O recomendavel seria
         * um valor 2 ou 3.
         * 
         */ 
        public int manaRegenBonus(Server.Mobiles.Jogador jogador)
        {
            int bonus = 0;

            //Trocar para o talento correto
            if (jogador.getSistemaTalento().possuiHabilidadeTalento(IdHabilidadeTalento.manaAprimorado))
            {
                bonus += 2;
            }

            return bonus;
        }
    }
}
