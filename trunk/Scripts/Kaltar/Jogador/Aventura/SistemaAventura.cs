/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */
 
using System;
using Server;
using Server.Mobiles;
using System.Collections;

namespace Kaltar.aventura {
	
	public enum StatusAventura : int {
		iniciada = 0,
		finalizada = -1,
	}
	
	/// <summary>
	/// Description of Sistemaaventuras.
	/// </summary>
	public class SistemaAventura	{
		
		#region atributos
		//jogador dono dos aventuras
		private Jogador jogador = null;

		 /**
		 * Armazena todos os aventuras do jogadore
		 * 
	 	 * Chave = (fgdint) IDAventura identificador da aventura.
	  	 * Valor = (int) passo no qual o jogador esta para aquela aventura.
	  	 * Ex.: 1,0 -> para a aventura 1, o jogador apenas iniciou.
	 	 * Ex.: 1,1 -> para a aventura 1, o jogador já fez o primeiro objetivo.
	 	 * Ex.: 1,2 -> para a aventura 1, o jogador já fez o primeiro e segundo objetivo.
	 	 * Ex.: 1,-1 -> para a aventura 1, o jogador já completou a aventura.
	 	 *
	 	 */		
		private Hashtable aventuras = new Hashtable();
		#endregion
		
		#region construtores
		public SistemaAventura(Jogador jogador){
			this.jogador = jogador;
		}		
		#endregion
		
		#region aventurass
		public Hashtable Aventuras {
			get{return aventuras;}
		}
		#endregion
		
		#region serialização
		public void Serialize( GenericWriter writer ){
			writer.Write((int)0);			//verso		
			
			//Console.WriteLine( "num aventuras: {0}", aventuras.Count);
		
			//serializa os objetivos
			writer.Write(aventuras.Count);	// número de objetivos
			foreach ( IDAventura chave in aventuras.Keys ){
				int valor = (int)aventuras[chave];
				writer.Write((int)chave);
				writer.Write(valor);				
			}
		}
		
		public void Deserialize( GenericReader reader ){
			int versao = reader.ReadInt();

			int numPropriedades = reader.ReadInt();
			
			//Console.WriteLine( "num aventuras: {0}", numaventuras);
			
			//recuperas os objectivos
			aventuras = new Hashtable();			
			for(int i = 0; i < numPropriedades; i++) {
				IDAventura chave = (IDAventura)reader.ReadInt();
				int valor = (int)reader.ReadInt();
				aventuras.Add(chave, valor);
			}
		}
		#endregion
		
		#region métodos
		
		/**
		 * Verifica se o jogador já completou a aventura.
		 * True para sim, false caso contrário.
		 */
		public bool completouAventura(IDAventura idAventura) {
			
			if(pegouAventura(idAventura)) {
				int andamento = (int)aventuras[idAventura];
				return andamento == (int)StatusAventura.finalizada;
			}
			
			return false;
		}
		 
		/**
		 * Verifica se o jogador já pegou a aventura para realizar.
		 * True se sim, false caso contrário.
		 */
		public bool pegouAventura(IDAventura idAventura) {
			return aventuras.ContainsKey(idAventura);
		}
		 
		/**
		 * Verifica se o jogador já completou o passo para a aventura.
		 * True se já completou, false caso contrário.
		 */
		public bool completouPasso(IDAventura idAventura, int passo) {
			
			if(pegouAventura(idAventura)) {
				int andamento = (int)aventuras[idAventura];
				return andamento >= passo;
			}

			return false;
		}
		
		/**
		 * Retorna o passo no qual o jogador esta na aventura.
		 * Se a aventura estiver finalizada, -1 será retornado.
		 * Se não tiver iniciado a aventura, -2 será retornado.
		 */
		public int passoCorrente(IDAventura idAventura) {
			if(pegouAventura(idAventura)) {
				return (int)aventuras[idAventura];
			}

			return -2;
		}
		
		/**
		 * Inicia a aventura caso a mesma não exista para o jogador.
		 * A aventura será adiciona ao Map com valor 0. 
		 */
		public void iniciarAventura(IDAventura idAventura) {
			if(!pegouAventura(idAventura)) {
				aventuras.Add(idAventura, (int)StatusAventura.iniciada);
			}
		}
		 
		/**
		 * Finaliza a aventura caso a mesma exista para o jogador.
		 * A aventura será atualizada para o valor -1. 
		 */
		public void finalizarAventura(IDAventura idAventura) {
			if(pegouAventura(idAventura)){
				aventuras.Remove(idAventura);
				aventuras.Add(idAventura, (int)StatusAventura.finalizada);
			}			
		}
		 
		/**
		 * Avança o objetivo da aventura, caso a mesma já exista.
		 * o valor para o objetivo da aventura é acrescido em 1
		 */
		public void finalizarObjetivo(IDAventura idAventura) {
			if(pegouAventura(idAventura)) {
				int andamento = (int)aventuras[idAventura];
				aventuras.Remove(idAventura);
				aventuras.Add(idAventura, ++andamento);
			}
		}
		
		#endregion
	}
}
