using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public class ArmaMarcialMachado : Talento {
		
		public ArmaMarcialMachado(IDTalento idTalento) {
			Nome = "Proficiencia com arma marcial (Machado)";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			if(jogador.getSistemaTalento().possuiTalento(IDTalento.armaComumMachado)) {
				return true;
			}
			return false;
		}
	}
}
