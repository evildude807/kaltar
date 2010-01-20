using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class EscudoGrande : Talento {
		
		private static EscudoGrande instance = new EscudoGrande();
		public static EscudoGrande Instance {
			get {return instance;}
		}		
		
		private EscudoGrande() {
			setIDTalento(IDTalento.escudoGrande);
			Nome = "Proficiencia com escudo grande";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			if(jogador.getSistemaTalento().possuiTalento(IDTalento.escudoMedio)) {
				return true;
			}
			return false;
		}
	}
}
