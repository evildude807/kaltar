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
using Kaltar.Morte;
using Kaltar.Modulo;
using Kaltar.Raca;

using Server.ACC.CM;

namespace Server.Mobiles {	
	
	public class Jogador : PlayerMobile {
		
		#region atributos

        //sistema de classes
        private SistemaClasse sistemaClasse = null;
		//sistema de talentos
		private SistemaTalento sistemaTalento = null;
		//sistema de propriedade
		private SistemaPropriedade sistemaPropriedade = null;		
		//sistema de aventura
		private SistemaAventura sistemaAventura = null;		
		//sistema de morte
        private SistemaMorte sistemaMorte = null;
        //sistema de raca
        private SistemaRaca sistemaRaca = null;
		#endregion

		#region construtores
		public Jogador() {
            sistemaClasse = new SistemaClasse(this);
			sistemaTalento = new SistemaTalento(this);
			sistemaPropriedade = new SistemaPropriedade(this);
			sistemaAventura = new SistemaAventura(this);
            sistemaMorte = new SistemaMorte(this);
            sistemaRaca = new SistemaRaca(this);

            RegistroModule.registrarModuleJogador(this);
            
            setClasse = classe.Aldeao;

			//maximo de status
            StatCap = getSistemaRaca().StatusCap;			
			
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
        
        //para adicionar a classe do personagem
		[CommandProperty( AccessLevel.GameMaster )]
		public classe setClasse {
			set {
                getSistemaClasse().adicionarClasse(value);
			}
            get { return getSistemaClasse().getClasse().idClasse(); }
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
        
        public SistemaClasse getSistemaClasse() {
            if (sistemaClasse == null)
            {
                Console.WriteLine("Sistema classe null");
            }
            return sistemaClasse;
        }

        public SistemaMorte getSistemaMorte()
        {
            return sistemaMorte;
        }

        public SistemaRaca getSistemaRaca()
        {
            return sistemaRaca;
        }

		#endregion
		
		#region serialização
		public override void Deserialize( GenericReader reader ) {

            RegistroModule.registrarModuleJogador(this);

            sistemaTalento = new SistemaTalento(this);
            sistemaClasse = new SistemaClasse(this);
            sistemaPropriedade = new SistemaPropriedade(this);
            sistemaAventura = new SistemaAventura(this);
            sistemaMorte = new SistemaMorte(this);
            sistemaRaca = new SistemaRaca(this);

            // a inicializacao dos sistemas devem ficar antes deste método. Pois ele invoca métodos como max hits quqe utiliza os sistemas.
			base.Deserialize( reader );

			int versao = reader.ReadInt();

		}
		
		public override void Serialize( GenericWriter writer ) {

	        base.Serialize( writer );		       

	        writer.Write((int)0);				//verso
		}

		#endregion
		
		#region eventos

        public override int HitsMax{
		 	get{
                int bonus = AtributoUtil.Instance.vidaBonus(this);
                return (int)((Str / 2 * getSistemaClasse().getClasse().MaxHP) + 50) + bonus;
            }
		}
		 
		public override int StamMax{
            get {
                int bonus = AtributoUtil.Instance.folegoBonus(this);
                return (int)((Dex / 2 * getSistemaClasse().getClasse().MaxST) + 20) + bonus; 
            }
		}

		public override int ManaMax{
            get {
                int bonus = AtributoUtil.Instance.manaBonus(this);
                return (int)((Int / 2 * getSistemaClasse().getClasse().MaxMA) + 20) + bonus; 
            }
		}
		
		public override bool AllowSkillUse( SkillName skill ) {
		 	bool podeUsarSkill = base.AllowSkillUse(skill);
		 	return getSistemaClasse().getClasse().podeUsarSkill(skill);
		}

		public bool podeEquiparArmadura(BaseArmor armor) {
			
			bool pode = true;
			
			pode = (ArmaduraUtil.isRoupa(armor) ||
                    getSistemaClasse().getClasse().podeEquiparArmadura(armor) || 
			        ArmaduraUtil.temTalentoParaEquipar(this, armor));
			
			if(!pode) {
				SendMessage( "Você não pode equipar essa armadura.");
			}
			
            //foi alterado para testes
			return true;
		}

		public bool podeEquiparArma(BaseWeapon arma) {
			
			bool pode = false;
			
			//testa as armas comuns para todos
			pode = (ArmaUtil.isItemComun(arma) ||
                    getSistemaClasse().getClasse().podeEquiparArma(arma) ||
			        ArmaUtil.temTalentoParaEquipar(this, arma));

			if(!pode) {
				SendMessage( "Você não pode equipar essa arma.");
			}

            //foi alterado para testes
			return true;
		}		

		#endregion
		
		#region sobrecarga

		public override int pesoMaximo() { 
            int bonus = AtributoUtil.Instance.cargaBonus(this);

            //Console.WriteLine("carga bonus: {0}", bonus);

			return 40 + this.Str + bonus;
		}

        public override int GetMinResistance(ResistanceType type)
        {
            int min = base.GetMinResistance(type);

            int bonus = ResistenciaUtil.Instance.bonusResistencia(this, type);

            //Console.WriteLine("resistencia min: {0} e {1}", min, bonus);

            return (min + bonus);
        }

		public override int GetMaxResistance( ResistanceType type ) {
			int max = base.GetMaxResistance( type );

            int bonus = ResistenciaUtil.Instance.bonusResistencia(this, type);

            //Console.WriteLine("resistencia max: {0} e {1}", max, bonus);

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
			SendGump(new HabilidadeTalentoGump(this));
		}
		#endregion
		
		#region Sistema avançado de arquearia
		//Utilizado pelo sistema de avançado de arquearia
		/*
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
		   */
		   #endregion
		  
	}
}
