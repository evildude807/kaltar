/*
 * User: Tiago Augusto
 * Date: 5/5/2006
 * Time: 20:58
 */

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
	/// Contem métodos para tratamento das skills dos jogadores.
	/// </summary>
	public sealed class SkillUtil{
		
		private static SkillUtil instancia = new SkillUtil();
		private SkillUtil()	{}
		public static SkillUtil Instance {
			get{return instancia;}
		}
		
		//skill cap das skills de trabalho
		public static int SKILL_CAP_TRABALHO = 1500;
		
		/**
		 * Maximo de pontos de skill que o jogador por ter em suas skills de classe 
		 */
		public static int skillCap(Jogador jogador) {
            int bonus = SkillUtil.instancia.skillsCap(jogador, TipoSkill.normal);
			return jogador.getSistemaClasse().getClasse().skillCap() + bonus;
		}

        /**
         * Maximo de pontos de skill que o jogador por ter em suas skills de classe 
         */
        public static int skillCapTrabalho(Jogador jogador)
        {
            int bonus = SkillUtil.instancia.skillsCap(jogador, TipoSkill.trabalho);
            return SKILL_CAP_TRABALHO + bonus;
        }

		public static int quantoPodeSubir(Jogador jogador, Skill skill) {
			
		 	if(eSkillTrabalho(skill)) {
                return skillCapTrabalho(jogador) - totalSkillTrabalho(jogador);
		 	}
		 	else {
				return skillCap(jogador) - totalSkill(jogador);
		 	}
		}
				 
		public static bool eSkillTrabalho(Skill skill) {
			
		 	SkillName nomeSkill = skill.SkillName;
		 	
		 	//Skills de trabalho
		 	if(nomeSkill == SkillName.Alchemy 
		 	   || nomeSkill == SkillName.Blacksmith 
		 	   || nomeSkill == SkillName.Fletching 
		 	   || nomeSkill == SkillName.Carpentry 
		 	   || nomeSkill == SkillName.Cartography 
		 	   || nomeSkill == SkillName.Cooking 
		 	   || nomeSkill == SkillName.Fishing 
		 	   || nomeSkill == SkillName.Tailoring 
		 	   || nomeSkill == SkillName.Tinkering 
		 	   || nomeSkill == SkillName.Lumberjacking 
		 	   || nomeSkill == SkillName.Mining) {
		 		
		 		return true;
		 	}
		 	
		 	return false;
		}
		
		public static int totalSkillTrabalho(Jogador jogador) {
		 	int totalSkillTrabalho = 0;
		 	Skills skills = jogador.Skills;
		 	
			totalSkillTrabalho = 
				 skills.Alchemy.BaseFixedPoint
               + skills.Blacksmith.BaseFixedPoint
               + skills.Fletching.BaseFixedPoint
               + skills.Carpentry.BaseFixedPoint
               + skills.Cartography.BaseFixedPoint
               + skills.Cooking.BaseFixedPoint
               + skills.Fishing.BaseFixedPoint
               + skills.Tailoring.BaseFixedPoint
               + skills.Tinkering.BaseFixedPoint
               + skills.Lumberjacking.BaseFixedPoint
               + skills.Mining.BaseFixedPoint;
			
			return totalSkillTrabalho;
		 }

        public static void zerarSkills(Jogador jogador)
        {
            Skills skills = jogador.Skills;
            Skill skill = null;
            for (int i = 0; i < skills.Length; i++)
            {
                skill = skills[i];
                skill.Base = 0.0;
            }

        }

		public static int totalSkill(Jogador jogador) {
			//Console.WriteLine("TotalSkillUO: {0} totalSkillTrabalho: {1}",(jogador.Skills.Total/10.0),totalSkillTrabalho(jogador));
		    return (jogador.Skills.Total) - totalSkillTrabalho(jogador);
		}
         
        /**
         * Recupera o skillCap da skill informada. Procurando alguma habilidade de possa aumentar o valor.
         * 
         */ 
         public double skillCap(Jogador jogador, Skill skill)
         {
             double cap = skill.Cap;
             double bonus = 0;

             //habilidade racial
             Dictionary<IdHabilidadeRacial, HabilidadeNode> racial = jogador.getSistemaRaca().getHabilidades();
             List<HabilidadeNode> habilidadesNode = new List<HabilidadeNode>(racial.Values);
             bonus += getBonus(habilidadesNode, HabilidadeTipo.racial, skill.SkillName);

             //habilidade talento
             Dictionary<IdHabilidadeTalento, HabilidadeNode> talento = jogador.getSistemaTalento().getHabilidades();
             habilidadesNode = new List<HabilidadeNode>(talento.Values);
             bonus += getBonus(habilidadesNode, HabilidadeTipo.talento, skill.SkillName);

             return cap + bonus;
         }

         private double getBonus(List<HabilidadeNode> habilidadesNode, HabilidadeTipo tipo, SkillName skillName)
         {
             double bonus = 0;
             Habilidade habilidade = null;

             foreach (HabilidadeNode node in habilidadesNode)
             {
                 habilidade = Habilidade.getHabilidade(node.Id, tipo);
                 bonus += habilidade.skillBonus(node, skillName);
             }

             return bonus;
         }


        /**
         * Recupera o valor do skillCap total. 
         */
        public int skillsCap(Jogador jogador, TipoSkill tipoSkill)
        {
            int skillsCap = jogador.Skills.Cap;

            //habilidade racial
            Dictionary<IdHabilidadeRacial, HabilidadeNode> racial = jogador.getSistemaRaca().getHabilidades();
            List<HabilidadeNode> habilidadesNode = new List<HabilidadeNode>(racial.Values);
            int bonus = getBonus(habilidadesNode, HabilidadeTipo.racial, tipoSkill);

            //habilidade talento
            Dictionary<IdHabilidadeTalento, HabilidadeNode> talento = jogador.getSistemaTalento().getHabilidades();
            habilidadesNode = new List<HabilidadeNode>(talento.Values);
            bonus += getBonus(habilidadesNode, HabilidadeTipo.talento, tipoSkill);

            return skillsCap + bonus;
        }

        private int getBonus(List<HabilidadeNode> habilidadesNode, HabilidadeTipo tipo, TipoSkill tipoSkill)
        {
            int bonus = 0;
            Habilidade habilidade = null;

            foreach (HabilidadeNode node in habilidadesNode)
            {
                habilidade = Habilidade.getHabilidade(node.Id, tipo);

                if (TipoSkill.ambos.Equals(tipo) || TipoSkill.trabalho.Equals(tipoSkill))
                {
                    bonus += habilidade.skillsCapTrabalhoBonus(node);
                }

                if (TipoSkill.ambos.Equals(tipo) || TipoSkill.normal.Equals(tipoSkill))
                {
                    bonus += habilidade.skillsCapBonus(node);
                }
            }

            return bonus;
        }
    }

    public enum TipoSkill : int{
        trabalho, normal, ambos
    }
}
