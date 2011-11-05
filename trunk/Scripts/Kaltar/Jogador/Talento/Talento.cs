using System;
using Server;
using Server.Mobiles;
using System.Collections;

namespace Kaltar.Talentos {
	
	public enum IDTalento : int {
		alerta,
		
		armaComumAmasso,
		armaComumDistancia,
		armaComumEspada,
		armaComumMachado,
		armaComumPontiaguda,

		armaMarcialAmasso,
		armaMarcialDistancia,
		armaMarcialEspada,
		armaMarcialMachado,
		armaMarcialPontiaguda,		
		
		armaduraLeve,
		armaduraMedia,
		armaduraPesada,
		
		escudoPequeno,
		escudoMedio,
		escudoGrande,
		
		magiaArcana,
		magiaDivina,
		
		acharArmadilha	
	}
	
	public abstract class Talento{
		
		#region atributos

		private string nome;
		private string descricao;
		private string preRequisito;
		private IDTalento id;

		#endregion

		#region Propriedades
		public string Nome {
			set {nome = value;}
			get {return nome;}
		}

		public string Descricao {
			set {descricao = value;}
			get {return descricao;}
		}		
		
		public string PreRequisitos {
			set {preRequisito = value;}
			get {return preRequisito;}
		}

		public IDTalento ID {
			get {return id;}
		}
		
		protected void setIDTalento(IDTalento idTalento) {
			id = idTalento;
		}
		#endregion
		
		//armazena todos os talentos
		private static Hashtable talentos = new Hashtable();		
		
		//todos os talentos disponíveis
		public static void Initialize()
		{
			talentos.Add(Alerta.Instance.ID, Alerta.Instance);

			talentos.Add(ArmaComumEspada.Instance.ID, ArmaComumEspada.Instance);
			talentos.Add(ArmaComumPontiaguda.Instance.ID, ArmaComumPontiaguda.Instance);
			talentos.Add(ArmaComumMachado.Instance.ID, ArmaComumMachado.Instance);
			talentos.Add(ArmaComumDistancia.Instance.ID, ArmaComumDistancia.Instance);
			talentos.Add(ArmaComumAmasso.Instance.ID, ArmaComumAmasso.Instance);
			
			talentos.Add(ArmaduraLeve.Instance.ID, ArmaduraLeve.Instance);
			talentos.Add(ArmaduraMedia.Instance.ID, ArmaduraMedia.Instance);
			talentos.Add(ArmaduraPesada.Instance.ID, ArmaduraPesada.Instance);			

			talentos.Add(EscudoPequeno.Instance.ID, EscudoPequeno.Instance);
			talentos.Add(EscudoMedio.Instance.ID, EscudoMedio.Instance);
		}		
		
		/**
		 * Retorna a classe do talento pelo seu ID
		 */
		public static Talento getTalento(IDTalento idTalento) {
			return (Talento) talentos[idTalento];
		}		
		
		/**
		 * Verifica se o Jogador possui o talento.
		 */
		public virtual bool possuiPreRequisitos (Jogador jogador) {
			return false;
		}
	}
}
