/*
 * Autor: Tiago Augusto Data: 9/2/2005 
 * Projeto: Kaltar
 */
 
using Server;
using Server.Mobiles; 
using Server.Items;
using System;
using System.Collections;
using System.Collections.Generic;

using Kaltar.Talentos;

namespace Kaltar.Classes
{
	/// <summary>
	/// 
	/// </summary>
	public class Aprendiz : Classe{
		
		//varivel nica, padro singleton
		private static Aprendiz instancia = null;
		public static Aprendiz getInstacia() {
			if(instancia == null) {
				instancia = new Aprendiz();
			}
			return instancia;
		}
		
		public Aprendiz(){
			Nome = "Aprendiz";
		}

        public override double MaxHP { get { return 1; } }
        public override double MaxST { get { return 1; } }
        public override double MaxMA { get { return 2; } }

        public override classe idClasse()
        {
            return classe.Aprendiz;
        }

		public override void adicionarClasse(Jogador jogador) {
			
			//seta hp e mana
            jogador.Hits = 40;
            jogador.Mana = 40;
            jogador.Stam = 40;
			
			//mximo de seguidor
			jogador.FollowersMax = 1;			
			
			//seta o ttulo com aldeao
			jogador.Title = "Aprendiz";
			
			adicionarSkillCap(jogador.Skills);
			
			adicionarTalentos(jogador);
		}
		
		private void adicionarTalentos(Jogador jogador) {
			SistemaTalento sistemaTalento = jogador.getSistemaTalento();
			
            /*
			sistemaTalento.aprender(IDTalento.magiaArcana, true);
             */
		}

        public override List<SkillName> skillsDaClasse()
        {
            List<SkillName> skills = new List<SkillName>();

            skills.Add(SkillName.Swords);
            skills.Add(SkillName.Macing);
            skills.Add(SkillName.Fencing);
            skills.Add(SkillName.TasteID);
            skills.Add(SkillName.Archery);
            skills.Add(SkillName.Wrestling);

            //aprendiz
            skills.Add(SkillName.Magery);
            skills.Add(SkillName.Necromancy);
            skills.Add(SkillName.MagicResist);
            skills.Add(SkillName.Meditation);
            skills.Add(SkillName.Alchemy);

            return skills;
        }

		private void adicionarSkillCap(Skills skills) {

            skillsMaximoCap(skills, 0.0);

            //colcoar o cap das skills de trabalho para 100

			maxSkill(skills.Alchemy,100.0);
		}		
	}
}
