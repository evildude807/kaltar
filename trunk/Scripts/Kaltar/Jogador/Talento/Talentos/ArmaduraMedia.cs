using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class ArmaduraMedia : Talento {
		
		private static ArmaduraMedia instance = new ArmaduraMedia();
		public static ArmaduraMedia Instance {
			get {return instance;}
		}		
		
		private ArmaduraMedia() {
			setIDTalento(IDTalento.armaduraMedia);
			Nome = "Proficiencia com armadura media";
			PreRequisitos = "Proficiencia com armadura leve";
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			if(jogador.getSistemaTalento().possuiTalento(IDTalento.armaduraLeve)) {
				return true;
			}
			return false;
		}
	}
}
