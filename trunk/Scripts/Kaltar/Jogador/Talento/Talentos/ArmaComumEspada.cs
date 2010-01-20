using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class ArmaComumEspada : Talento {
		
		private static ArmaComumEspada instance = new ArmaComumEspada();
		public static ArmaComumEspada Instance {
			get {return instance;}
		}
		
		private ArmaComumEspada() {
			setIDTalento(IDTalento.armaComumEspada);
			Nome = "Proficiencia com arma comum (Espada)";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return true;
		}
	}
}
