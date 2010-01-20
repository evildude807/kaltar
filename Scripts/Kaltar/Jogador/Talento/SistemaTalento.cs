/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */
 
using System;
using Server;
using Server.Mobiles;
using System.Collections;

namespace Kaltar.Talentos
{
	/// <summary>
	/// Description of SistemaTalento.
	/// </summary>
	public class SistemaTalento	{
		
		#region atributos
		//jogador dono dos talentos
		private Jogador jogador = null;
		//armazena todos os talentos do jogadore
		private Hashtable talentos = new Hashtable();
		//pontos gastos de talentos
		private int pontosGastos = 0;
		#endregion
			
		public SistemaTalento(Jogador jogador){
			this.jogador = jogador;
		}		
		
		#region propriedades
		public Hashtable Talentos {
			get{return talentos;}
		}
		
		public int PontosGastos {
			get{return pontosGastos;}
		}
		#endregion
		
		#region serialização
		public void Serialize( GenericWriter writer ){
			writer.Write((int)0);			//verso		
			
			//Console.WriteLine( "num talentos: {0}", talentos.Count);
			
			writer.Write((int)pontosGastos);
			
			//serializa os objetivos
			writer.Write(talentos.Count);	// nmero de objetivos
			foreach ( IDTalento idTalento in talentos.Values ){
				writer.Write((int)idTalento);
			}
		}
		
		public void Deserialize( GenericReader reader ){
			int versao = reader.ReadInt();
			
			pontosGastos = reader.ReadInt();
			
			int numTalentos = reader.ReadInt();
			
			//Console.WriteLine( "num talentos: {0}", numTalentos);
			
			//recuperas os objectivos
			talentos = new Hashtable();			
			for(int i = 0; i<numTalentos; i++) {
				IDTalento idTalento = (IDTalento)reader.ReadInt();
				talentos.Add(idTalento, idTalento);
			}
		}
		#endregion
		
		/**
		 * Pontos disponíveis para aprender talentos.
		 */
		public int pontosDisponiveis() {

			int meses = (int)(DateTime.Now - jogador.CreationTime).TotalDays/30;
			int horasJogadas = jogador.GameTime.Hours;
			
			int minino = meses < (horasJogadas/30) ? meses : (horasJogadas/30);
			
		 	return minino - pontosGastos + 1;
		}
		
		/*
		 * Adiciona o talento ao jogador.
		 * É verificado se o jogador possui os pré-requisitos
		 * e já não possua o talento.
		 * 
		 * @mudancaClasse true quando for mudança de classe,  para não verificar pontos de talento.
		 */ 
		public bool aprender (IDTalento idTalento, bool mudancaClasse) {
		 	Talento talento = Talento.getTalento(idTalento);
		 	
		 	if(talento == null) {
		 		jogador.SendMessage("Talento não encontrado, informe os administradores.");
		 		return false;	
		 	}
		 	
		 	if(!mudancaClasse && pontosDisponiveis() < 1) {
		 		jogador.SendMessage("Você não possui pontos de talento disponíveis.");
		 		return false;
		 	}
		 	
			if (!talento.possuiPreRequisitos (jogador)) {
		 		jogador.SendMessage("Você não possui os pré-requisitos para aprender o talento.");		 		
				return false;
			}
				
			if (possuiTalento(talento.ID)) {
				jogador.SendMessage("Você já possui o talento.");
				return false;
			}
		 	
			adicionarTalento(talento);
			
			if(!mudancaClasse) {
				pontosGastos++;
			}

			return true;		 	
		}
		 
		/*
		 * Adiciona o talento ao jogador.
		 * É verificado se o jogador possui os pré-requisitos
		 * e já não possua o talento.
		 */ 
		public bool aprender (IDTalento idTalento) {
		 	return aprender(idTalento, false);
		}
		
		private void adicionarTalento (Talento talento) {
			jogador.SendMessage("Você acaba de aprender o talento {0}", talento.Nome);
			talentos.Add(talento.ID, talento.ID);
		}
		
		 /**
		  * Verifica se o jogador já possui o talento
		  */ 
		public bool possuiTalento (IDTalento idTalento) {
			
			if(talentos[idTalento] != null) {
				return true;
			}
			return false;
		}
	}
}
