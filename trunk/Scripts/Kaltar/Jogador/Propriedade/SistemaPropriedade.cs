/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */
 
using System;
using Server;
using Server.Mobiles;
using System.Collections;

namespace Kaltar.propriedade
{
	/// <summary>
	/// Description of Sistemapropriedades.
	/// </summary>
	public class SistemaPropriedade	{
		
		#region atributos
		//jogador dono dos propriedades
		private Jogador jogador = null;
		//armazena todos os propriedades do jogadore
		private Hashtable propriedades = new Hashtable();
		#endregion
		
		#region construtores
		public SistemaPropriedade(Jogador jogador){
			this.jogador = jogador;
		}		
		#endregion
		
		#region propriedadess
		public Hashtable Propriedades {
			get{return propriedades;}
		}
		#endregion
		
		#region serialização
		public void Serialize( GenericWriter writer ){
			writer.Write((int)0);			//versao		
			
			//Console.WriteLine( "num propriedades: {0}", propriedades.Count);
		
			//serializa os objetivos
			writer.Write(propriedades.Count);	// número de objetos
			foreach ( string chave in propriedades.Keys ){
				string valor = (string)propriedades[chave];
				writer.Write(chave);
				writer.Write(valor);				
			}
		}
		
		public void Deserialize( GenericReader reader ){
			int versao = reader.ReadInt();

			int numPropriedades = reader.ReadInt();
			
			//Console.WriteLine( "num propriedades: {0}", numpropriedades);
			
			//recuperas os objectivos
			propriedades = new Hashtable();			
			for(int i = 0; i < numPropriedades; i++) {
				string chave = reader.ReadString();
				string valor = reader.ReadString();
				propriedades.Add(chave, valor);
			}
		}
		#endregion
		
		#region métodos
		public string getPropriedade(string chave) {
			return (string)propriedades[chave];
		}
		
		public void setPropriedade(string chave, string valor) {
			propriedades.Add(chave, valor);
		}		
		#endregion
	}
}
