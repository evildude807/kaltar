using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class MagiaArcana : Talento {
		
		private static MagiaArcana instance = new MagiaArcana();
		public static MagiaArcana Instance {
			get {return instance;}
		}			
		
		private MagiaArcana() {
			setIDTalento(IDTalento.magiaArcana);
			Nome = "Magia Arcana";
			Descricao = "Aprendeu a conjurar as energias arcanas a seu favor";
			PreRequisitos = "Classe Aprendiz";
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return jogador.classe == classe.Aprendiz;
		}
	}
}
