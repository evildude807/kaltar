/*
 * Autor: Tiago Augusto Data: 9/2/2005 
 * Projeto: Kaltar
 */
 
using Server;
using Server.Mobiles; 
using Server.Items;
using System;

namespace Kaltar.Classes
{
	/// <summary>
	/// Aldeo. Classe inicial de todos os jogadores.	
	/// </summary>
	public class Aldeao : Classe {
		
		//varivel nica, padro singleton
		private static Aldeao instancia = null;
		public static Aldeao getInstacia() {
			if(instancia == null) {
				instancia = new Aldeao();
			}
			return instancia;
		}
		
		public Aldeao(){
			Nome = "Aldeão";
		}

		public override int MaxHP {get {return 40;} }
		public override int MaxST {get {return 20;} }
		public override int MaxMA {get {return 20;} }

		public override void adicionarClasse(Jogador jogador) {
			
			//seta a classe com aldeao
			jogador.classe = classe.Aldeao;
			
			//seta o ttulo com aldeao
			jogador.Title = "Aldeao";
		
			//seta hp e mana
			jogador.Hits = 40;
			jogador.Mana = 20;
			jogador.Stam = 20;
		
			//mximo de seguidor
			jogador.FollowersMax = 1;
		
			adicionarSkillCap(jogador.Skills);
		}
		
		private void adicionarSkillCap(Skills skills) {
			//seta todas as skills para 35 se forem maior.
			Skill skill = null;
			for(int i=0; i < skills.Length; i++) {
				skill = skills[i];

				skill.Cap = 35.0;
				
				if(skill.Base > 35.0) {
					skill.Base = 35.0;
				}
			}			
		}
	}
}
