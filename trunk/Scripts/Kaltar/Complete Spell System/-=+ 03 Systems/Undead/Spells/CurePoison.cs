using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Items;

namespace Server.ACC.CSS.Systems.Cleric
{
	public class UndeadCurePoisonSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Cure", "Un Gra Nox",
		                                                //SpellCircle.Second,
		                                                212,
		                                                9061,
		                                                Reagent.Garlic,
		                                                Reagent.Ginseng
		                                               );

        public override SpellCircle Circle { get { return SpellCircle.Second; } }
		public override int RequiredMana{ get{ return 20; } }
		public override double RequiredSkill{ get{ return 15.0; } }
		public override int RequiredHealth{ get{ return 0; } }
		
		public UndeadCurePoisonSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
		
		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );
				
				Poison p = m.Poison;
				
				if ( p != null )
				{
					int chanceToCure = 10000 + (int)(Caster.Skills[SkillName.Necromancy].Value * 75) - ((p.Level + 1) * 1750);
					chanceToCure /= 100;
					
					if ( chanceToCure > Utility.Random( 100 ) )
					{
						if ( m.CurePoison( Caster ) )
						{
							if ( Caster != m )
								Caster.SendLocalizedMessage( 1010058 ); // You have cured the target of all poisons!
							
							m.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
						}
					}
					else
					{
						m.SendLocalizedMessage( 1010060 ); // You have failed to cure your target!
					}
				}
				
				m.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
				m.PlaySound( 0x1E0 );
			}
			
			FinishSequence();
		}
		
		public class InternalTarget : Target
		{
			private UndeadCurePoisonSpell m_Owner;
			
			public InternalTarget( UndeadCurePoisonSpell owner ) : base( 12, false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}
			
			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
