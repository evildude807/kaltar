using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using Kaltar.Raca;
using Kaltar.Talentos;
using Kaltar.Habilidades;

namespace Kaltar.Util
{
	/// <summary>
	/// Description of ResistenciaUtil.
	/// </summary>
	public sealed class ResistenciaUtil {
		private static ResistenciaUtil instance = new ResistenciaUtil();
		public static ResistenciaUtil Instance {
			get {
				return instance;
			}
		}
		
		private ResistenciaUtil() {
		}
		
		/**
		 * Retorna o bônus que o jogador tem para o tipo de resistencia.
		 */ 
		public int bonusResistencia(Jogador jogador, ResistanceType type) {

            int bonus = 0;

            //habilidade racial
            Dictionary<IdHabilidadeRacial, HabilidadeNode> racial = jogador.getSistemaRaca().getHabilidades();
            List<HabilidadeNode> habilidadesNode = new List<HabilidadeNode>(racial.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.racial, type);

            //habilidade talento
            Dictionary<IdHabilidadeTalento, HabilidadeNode> talento = jogador.getSistemaTalento().getHabilidades();
            habilidadesNode = new List<HabilidadeNode>(talento.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.talento, type);

            return bonus;
		}

        private int getBonus(List<HabilidadeNode> habilidadesNode, HabilidadeTipo tipo, ResistanceType type)
        {
            int bonus = 0;
            Habilidade habilidade = null;

            foreach (HabilidadeNode node in habilidadesNode)
            {
                habilidade = Habilidade.getHabilidade(node.Id, tipo);
                bonus += habilidade.resistenciaBonus(node, type);
            }

            return bonus;
        }

        /**
         * Realiza o teste se o alvo conseguiu resistir ao tipo de ataque. 
         * O teste é baseado na resistencia menos a dificultade.
         * A dificuldade deve ser um valor entre 0 a 100. Que será como %.
         * 
         * Ex.: (Resistencia - dificuldade) %
         */ 
        public bool resistiu(Mobile alvo, int dificultade, ResistanceType tipo)
        {
            bool resistiu = false;

            int valorResistencia = alvo.GetResistance(tipo);
            valorResistencia -= dificultade;

            //ajusta para ter mínimo de 5% e máximo de 95%
            valorResistencia = valorResistencia < 5 ? 5 : valorResistencia;
            valorResistencia = valorResistencia > 100 ? 95 : valorResistencia;
            valorResistencia /= 100;

            if (valorResistencia > Utility.RandomDouble())
            {
                resistiu = true;
            }

            return resistiu;
        }
	}
}
