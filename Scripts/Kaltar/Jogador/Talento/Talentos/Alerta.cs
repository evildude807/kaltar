using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class Alerta : Talento {
		
		private static Alerta instance = new Alerta();
		public static Alerta Instance {
			get {return instance;}
		}		
		
		private Alerta() {
			setIDTalento(IDTalento.alerta);
			Nome = "Alerta";
			Descricao = "O seu estado de alerta é aprimorado o que lhe permite seila oque.";
			PreRequisitos = null;
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return true;
		}
	}
}
