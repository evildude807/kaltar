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
	public class Escudeiro : Classe{
		
		//varivel nica, padro singleton
		private static Escudeiro instancia = null;
		public static Escudeiro getInstacia() {
			if(instancia == null) {
				instancia = new Escudeiro();
			}
			return instancia;
		}
		
		public Escudeiro(){
			Nome = "Homem de arma";
		}

		public override int MaxHP {get {return 80;} }
		public override int MaxST {get {return 40;} }
		public override int MaxMA {get {return 30;} }

		public override void adicionarClasse(Jogador jogador) {
			
			//seta a classe com aldeao
			jogador.classe = classe.Escudeiro;
			
			//seta hp e mana
			jogador.Hits = 80;
			jogador.Mana = 30;
			jogador.Stam = 40;

			//mximo de seguidor
			jogador.FollowersMax = 1;
		
			//seta o ttulo com aldeao
			jogador.Title = "Escudeiro";
						
			adicionarSkillCap(jogador.Skills);
			
			adicionarTalentos(jogador);
		}
		
		private void adicionarTalentos(Jogador jogador) {
			SistemaTalento sistemaTalento = jogador.getSistemaTalento();
			
			sistemaTalento.aprender(IDTalento.armaComumAmasso, true);
			sistemaTalento.aprender(IDTalento.armaComumEspada, true);
			sistemaTalento.aprender(IDTalento.armaComumMachado, true);
			sistemaTalento.aprender(IDTalento.armaComumPontiaguda, true);
			
			sistemaTalento.aprender(IDTalento.armaduraLeve, true);
			sistemaTalento.aprender(IDTalento.armaduraMedia, true);
			
			sistemaTalento.aprender(IDTalento.escudoPequeno, true);
			sistemaTalento.aprender(IDTalento.escudoMedio, true);
		}
		
		private void adicionarSkillCap(Skills skills) {
			maxSkill(skills.Alchemy,100.0);
			maxSkill(skills.Anatomy,45.0);
			maxSkill(skills.AnimalLore,0.0);
			maxSkill(skills.AnimalTaming,0.0);
			maxSkill(skills.Archery,45.0);
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
			maxSkill(skills.EvalInt,0.0);
			maxSkill(skills.Fencing,45.0);
			maxSkill(skills.Fishing,100.0);
			maxSkill(skills.Fletching,100.0);
			maxSkill(skills.Focus,40.0);
			maxSkill(skills.Forensics,20.0);
			maxSkill(skills.Healing,20.0);
			maxSkill(skills.Herding,0.0);
			maxSkill(skills.Hiding,0.0);
			maxSkill(skills.Highest,0.0);
			maxSkill(skills.Inscribe,0.0);
			maxSkill(skills.ItemID,0.0);
			maxSkill(skills.Lockpicking,0.0);
			maxSkill(skills.Lumberjacking,100.0);
			maxSkill(skills.Macing,45.0);
			maxSkill(skills.Magery,0.0);
			maxSkill(skills.MagicResist,20.0);
			maxSkill(skills.Meditation,0.0);
			maxSkill(skills.Mining,100.0);
			maxSkill(skills.Musicianship,0.0);
			maxSkill(skills.Necromancy,0.0);
			maxSkill(skills.Parry,45.0);
			maxSkill(skills.Peacemaking,0.0);
			maxSkill(skills.Poisoning,0.0);
			maxSkill(skills.Provocation,0.0);
			maxSkill(skills.RemoveTrap,0.0);
			maxSkill(skills.Snooping,0.0);
			maxSkill(skills.SpiritSpeak,0.0);
			maxSkill(skills.Stealing,0.0);
			maxSkill(skills.Stealth,0.0);
			maxSkill(skills.Swords,45.0);
			maxSkill(skills.Tactics,45.0);
			maxSkill(skills.Tailoring,100.0);
			maxSkill(skills.TasteID,0.0);
			maxSkill(skills.Tinkering,100.0);
			maxSkill(skills.Tracking,0.0);
			maxSkill(skills.Veterinary,0.0);
			maxSkill(skills.Wrestling,45.0);
		}
	}
}
