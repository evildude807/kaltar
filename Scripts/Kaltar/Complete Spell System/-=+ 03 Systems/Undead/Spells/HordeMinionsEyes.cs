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
	public class UndeadHordeMinionsEyesSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Horde Minions Eyes", "Hor De In Lor",
		                                                //SpellCircle.First,
		                                                236,
		                                                9031,
		                                                Reagent.BatWing,
		                                                Reagent.NoxCrystal
		                                               );

        public override SpellCircle Circle { get { return SpellCircle.First; } }
		public override double RequiredSkill{ get{ return 5.0; } }
		public override int RequiredMana{ get{ return 15; } }
		public override int RequiredHealth{ get{ return 0; } }
		
		public UndeadHordeMinionsEyesSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new HordeMinionsEyesSpellTarget( this );
		}
		
		private class HordeMinionsEyesSpellTarget : Target
		{
			private Spell m_Spell;
			
			public HordeMinionsEyesSpellTarget( Spell spell ) : base( 10, false, TargetFlags.None )
			{
				m_Spell = spell;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile && m_Spell.CheckSequence() )
				{
					Mobile targ = (Mobile)targeted;
					
					SpellHelper.Turn( m_Spell.Caster, targ );
					
					if ( targ.BeginAction( typeof( LightCycle ) ) )
					{
						new LightCycle.NightSightTimer( targ ).Start();
						int level = (int)Math.Abs( LightCycle.DungeonLevel * ( m_Spell.Caster.Skills[SkillName.Necromancy].Base / 100 ) );
						
						if ( level > 25 || level < 0 )
							level = 25;
						
						targ.LightLevel = level;
						
						targ.FixedParticles( 0x376A, 9, 32, 5007, EffectLayer.Waist );
						targ.PlaySound( 0x1E3 );
					}
					else
					{
						from.SendMessage( "{0} already have horde minions eyes.", from == targ ? "You" : "They" );
					}
				}
				
				m_Spell.FinishSequence();
			}
			
			protected override void OnTargetFinish( Mobile from )
			{
				m_Spell.FinishSequence();
			}
		}
	}
}
