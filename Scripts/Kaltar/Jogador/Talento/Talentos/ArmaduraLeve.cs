using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class ArmaduraLeve : Talento {
		
		private static ArmaduraLeve instance = new ArmaduraLeve();
		public static ArmaduraLeve Instance {
			get {return instance;}
		}
		
		private ArmaduraLeve() {
			setIDTalento(IDTalento.armaduraLeve);
			Nome = "Proficiencia com armadura leve";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return true;
		}
	}
}
