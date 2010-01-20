using System;
using Server.Network;
using Server;

namespace Server.Misc
{
	public class FoodDecayTimer : Timer
	{
		public static void Initialize()
		{
			new FoodDecayTimer().Start();
		}

		public FoodDecayTimer() : base( TimeSpan.FromMinutes( 5 ), TimeSpan.FromMinutes( 5 ) )
		{
			Priority = TimerPriority.OneMinute;
		}

		protected override void OnTick()
		{
			FoodDecay();			
		}

		public static void FoodDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				HungerDecay( state.Mobile );
				ThirstDecay( state.Mobile );
				
				//Katlar, adicionar para tratar fome e sede
				verificarFomeSede(state.Mobile);
			}
		}
		
		/*
		 * Kaltar
		 * 
		 * Reduz a metade dos atributos do jogador se estiver com fome ou sede.
		 * As penalidades são comulativas de sede e fome.
		 */
		private static void verificarFomeSede(Mobile m) {
			
			string estadoFome = null;
			string estadoSede = null;
			
			if(m.Hunger < -8) {
				estadoFome = "a beira da morte por fome";
			}
			else if(m.Hunger < -5) {
				estadoFome = "morrendo de fome";
			}
			else if(m.Hunger < 0) {
				estadoFome = "com muita fome";
			}
			else if(m.Hunger < 2) {
				estadoFome = "com fome";
			}

			if(m.Thirst < -8) {
				estadoSede = "a beira da morte por sede";
			}
			else if(m.Thirst < -5) {
				estadoSede = "morrendo de sede";
			}
			else if(m.Thirst < 0) {
				estadoSede = "com muita sede";
			}
			else if(m.Thirst < 2) {
				estadoSede = "com sede";
			}
			
			if(estadoFome != null || estadoSede != null) {
				m.SendMessage("Você esta {0} e {1}.", (estadoFome != null ? estadoFome : "alimentado"), (estadoSede != null ? estadoSede : "hidratado"));
				
				if(!m.Female) {
					m.SendSound(1077); //son de reclamando
				}
				else {
					m.SendSound(805);  //son de reclamando
				}
			}
			
			if(m.Thirst < -10) {
				m.Thirst = -10;
				
				if(m.AccessLevel == AccessLevel.Player) {
					m.Hits /= 2;
					m.Mana /= 2;
					m.Stam /=2;
				}
			}
			
			if(m.Hunger < -10) {
				m.Hunger = -10;
					
				if(m.AccessLevel == AccessLevel.Player) {
					m.Hits /= 2;
					m.Mana /= 2;
					m.Stam /=2;
				}
			}
		}
		
		public static void HungerDecay( Mobile m )
		{
			//kaltar	
			//if ( m != null && m.Hunger >= 1 )
			//	m.Hunger -= 1;
			
			#region kaltar
			if ( m != null && m.CheckAlive()) {
				m.Hunger -= 1;
			}
			#endregion			
		}

		public static void ThirstDecay( Mobile m )
		{
			//kaltar
			//if ( m != null && m.Thirst >= 1 )
			//	m.Thirst -= 1;
			
			#region kaltar
			if ( m != null && m.CheckAlive()) {
				m.Hunger -= 1;
			}
			#endregion						
		}
	}
}
