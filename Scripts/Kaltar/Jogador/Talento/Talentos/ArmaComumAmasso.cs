using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class ArmaComumAmasso : Talento {
		
		private static ArmaComumAmasso instance = new ArmaComumAmasso();
		public static ArmaComumAmasso Instance {
			get {return instance;}
		}			
		
		private ArmaComumAmasso() {
			setIDTalento(IDTalento.armaComumAmasso);
			Nome = "Proficiencia com arma comum (Amasso)";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return true;
		}
	}
}
