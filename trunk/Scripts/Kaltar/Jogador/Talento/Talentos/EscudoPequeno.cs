using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class EscudoPequeno : Talento {
		
		private static EscudoPequeno instance = new EscudoPequeno();
		public static EscudoPequeno Instance {
			get {return instance;}
		}		
		
		private EscudoPequeno() {
			setIDTalento(IDTalento.escudoPequeno);
			Nome = "Proficiencia com escudo pequeno";
			Descricao = "Não recebem as penalidade que os escudos impõem a quem os utiliza. Permite que seja utilizado.";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return true;
		}
	}
}
