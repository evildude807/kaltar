/*
 * Autor: Tiago Augusto Data: 9/2/2005 
 * Projeto: Kaltar
 */
 
using Server;
using Server.Items;
using Server.Mobiles;
using System;

namespace Kaltar.Classes
{
	/// <summary>
	/// Classe pai de todas as classes do jogo.
	/// Contem mtodos que deve sem implementados por todos as subclasses.
	/// </summary>
	public abstract class Classe {
		
		private string nome;	//nome da classe
		
		public Classe(){
			Nome = "sem classe";
		}

		public virtual bool podeEquiparArma(BaseWeapon arma) {
			return false;
		}
		public virtual bool podeEquiparArmadura(BaseArmor armadura){
			return false;
		}
		public abstract void adicionarClasse(Jogador jogador);
		public virtual int MaxHP {get {return 1;} }
		public virtual int MaxST {get {return 1;} }
		public virtual int MaxMA {get {return 1;} }
		public virtual bool podeUsarItem(Item item){return true;}
		public virtual bool podeUsarSkill(SkillName skill){return true;}		
		public virtual int skillCap(){return 300;}
		
		/**
		 * atribui o máximo, Cap da skill para o valor informado.
		 * e se a skill tiver Base maior que valor, abaixa para valor.
		 */
		public void maxSkill(Skill skill, double valor) {
			skill.Cap = valor;
			if(skill.Base > valor)
				skill.Base = valor;			
		}
	
		//getter setter --------------------------------------------------------------------
		public string Nome {
			get {return nome;}
			set {nome = value;}
		}
	}
}
