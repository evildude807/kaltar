using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Items;

namespace Server.ACC.CSS.Systems.Cleric
{
	public class RefeicaoSpell : SeminaristaSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Criar Refeição", 
		                                                "Criar Refeição",
		                                                //SpellCircle.Third,
		                                                212,
		                                                9041
		                                               );

        public override SpellCircle Circle {
            get { return SpellCircle.First; }
        }
		
		
		public override int RequiredTithing{ get{ return 5; } }
		public override double RequiredSkill{ get{ return 20.0; } }
		public override double CastDelay{ get{ return 2.0; } }
		public override int RequiredMana   { get{ return 20; } }
		
		public RefeicaoSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}

		public override void OnCast() {
			
			if ( CheckSequence() ) {
				
				Item comida = new Carrot();
				Item comida1 = new Apple();

				Caster.AddToBackpack( comida );
				Caster.AddToBackpack( comida1 );
				
				Caster.SendMessage( "A refeição foi criada e colocada na sua mochila." );

				Caster.PlaySound( 0x212 );
				Caster.PlaySound( 0x206 );

				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x376A, 1, 29, 0x47D, 2, 9962, 0 );
				Effects.SendLocationParticles( EffectItem.Create( new Point3D( Caster.X, Caster.Y, Caster.Z - 7 ), Caster.Map, EffectItem.DefaultDuration ), 0x37C4, 1, 29, 0x47D, 2, 9502, 0 );				
				
			}

			FinishSequence();
		}
	}
}
