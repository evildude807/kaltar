using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "Globo de Luz apagado" )]
	public class GloboDeLuzFamiliar : BaseFamiliar {
		public GloboDeLuzFamiliar() {
			Name = "Globo de Luz";
			Body = 165;
			Hue = 0x901;
			BaseSoundID = 466;

			SetStr( 0 );
			SetDex( 0 );
			SetInt( 0 );

			SetHits( 1 );
			SetStam( 100 );
			SetMana( 0 );

			SetDamage( 0, 0 );
			SetDamageType( ResistanceType.Energy, 100 );
			
			ControlSlots = 1;
		}

		private DateTime m_NextFlare;

		public override void OnThink() {
			base.OnThink();

			if ( DateTime.Now < m_NextFlare )
				return;

			m_NextFlare = DateTime.Now + TimeSpan.FromSeconds( 5.0 + (25.0 * Utility.RandomDouble()) );

			this.FixedEffect( 0x37C4, 1, 12, 1109, 6 );
			this.PlaySound( 0x1D3 );

			Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ), new TimerCallback( Flare ) );
		}

		private void Flare() {
			Mobile caster = this.ControlMaster;

			if ( caster == null )
				caster = this.SummonMaster;

			if ( caster == null )
				return;

			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 5 ) ) {
				if ( m.Player && m.Alive && !m.IsDeadBondedPet && m.Karma <= 0 && m.AccessLevel < AccessLevel.Counselor )
					list.Add( m );
			}

			for ( int i = 0; i < list.Count; ++i ) {
				Mobile m = (Mobile)list[i];
				bool friendly = true;

				for ( int j = 0; friendly && j < caster.Aggressors.Count; ++j )
					friendly = ( caster.Aggressors[j].Attacker != m );

				for ( int j = 0; friendly && j < caster.Aggressed.Count; ++j )
					friendly = ( caster.Aggressed[j].Defender != m );

				if ( friendly )
				{
					m.FixedEffect( 0x37C4, 1, 12, 1109, 3 ); // At player
					m.Mana += 1 - (m.Karma / 1000);
				}
			}
		}

		public GloboDeLuzFamiliar( Serial serial ) : base( serial ) {
		}

		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
