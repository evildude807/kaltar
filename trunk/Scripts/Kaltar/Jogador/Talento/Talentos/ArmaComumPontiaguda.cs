using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class ArmaComumPontiaguda : Talento {
		
		private static ArmaComumPontiaguda instance = new ArmaComumPontiaguda();
		public static ArmaComumPontiaguda Instance {
			get {return instance;}
		}		
		
		private ArmaComumPontiaguda() {
			setIDTalento(IDTalento.armaComumPontiaguda);
			Nome = "Proficiencia com arma comum (Pontiaguda)";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return true;
		}
	}
}
