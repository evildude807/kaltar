using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public class ArmaMarcialEspada : Talento {
		
		public ArmaMarcialEspada(IDTalento idTalento) {
			Nome = "Proficiencia com arma marcial (Espada)";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			if(jogador.getSistemaTalento().possuiTalento(IDTalento.armaComumEspada)) {
				return true;
			}
			return false;
		}
	}
}
