using System;
using Server.Targeting;
using Server.Network;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Aprendiz {
	
	public class MaosFlamejantesSpell : AprendizSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
				"Mãos Flamejantes", 
				"Mãos Flamejantes",
				212,
				9041,
				Reagent.SulfurousAsh
		);

		public override SpellCircle Circle { get { return SpellCircle.First; } }
		public override double RequiredSkill{ get{ return 15.0; } }
		public override double CastDelay{ get{ return 1.0; } }
		public override int RequiredMana   { get{ return 10; } }
		
		public MaosFlamejantesSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}
		
		public override void OnCast() {
		
			if(CheckSequence()) {
				double damageReal = Utility.Random( 10, 20 );						
				
				IPooledEnumerable mobiles = Caster.GetMobilesInRange(4);
				foreach (Mobile mobile in mobiles) {
				
					
					double damage = 0;
					if(Caster.CanSee( mobile) && Caster != mobile && Caster.CanBeHarmful(mobile) && mobile.Alive) {
						
						Caster.DoHarmful( mobile );
						
						/*
						damage = damageReal * GetDamageScalar( mobile );
						SpellHelper.Damage( this, mobile, damage);
						
						Caster.MovingParticles( mobile, 0x36FE, 5, 10, false, true, 3006, 4006, 0 );
						mobile.PlaySound( 0x1E5 );
						*/
					}
	            }
			}
			
			FinishSequence();
		}
	}
}
