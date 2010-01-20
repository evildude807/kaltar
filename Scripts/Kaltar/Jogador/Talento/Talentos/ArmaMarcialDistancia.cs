using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public class ArmaMarcialDistancia : Talento {
		
		public ArmaMarcialDistancia(IDTalento idTalento) {
			Nome = "Proficiencia com arma marcial (Distancia)";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			if(jogador.getSistemaTalento().possuiTalento(IDTalento.armaComumDistancia)) {
				return true;
			}
			return false;
		}
	}
}
