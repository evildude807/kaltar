/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */

using System;
using Server;
using Server.Mobiles;
using System.Collections;

namespace Kaltar.Quest
{
	/// <summary>
	/// Description of Class1.	
	/// </summary>
	public class SistemaAventura
	{
		//lista com as quest do jogador
		private Hashtable aventuras = new Hashtable();
		private Jogador jogador;
		
		public SistemaAventura(Jogador jogador){
			this.jogador = jogador;
		}
		
		public Jogador Jogador {
			get{return jogador;}
			set{jogador = value;}
		}
		
		public Hashtable Aventuras{
			get{return aventuras;}
		}
		
		public Aventura getAventura(string nome) {
			
			if(temAventura(nome)) {
				return (Aventura)aventuras[nome];
			}
			return null;
		}
		
		public void addAventura(Aventura aventura) {
				
			if(!temAventura(aventura.Nome)) {
				aventuras.Add(aventura.Nome,aventura);
			}
		}
		
		public bool temAventura(string nome) {
			return aventuras.ContainsKey(nome);
		}
		
		public void Serialize( GenericWriter writer ){
			writer.Write((int)0);			//verso
	
			//Console.WriteLine( "num aventuras: {0}", aventuras.Count);
	
			//serializa as aventura
			writer.Write(aventuras.Count);	// nmero de aventuras
			foreach ( Aventura aven in aventuras.Values ){
				Console.WriteLine( "{0}", aven.Nome);
				aven.Serialize(writer);
			}
			
		}
		
		public void Deserialize( GenericReader reader ){
			int versao = reader.ReadInt();
			
			int numAventura = reader.ReadInt();
			
			//Console.WriteLine( "num aventuras: {0}", numAventura);
			
			//recupera as aventuras
			aventuras = new Hashtable();
			for(int i = 0; i<numAventura; i++) {
				Aventura aven = new Aventura();
				aven.Deserialize(reader);
				
				aventuras.Add(aven.Nome,aven);
			}
		}
		
	}
	
	public class Aventura{
		
		//nome da aventura
		private string nome;
		//lista dos objetivos
		private Hashtable objetivos;
		//se esta concluida
		private bool concluida;
		
		public Aventura() {
		}

		public Aventura(string nome, Objetivo obj) {
			this.nome = nome;
			this.objetivos = new Hashtable();
			this.concluida = false;
			
			//adiciona o primeiro objeto. Obrigatrio ter um
			addObjetivo(obj);
		}
		
		public string Nome{
			get{return nome;}
			set{nome = value;}
		}
		
		public bool addObjetivo(Objetivo obj) {
			
			if(!temObjetivo(obj.Nome)) {
				objetivos.Add(obj.Nome,obj);
				return true;
			}
			return false;
		}
		
		public bool temObjetivo(string nome){
			return objetivos.ContainsKey(nome);		
		}
		
		public bool objConcluido(string nome) {
			Objetivo obj = (Objetivo)objetivos[nome];
			if(obj != null)
				return obj.Concluido;
			
			return false;
		}
		
		public Objetivo getObjetivo(string nome) {
			if(temObjetivo(nome)) {
				return (Objetivo)objetivos[nome];
			}
			return null;
		}
		
		public bool Concluido {
			get{return concluida;}
		}
		
		public void Serialize( GenericWriter writer ){
			writer.Write((int)0);			//verso
				
			writer.Write((string) nome);		//nome
			writer.Write((bool) concluida);	//concluida
			
			//Console.WriteLine( "num objetivos: {0}", objetivos.Count);
						
			//serializa os objetivos
			writer.Write(objetivos.Count);	// nmero de objetivos
			foreach ( Objetivo obj in objetivos.Values ){
				obj.Serialize(writer);
			}
			
		}
		
		public void Deserialize( GenericReader reader ){
			int versao = reader.ReadInt();
			
			nome = reader.ReadString();
			concluida = reader.ReadBool();
			
			int numObjetivo = reader.ReadInt();
			
			//Console.WriteLine( "num objetivos: {0}", numObjetivo);
			
			//recuperas os objectivos
			objetivos = new Hashtable();			
			for(int i = 0; i<numObjetivo; i++) {
				Objetivo obj = new Objetivo();
				obj.Deserialize(reader);
				objetivos.Add(obj.Nome,obj);
			}
		}
	}
	
	public class Objetivo {
		
		private string nome;
		private bool concluido = false;
		
		public Objetivo(string nome) {
			this.nome = nome;
			concluido = false;
		}

		public Objetivo() {
		}
		
		public bool Concluido{
			get{return concluido;}
			set{concluido = value;}
		}
		public string Nome{
			get{return nome;}
		}		
		
		public void Serialize( GenericWriter writer ){
			writer.Write((int)0);			//verso
						
			writer.Write((string) nome);		//nome
			writer.Write((bool) concluido);	//concluida
		}
		
		public void Deserialize( GenericReader reader ){
			int versao = reader.ReadInt();
					
			nome = reader.ReadString();
			concluido = reader.ReadBool();
		}
	}
}
