/*
 * Autor: Tiago Augusto Data: 9/2/2005 
 * Projeto: Kaltar
 */
 
using Server;
using Server.Mobiles; 
using Server.Items;
using System;

using Kaltar.Talentos;

namespace Kaltar.Classes
{
	/// <summary>
	/// 
	/// </summary>
	public class Seminarista : Classe{
		
		//varivel nica, padro singleton
		private static Seminarista instancia = null;
		public static Seminarista getInstacia() {
			if(instancia == null) {
				instancia = new Seminarista();
			}
			return instancia;
		}
		
		public Seminarista(){
			Nome = "Seminarista";
		}

		public override int MaxHP {get {return 60;} }
		public override int MaxST {get {return 35;} }
		public override int MaxMA {get {return 45;} }

		public override void adicionarClasse(Jogador jogador) {
			
			//seta a classe com aldeao
			jogador.classe = classe.Seminarista;
			
			//seta hp e mana
			jogador.Hits = 60;
			jogador.Mana = 45;
			jogador.Stam = 35;			
			
			//mximo de seguidor
			jogador.FollowersMax = 1;			
			
			//seta o ttulo com aldeao
			jogador.Title = "Seminarista";
			
			adicionarSkillCap(jogador.Skills);
			
			adicionarTalentos(jogador);
		}
		
		private void adicionarTalentos(Jogador jogador) {
			SistemaTalento sistemaTalento = jogador.getSistemaTalento();
			
			sistemaTalento.aprender(IDTalento.armaComumAmasso, true);
			
			sistemaTalento.aprender(IDTalento.armaduraLeve, true);
			
			sistemaTalento.aprender(IDTalento.escudoPequeno, true);
			
			sistemaTalento.aprender(IDTalento.magiaDivina, true);
		}		
		
		private void adicionarSkillCap(Skills skills) {
			maxSkill(skills.Alchemy,100.0);
			maxSkill(skills.Anatomy,35.0);
			maxSkill(skills.AnimalLore,0.0);
			maxSkill(skills.AnimalTaming,0.0);
			maxSkill(skills.Archery,35.0);
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
			maxSkill(skills.Fencing,35.0);
			maxSkill(skills.Fishing,100.0);
			maxSkill(skills.Fletching,100.0);
			maxSkill(skills.Focus,40.0);
			maxSkill(skills.Forensics,40.0);
			maxSkill(skills.Healing,45.0);
			maxSkill(skills.Herding,0.0);
			maxSkill(skills.Hiding,0.0);
			maxSkill(skills.Highest,0.0);
			maxSkill(skills.Inscribe,45.0);
			maxSkill(skills.ItemID,45.0);
			maxSkill(skills.Lockpicking,0.0);
			maxSkill(skills.Lumberjacking,100.0);
			maxSkill(skills.Macing,35.0);
			maxSkill(skills.Magery,0.0);
			maxSkill(skills.MagicResist,45.0);
			maxSkill(skills.Meditation,45.0);
			maxSkill(skills.Mining,100.0);
			maxSkill(skills.Musicianship,0.0);
			maxSkill(skills.Necromancy,0.0);
			maxSkill(skills.Parry,35.0);
			maxSkill(skills.Peacemaking,0.0);
			maxSkill(skills.Poisoning,0.0);
			maxSkill(skills.Provocation,0.0);
			maxSkill(skills.RemoveTrap,0.0);
			maxSkill(skills.Snooping,0.0);
			maxSkill(skills.SpiritSpeak,45.0);
			maxSkill(skills.Stealing,0.0);
			maxSkill(skills.Stealth,0.0);
			maxSkill(skills.Swords,35.0);
			maxSkill(skills.Tactics,35.0);
			maxSkill(skills.Tailoring,100.0);
			maxSkill(skills.TasteID,100.0);
			maxSkill(skills.Tinkering,100.0);
			maxSkill(skills.Tracking,0.0);
			maxSkill(skills.Veterinary,0.0);
			maxSkill(skills.Wrestling,35.0);
		}		
	}
}
