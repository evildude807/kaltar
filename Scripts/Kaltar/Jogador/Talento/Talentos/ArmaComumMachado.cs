using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class ArmaComumMachado : Talento {

		private static ArmaComumMachado instance = new ArmaComumMachado();
		public static ArmaComumMachado Instance {
			get {return instance;}
		}		
		
		private ArmaComumMachado() {
			setIDTalento(IDTalento.armaComumMachado);
			Nome = "Proficiencia com arma comum (Machado)";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return true;
		}
	}
}
