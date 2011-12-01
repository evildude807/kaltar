using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using Kaltar.Habilidades;
using Kaltar.Raca;
using Kaltar.Talentos;

namespace Kaltar.Util
{

	public sealed class AtributoUtil {

        #region instancia
        
        private static AtributoUtil instance = new AtributoUtil();
		public static AtributoUtil Instance {
			get {
				return instance;
			}
		}
		
		private AtributoUtil() {
        }

        #endregion

        public enum AtributoTipo {
            vida, 
            folego,
            mana,
            forca,
            destreza,
            inteligencia,
            carga
        }

        /*
         * Recupera os bonus que o jogador tem sobre a vida.
         * 
         */ 
        public int vidaBonus(Jogador jogador)
        {
            int bonus = AosAttributes.GetValue(jogador, AosAttribute.BonusHits);
            bonus += atributoBonus(jogador, AtributoTipo.vida);
            return bonus;
        }

        /*
         * Recupera os bonus que o jogador tem sobre a folego.
         * 
         */
        public int folegoBonus(Jogador jogador)
        {
            int bonus = AosAttributes.GetValue(jogador, AosAttribute.BonusStam);
            bonus += atributoBonus(jogador, AtributoTipo.folego);
            return bonus;
        }

        /*
         * Recupera os bonus que o jogador tem sobre a mana.
         * 
         */
        public int manaBonus(Jogador jogador)
        {
            int bonus = AosAttributes.GetValue(jogador, AosAttribute.BonusMana);
            bonus += atributoBonus(jogador, AtributoTipo.mana);
            return bonus;
        }

        public int forcaBonus(Jogador jogador)
        {
            return atributoBonus(jogador, AtributoTipo.forca);
        }

        public int destrezaBonus(Jogador jogador)
        {
            return atributoBonus(jogador, AtributoTipo.destreza);
        }

        public int inteligenciaBonus(Jogador jogador)
        {
            return atributoBonus(jogador, AtributoTipo.inteligencia);
        }

        public int cargaBonus(Jogador jogador)
        {
            return atributoBonus(jogador, AtributoTipo.carga);
        }

        private int atributoBonus(Jogador jogador, AtributoTipo atributoTipo) {
            int bonus = 0;

            //habilidade racial
            Dictionary<IdHabilidadeRacial, HabilidadeNode> racial = jogador.getSistemaRaca().getHabilidades();
            List<HabilidadeNode> habilidadesNode = new List<HabilidadeNode>(racial.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.racial, atributoTipo);

            //habilidade talento
            Dictionary<IdHabilidadeTalento, HabilidadeNode> talento = jogador.getSistemaTalento().getHabilidades();
            habilidadesNode = new List<HabilidadeNode>(talento.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.talento, atributoTipo);

            return bonus;
        }

        private int getBonus(List<HabilidadeNode> habilidadesNode, HabilidadeTipo tipo, AtributoTipo atributoTipo)
        {
            int bonus = 0;
            Habilidade habilidade = null;

            foreach (HabilidadeNode node in habilidadesNode)
            {
                habilidade = Habilidade.getHabilidade(node.Id, tipo);

                if (AtributoTipo.vida.Equals(atributoTipo))
                {
                    bonus += habilidade.vidaBonus(node);
                }
                else if (AtributoTipo.folego.Equals(atributoTipo))
                {
                    bonus += habilidade.folegoBonus(node);
                }
                else if (AtributoTipo.mana.Equals(atributoTipo))
                {
                    bonus += habilidade.manaBonus(node);
                }
                else if (AtributoTipo.forca.Equals(atributoTipo))
                {
                    bonus += habilidade.forcaBonus(node);
                }
                else if (AtributoTipo.destreza.Equals(atributoTipo))
                {
                    bonus += habilidade.destrezaBonus(node);
                }
                else if (AtributoTipo.inteligencia.Equals(atributoTipo))
                {
                    bonus += habilidade.inteligenciaBonus(node);
                }
                else if (AtributoTipo.carga.Equals(atributoTipo))
                {
                    bonus += habilidade.cargaBonus(node);
                }
            }

            return bonus;
        }
    }
}
