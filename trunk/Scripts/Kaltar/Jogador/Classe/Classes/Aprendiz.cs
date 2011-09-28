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
			maxSkill(skills.Alchemy,100.0);
			maxSkill(skills.Anatomy,20.0);
			maxSkill(skills.AnimalLore,0.0);
			maxSkill(skills.AnimalTaming,0.0);
			maxSkill(skills.Archery,20.0);
			maxSkill(skills.ArmsLore,100.0);
			maxSkill(skills.Begging,0.0);
			maxSkill(skills.Blacksmith,100.0);
			maxSkill(skills.Camping,100.0);
			maxSkill(skills.Carpentry,100.0);
			maxSkill(skills.Cartography,100.0);
			maxSkill(skills.Chivalry,0.0);
			maxSkill(skills.Cooking,100.0);
			maxSkill(skills.DetectHidden,20.0);
			maxSkill(skills.Discordance,0.0);
			maxSkill(skills.EvalInt,45.0);
			maxSkill(skills.Fencing,20.0);
			maxSkill(skills.Fishing,100.0);
			maxSkill(skills.Fletching,100.0);
			maxSkill(skills.Focus,40.0);
			maxSkill(skills.Forensics,20.0);
			maxSkill(skills.Healing,20.0);
			maxSkill(skills.Herding,0.0);
			maxSkill(skills.Hiding,0.0);
			maxSkill(skills.Highest,0.0);
			maxSkill(skills.Inscribe,45.0);
			maxSkill(skills.ItemID,45.0);
			maxSkill(skills.Lockpicking,0.0);
			maxSkill(skills.Lumberjacking,100.0);
			maxSkill(skills.Macing,20.0);
			maxSkill(skills.Magery,45.0);
			maxSkill(skills.MagicResist,45.0);
			maxSkill(skills.Meditation,45.0);
			maxSkill(skills.Mining,100.0);
			maxSkill(skills.Musicianship,0.0);
			maxSkill(skills.Necromancy,0.0);
			maxSkill(skills.Parry,20.0);
			maxSkill(skills.Peacemaking,0.0);
			maxSkill(skills.Poisoning,0.0);
			maxSkill(skills.Provocation,0.0);
			maxSkill(skills.RemoveTrap,0.0);
			maxSkill(skills.Snooping,0.0);
			maxSkill(skills.SpiritSpeak,0.0);
			maxSkill(skills.Stealing,0.0);
			maxSkill(skills.Stealth,0.0);
			maxSkill(skills.Swords,20.0);
			maxSkill(skills.Tactics,35.0);
			maxSkill(skills.Tailoring,100.0);
			maxSkill(skills.TasteID,40.0);
			maxSkill(skills.Tinkering,100.0);
			maxSkill(skills.Tracking,0.0);
			maxSkill(skills.Veterinary,0.0);
			maxSkill(skills.Wrestling,35.0);
		}		
	}
}
