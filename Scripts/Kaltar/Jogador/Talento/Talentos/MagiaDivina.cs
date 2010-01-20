using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {
	
	public sealed class MagiaDivina : Talento {
		
		private static MagiaDivina instance = new MagiaDivina();
		public static MagiaDivina Instance {
			get {return instance;}
		}			
		
		private MagiaDivina() {
			setIDTalento(IDTalento.magiaDivina);
			Nome = "Magia Divina";
			Descricao = "Sua fé é forte e com ela você é capaz de receber as graças dos deuses";
			PreRequisitos = "Classe Seminarista";
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return jogador.classe == classe.Seminarista;
		}
	}
}
