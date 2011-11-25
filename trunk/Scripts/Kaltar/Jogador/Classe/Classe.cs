/*
 * Autor: Tiago Augusto Data: 9/2/2005 
 * Projeto: Kaltar
 */
 
using Server;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Kaltar.Classes
{

    //Diz qual  a classe do jogador
    public enum classe {
        Aldeao,
        Escudeiro,
        Seminarista,
        Aprendiz,
        Gatuno
    }

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
        public abstract classe idClasse();
        public virtual double MaxHP { get { return 1; } }
        public virtual double MaxST { get { return 1; } }
        public virtual double MaxMA { get { return 1; } }
		public virtual bool podeUsarItem(Item item){return true;}
		public virtual bool podeUsarSkill(SkillName skill){return true;}		
		public virtual int skillCap(){return 3000;}

        /*
         * Lista das skills da classe, responsável pelo calculo dos pontos.
         */ 
        public virtual List<SkillName> skillsDaClasse()
        {
            return new List<SkillName>();
        }

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

        /**
         * Atribui o valorMaximo para o Cap de todas as skills.
         */
        protected void skillsMaximoCap(Skills skills, double valorMaximo) {
            
            Skill skill;
            for (int i = 0; i < skills.Length; i++)
            {
                skill = skills[i];
                skill.Cap = valorMaximo;
            }
        }
	}
}
