using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Items;
using Kaltar.Habilidades;
using Kaltar.Raca;
using Kaltar.Talentos;

namespace Kaltar.Util
{

	public sealed class CombateUtil {

        #region instancia
        
        private static CombateUtil instance = new CombateUtil();
		public static CombateUtil Instance {
			get {
				return instance;
			}
		}
		
		private CombateUtil() {
        }

        #endregion

        public int acertarBonus(Jogador jogador, WeaponType tipo)
        {
            int bonus = 0;

            //habilidade racial
            Dictionary<IdHabilidadeRacial, HabilidadeNode> racial = jogador.getSistemaRaca().getHabilidades();
            List<HabilidadeNode> habilidadesNode = new List<HabilidadeNode>(racial.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.racial, tipo);

            //habilidade talento
            Dictionary<IdHabilidadeTalento, HabilidadeNode> talento = jogador.getSistemaTalento().getHabilidades();
            habilidadesNode = new List<HabilidadeNode>(talento.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.talento, tipo);

            return bonus;
        }

        private int getBonus(List<HabilidadeNode> habilidadesNode, HabilidadeTipo tipo, WeaponType weaponType)
        {
            int bonus = 0;
            Habilidade habilidade = null;

            foreach (HabilidadeNode node in habilidadesNode)
            {
                habilidade = Habilidade.getHabilidade(node.Id, tipo);
                bonus += habilidade.acertarBonus(node, weaponType);
            }

            return bonus;
        }

        public int defenderBonus(Jogador jogador)
        {
            int bonus = 0;

            BaseWeapon arma = jogador.Weapon as BaseWeapon;
            Item escudo = jogador.ShieldArmor;

            //habilidade racial
            Dictionary<IdHabilidadeRacial, HabilidadeNode> racial = jogador.getSistemaRaca().getHabilidades();
            List<HabilidadeNode> habilidadesNode = new List<HabilidadeNode>(racial.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.racial, arma, escudo);

            //habilidade talento
            Dictionary<IdHabilidadeTalento, HabilidadeNode> talento = jogador.getSistemaTalento().getHabilidades();
            habilidadesNode = new List<HabilidadeNode>(talento.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.racial, arma, escudo);

            return bonus;
        }

        private int getBonus(List<HabilidadeNode> habilidadesNode, HabilidadeTipo tipo, BaseWeapon arma, Item escudo)
        {

            int bonus = 0;
            Habilidade habilidade = null;

            foreach (HabilidadeNode node in habilidadesNode)
            {
                habilidade = Habilidade.getHabilidade(node.Id, tipo);
                bonus += habilidade.defenderBonus(node, arma, escudo);
            }

            return bonus;
        }

        public double apararBonus(Jogador jogador, Item item)
        {
            int bonus = 0;

            //habilidade racial
            Dictionary<IdHabilidadeRacial, HabilidadeNode> racial = jogador.getSistemaRaca().getHabilidades();
            List<HabilidadeNode> habilidadesNode = new List<HabilidadeNode>(racial.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.racial, item);

            //habilidade talento
            Dictionary<IdHabilidadeTalento, HabilidadeNode> talento = jogador.getSistemaTalento().getHabilidades();
            habilidadesNode = new List<HabilidadeNode>(talento.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.racial, item);

            return bonus;
        }

        private int getBonus(List<HabilidadeNode> habilidadesNode, HabilidadeTipo tipo, Item item)
        {
            int bonus = 0;
            Habilidade habilidade = null;

            foreach (HabilidadeNode node in habilidadesNode)
            {
                habilidade = Habilidade.getHabilidade(node.Id, tipo);
                bonus += habilidade.apararBonus(node, item);
            }

            return bonus;
        }

        public int danoBonus(Jogador atacante, Mobile defensor)
        {
            int bonus = 0;

            /*
            BaseWeapon arma = atacante.Weapon as BaseWeapon;
            WeaponType  weaponTipo = WeaponType.Fists;
            if (arma != null)
            {
                weaponTipo = arma.Type;
            }*/

            //habilidade racial
            Dictionary<IdHabilidadeRacial, HabilidadeNode> racial = atacante.getSistemaRaca().getHabilidades();
            List<HabilidadeNode> habilidadesNode = new List<HabilidadeNode>(racial.Values);
            bonus += getDanoBonus(habilidadesNode, HabilidadeTipo.racial, atacante, defensor);

            //habilidade talento
            Dictionary<IdHabilidadeTalento, HabilidadeNode> talento = atacante.getSistemaTalento().getHabilidades();
            habilidadesNode = new List<HabilidadeNode>(talento.Values);
            bonus += getDanoBonus(habilidadesNode, HabilidadeTipo.racial, atacante, defensor);

            return bonus;            
        }

        private int getDanoBonus(List<HabilidadeNode> habilidadesNode, HabilidadeTipo tipo, Jogador atacante, Mobile defensor)
        {
            int bonus = 0;
            Habilidade habilidade = null;

            foreach (HabilidadeNode node in habilidadesNode)
            {
                habilidade = Habilidade.getHabilidade(node.Id, tipo);
                bonus += habilidade.danoBonus(node, atacante, defensor);
            }

            return bonus;
        }

        /**
         * Retorna o valor do atributo que � utilizado para calcular o dano.
         * Normalmente ser� a for�a, mas existe talento que pode alterar isso.
         * 
         */ 
        public int valorAtributoBonusAtaque(Jogador jogador)
        {
            int valorAtributo = jogador.Str;

            
            
            //TODO TIAGO, mudar o talento alerta para o talento certo




            //procura pelo talento que altera o atributo para dar dano com dex
            bool dex = jogador.getSistemaTalento().possuiHabilidadeTalento(IdHabilidadeTalento.alerta);

            //se possuir, verifica se ele esta com arma pontiaguda
            if (dex)
            {
                if(WeaponType.Piercing.Equals(jogador.Weapon.GetType())) {
                    valorAtributo = jogador.Dex;
                }
            }

            return valorAtributo;
        }
    }
}
