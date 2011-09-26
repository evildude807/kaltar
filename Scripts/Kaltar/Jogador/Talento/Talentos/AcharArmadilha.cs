using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;

namespace Kaltar.Talentos {
	
	public sealed class AcharArmadilha : Talento {
		
		private static AcharArmadilha instance = new AcharArmadilha();
		public static AcharArmadilha Instance {
			get {return instance;}
		}			
		
		private AcharArmadilha() {
			setIDTalento(IDTalento.acharArmadilha);
			Nome = "Achar Armadilha";
			Descricao = "Seu faro ao perigo é tanto que consegue descobrir armadilhas escondidas";
			PreRequisitos = "Classe Gatuno";
		}
		
		public override bool possuiPreRequisitos (Jogador jogador){
			return jogador.getSistemaClasse().getClasse().idClasse() == classe.Gatuno;
		}
	}
}
