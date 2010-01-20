using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class ArmaComumDistancia : Talento {
		
		private static ArmaComumDistancia instance = new ArmaComumDistancia();
		public static ArmaComumDistancia Instance {
			get {return instance;}
		}
		
		private ArmaComumDistancia() {
			setIDTalento(IDTalento.armaComumDistancia);
			Nome = "Proficiencia com arma comum (Distancia)";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return true;
		}
	}
}
