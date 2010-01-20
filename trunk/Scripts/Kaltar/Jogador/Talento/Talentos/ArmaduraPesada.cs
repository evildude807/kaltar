using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class ArmaduraPesada : Talento {
		
		private static ArmaduraPesada instance = new ArmaduraPesada();
		public static ArmaduraPesada Instance {
			get {return instance;}
		}		
		
		private ArmaduraPesada() {
			setIDTalento(IDTalento.armaduraPesada);
			Nome = "Proficiencia com armadura pesada";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			if(jogador.getSistemaTalento().possuiTalento(IDTalento.armaduraMedia)) {
				return true;
			}
			return false;
		}
	}
}
