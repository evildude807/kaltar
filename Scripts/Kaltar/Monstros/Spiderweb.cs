using System;
using Server;
using Server.Mobiles;
using Server.Spells;
using System.Collections;

namespace Server.Items
{
	public class SpiderWeb : Item
	{
		private TimeSpan m_Duration;
		private DateTime m_Created;
		private bool m_Drying;
		private Timer m_Timer;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Drying
		{
			get
			{
				return m_Drying;
			}
			set
			{
				m_Drying = value;

				if( m_Drying )
					ItemID = 0x0EE6;
				else
					ItemID = 0x0EE4;
			}
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan Duration{ get{ return m_Duration; } set{ m_Duration = value; } }

		[Constructable]
		
		public SpiderWeb ( TimeSpan duration ) : base( 0x0EE6 )
		{
			Movable = false;
			m_Created = DateTime.Now;
			m_Duration = duration;

			m_Timer = Timer.DelayCall( TimeSpan.Zero, TimeSpan.FromSeconds( 1 ), new TimerCallback( OnTick ) );
		}

		public override void OnAfterDelete()
		{
			if( m_Timer != null )
				m_Timer.Stop();
		}

		private void OnTick()
		{
			DateTime now = DateTime.Now;
			TimeSpan age = now - m_Created;

			if( age > m_Duration )
				Delete();
			else
			{
				if( !Drying && age > (m_Duration - age) )
					Drying = true;

				ArrayList toDamage = new ArrayList();

				foreach( Mobile m in GetMobilesInRange( 0 ) )
				{
					BaseCreature bc = m as BaseCreature;

					
						
						m.LocalOverheadMessage( Server.Network.MessageType.Regular, 0x3B2, false, "Il ragno ti immobilizza con uno spruzzo di tele!" );
						m.Freeze( TimeSpan.FromSeconds( 5.0 ) );
					break;
				}

			}
		}

		public SpiderWeb( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			//Don't serialize these
		}

		public override void Deserialize( GenericReader reader )
		{
		}
	}
}
