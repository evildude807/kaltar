using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Items;

namespace Server.ACC.CSS.Systems.Cleric
{
	public class GloboDeLuzSpell : SeminaristaSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Globo de Luz", 
		                                                "Globo de Luz",
		                                                //SpellCircle.Third,
		                                                212,
		                                                9041
		                                               );

        public override SpellCircle Circle {
            get { return SpellCircle.First; }
        }
		
		
		public override int RequiredTithing{ get{ return 5; } }
		public override double RequiredSkill{ get{ return 0.0; } }
		public override double CastDelay{ get{ return 3.0; } }
		public override int RequiredMana   { get{ return 20; } }
		
		public GloboDeLuzSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}

		private static Hashtable m_Table = new Hashtable();

		public static Hashtable Table{ get{ return m_Table; } }
		
		public override bool CheckCast() {
			BaseCreature check = (BaseCreature)m_Table[Caster];

			if ( check != null && !check.Deleted )
			{
				Caster.SendLocalizedMessage( 1061605 ); // You already have a familiar.
				return false;
			}

			return base.CheckCast();
		}		
		
		public override void OnCast() {
			
			if ( CheckSequence() ) {
				try {
					BaseCreature bc = (BaseCreature)Activator.CreateInstance( typeof( GloboDeLuzFamiliar ) );

					if ( BaseCreature.Summon( bc, Caster, Caster.Location, -1, TimeSpan.FromDays( 1.0 ) ) ) {

						Caster.FixedParticles( 0x3728, 1, 10, 9910, EffectLayer.Head );
						bc.PlaySound( bc.GetIdleSound() );
						
						GloboDeLuzSpell.Table[Caster] = bc;
					}
				}
				catch {
				}
			}

			FinishSequence();
		}		
		
	}
}
