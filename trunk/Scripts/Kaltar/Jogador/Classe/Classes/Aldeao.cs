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

		public override double MaxHP {get {return 1.5;} }
        public override double MaxST { get { return 1.5; } }
        public override double MaxMA { get { return 1.5; } }

        public override classe idClasse()
        {
            return classe.Aldeao;
        }

		public override void adicionarClasse(Jogador jogador) {
			
			//seta o ttulo com aldeao
			jogador.Title = "Aldeao";
		
			//seta hp e mana
			jogador.Hits = 40;
            jogador.Mana = 40;
            jogador.Stam = 40;
		
			//mximo de seguidor
			jogador.FollowersMax = 1;
		
			adicionarSkillCap(jogador.Skills);
		}
		
		private void adicionarSkillCap(Skills skills) {
            skillsMaximoCap(skills, 40.0);			
		}
	}
}
