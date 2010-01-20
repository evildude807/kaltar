using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class EscudoMedio : Talento {
		
		private static EscudoMedio instance = new EscudoMedio();
		public static EscudoMedio Instance {
			get {return instance;}
		}		
		
		private EscudoMedio() {
			setIDTalento(IDTalento.escudoMedio);
			Nome = "Proficiencia com escudo medio";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			if(jogador.getSistemaTalento().possuiTalento(IDTalento.escudoPequeno)) {
				return true;
			}
			return false;
		}
	}
}
