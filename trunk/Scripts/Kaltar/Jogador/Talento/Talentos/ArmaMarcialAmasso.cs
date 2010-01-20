using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public class ArmaMarcialAmasso : Talento {
		
		public ArmaMarcialAmasso(IDTalento idTalento) {
			Nome = "Proficiencia com arma marcial (Amasso)";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			
			if(jogador.getSistemaTalento().possuiTalento(IDTalento.armaComumAmasso)) {
				return true;
			}
			return false;
		}
	}
}
