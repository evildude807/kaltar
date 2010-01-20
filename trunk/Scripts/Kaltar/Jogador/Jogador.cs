using System;
using Server;
using Server.ContextMenus;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

using Server.Gumps;
using Kaltar.Classes;
using Kaltar.Talentos;
using Kaltar.Util;
using Kaltar.propriedade;
using Kaltar.aventura;

using Server.ACC.CM;

namespace Server.Mobiles {
	
	//Diz qual  a classe do jogador
	public enum classe {
		Aldeao,
		Escudeiro,
		Seminarista,
		Aprendiz,
		Gatuno
	}	
	
	public class Jogador : PlayerMobile {
		
		#region atributos
		//marca qual a classe do jogador
		private classe e_classe;		
		//sistema de talentos
		private SistemaTalento sistemaTalento = null;
		//sistema de propriedade
		private SistemaPropriedade sistemaPropriedade = null;		
		//sistema de aventura
		private SistemaAventura sistemaAventura = null;				
		#endregion

		#region construtores
		public Jogador() {
			setClasse = classe.Aldeao;
			sistemaTalento = new SistemaTalento(this);
			sistemaPropriedade = new SistemaPropriedade(this);
			sistemaAventura = new SistemaAventura(this);
				
			//maximo de status
			StatCap = 225;			
			
			inicializarAtributos();
		}
		
		public Jogador( Serial s ) : base( s ) {
		}
		
		private void inicializarAtributos() {
			Str = 10;
			Dex = 10;
			Int = 10;
		}
		
		#endregion

		#region propriedades
		public classe classe {
			set {e_classe = value;}
			get {return e_classe;}
		}		

		//para adicionar a classe do personagem
		[CommandProperty( AccessLevel.GameMaster )]
		public classe setClasse {
			set {
				e_classe = value;
				getClasse().adicionarClasse(this);
			}
			get {return e_classe;}
		}		
	
		public SistemaTalento getSistemaTalento() {
			return sistemaTalento;
		}		
		
		public SistemaPropriedade getSistemaPropriedade() {
			return sistemaPropriedade;
		}		

		public SistemaAventura getSistemaAventura() {
			return sistemaAventura;
		}		
		
		/**
		 * Retorna a classe do personagem,  baseado no int classe para retornar a Classe mesmo.
		 */
		public Classe getClasse() {
		 	
			if (classe == classe.Aldeao) {
				return Aldeao.getInstacia();
			}
			else if (classe == classe.Escudeiro) {
				return Escudeiro.getInstacia();
			}
			else if (classe == classe.Aprendiz) {
				return Aprendiz.getInstacia();
			}
			else if (classe == classe.Seminarista) {
				return Seminarista.getInstacia();
			}			
			else if (classe == classe.Gatuno) {
				return Gatuno.getInstacia();
			}			
			else {
				Console.WriteLine("ERROR: personagem {0} sem classe definida",Name);
				setClasse = classe.Aldeao;
				return Aldeao.getInstacia();
			}
		}		
		#endregion
		
		#region serialização
		public override void Deserialize( GenericReader reader ) {

			base.Deserialize( reader );		       

			int versao = reader.ReadInt();
			
			switch(versao) {
				case 0: {
					e_classe = (classe) reader.ReadInt();
					break;
				}
			}
			
			sistemaTalento = new SistemaTalento(this);
			sistemaTalento.Deserialize(reader);
			
			sistemaPropriedade = new SistemaPropriedade(this);
			sistemaPropriedade.Deserialize(reader);			
			
			sistemaAventura = new SistemaAventura(this);
			sistemaAventura.Deserialize(reader);
		}
		
		public override void Serialize( GenericWriter writer ) {

	        base.Serialize( writer );		       

	        writer.Write((int)0);				//verso
			writer.Write((int) e_classe );		//classe
		
			sistemaTalento.Serialize(writer);
			sistemaPropriedade.Serialize(writer);
			sistemaAventura.Serialize(writer);
		}
		#endregion
		
		#region eventos
		public override int HitsMax{
		 	get{return getClasse().MaxHP;}
		}
		 
		public override int StamMax{
			get{return getClasse().MaxST;}
		}

		public override int ManaMax{
			get{return getClasse().MaxMA;}
		}
		
		public override bool AllowSkillUse( SkillName skill ) {
		 	bool podeUsarSkill = base.AllowSkillUse(skill);
		 	return getClasse().podeUsarSkill(skill);
		}

		public bool podeEquiparArmadura(BaseArmor armor) {
			
			bool pode = true;
			
			pode = (ArmaduraUtil.isRoupa(armor) || 
			        getClasse().podeEquiparArmadura(armor) || 
			        ArmaduraUtil.temTalentoParaEquipar(this, armor));
			
			if(!pode) {
				SendMessage( "Você não pode equipar essa armadura.");
			}
			
			return pode;
		}

		public bool podeEquiparArma(BaseWeapon arma) {
			
			bool pode = false;
			
			//testa as armas comuns para todos
			pode = (ArmaUtil.isItemComun(arma) || 
			        getClasse().podeEquiparArma(arma) ||
			        ArmaUtil.temTalentoParaEquipar(this, arma));

			if(!pode) {
				SendMessage( "Você não pode equipar essa arma.");
			}
			
			return pode;
		}		
		#endregion
		
		#region sobrecarga
		public override int MaxWeight { 
			get { 
				return 40 + (2 * this.Str);
			}
		}

		public override int GetMaxResistance( ResistanceType type ) {
			int max = base.GetMaxResistance( type );

			int bonus = ResistenciaUtil.bonusResistencia(this, type);
			
			return (max + bonus);
		}
			
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) {
			//alguns dos itens foram comentado no playermobile
			base.GetContextMenuEntries( from, list );
			
			if ( from == this ) {
				adicionarEntradaDeMenu((Jogador)from, list);
			}
		}
		#endregion
		
		#region Entrada do menu ao clicar no jogador
		private void adicionarEntradaDeMenu(Jogador jogador, List<ContextMenuEntry> list) {
			list.Add( new CallbackEntry( 7000, new ContextCallback( menuTalentoGump ) ) );
		}

		private void menuTalentoGump() {
			SendGump(new TalentosGump(this));
		}
		#endregion
		
		#region Sistema avançado de arquearia
		//Utilizado pelo sistema de avançado de arquearia
		private PlayerModule m_PlayerModule;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public PlayerModule PlayerModule
		{
		    get
		{
		    PlayerModule existingModule = ( PlayerModule )CentralMemory.GetModule( this.Serial, typeof( PlayerModule ) );
					
		    if ( existingModule == null )
		    {
			PlayerModule module = new PlayerModule( this.Serial );
			CentralMemory.AppendModule( this.Serial, module, true );
						
			return ( m_PlayerModule = module as PlayerModule );
		   }
		   else
		   {
		       if ( m_PlayerModule != null )
			   return m_PlayerModule;
						
		       return ( m_PlayerModule = existingModule as PlayerModule );
		   }
			}
		   }
		   #endregion
		  
	}
}
