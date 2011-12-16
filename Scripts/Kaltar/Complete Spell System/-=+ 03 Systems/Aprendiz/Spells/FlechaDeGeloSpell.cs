using System;
using Server.Targeting;
using Server.Network;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Aprendiz {
	
	public class FlechaDeGeloSpell : AprendizSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
				"Flecha de Gelo", 
				"Flecha de Gelo",
				212,
				9041,
				Reagent.SulfurousAsh
		);

		public override SpellCircle Circle { get { return SpellCircle.First; } }
		public override double RequiredSkill{ get{ return 10.0; } }
		public override double CastDelay{ get{ return 2.0; } }
		public override int RequiredMana   { get{ return    5; } }
		
		public FlechaDeGeloSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}
		
		public override void OnCast() {
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m ) {
			if ( !Caster.CanSee( m ) ) {
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )	{
				Mobile source = Caster;

				SpellHelper.Turn( source, m );
				SpellHelper.CheckReflect( (int)this.Circle, ref source, ref m );

                //dano 2d6 + 10
                int damage = GetNewAosDamage(10, 2, 6, m);

                source.MovingParticles( m, 0x36E4, 5, 0, false, true, 0x480,0, 3006, 4006, 0, 0);
				source.PlaySound( 0x1E5 );

				SpellHelper.Damage( this, m, damage, 0, 0, 100, 0, 0 );
			}

			FinishSequence();
		}

		private class InternalTarget : Target {
			private FlechaDeGeloSpell m_Owner;

			public InternalTarget( FlechaDeGeloSpell owner ) : base(12, false, TargetFlags.Harmful ) {
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o ) {
				if ( o is Mobile ) {
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from ) {
				m_Owner.FinishSequence();
			}
		}
	}
}
