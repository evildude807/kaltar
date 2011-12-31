using System;
using Server.Network;
using Server.Multis;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Regions;
using System.Collections;
using Server.Mobiles;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Cleric
{
	public class UndeadPoisonMarkSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Poison Mark", "Nox Por Ylem",
		                                                //SpellCircle.Sixth,
		                                                218,
		                                                9002,
		                                                Reagent.DaemonBlood,
		                                                Reagent.PigIron
		                                               );

        public override SpellCircle Circle { get { return SpellCircle.Sixth; } }
		public override int RequiredMana{ get{ return 30; } }
		public override double RequiredSkill{ get{ return 59.9; } }
		public override int RequiredHealth{ get{ return 0; } }
		
		public UndeadPoisonMarkSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
		
		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;
			
			return SpellHelper.CheckTravel( Caster, TravelCheckType.Mark );
		}
		
		public void Target( RecallRune rune )
		{
			if ( !Caster.CanSee( rune ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.Mark ) )
			{
			}
			else if ( SpellHelper.CheckMulti( Caster.Location, Caster.Map, !Core.AOS ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( !rune.IsChildOf( Caster.Backpack ) )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1062422 ); // You must have this rune in your backpack in order to mark it.
			}
			else if ( CheckSequence() )
			{
				rune.Mark( Caster );
				Effects.SendLocationEffect( Caster, Caster.Map, 14613, 16 );
				Caster.PlaySound( 0x1FA );
				Effects.SendLocationEffect( Caster, Caster.Map, 14626, 16 );
			}
			
			FinishSequence();
		}
		
		private class InternalTarget : Target
		{
			private UndeadPoisonMarkSpell m_Owner;
			
			public InternalTarget( UndeadPoisonMarkSpell owner ) : base( 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					m_Owner.Target( (RecallRune) o );
				}
				else
				{
					from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501797, from.Name, "" ) ); // I cannot mark that object.
				}
			}
			
			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
