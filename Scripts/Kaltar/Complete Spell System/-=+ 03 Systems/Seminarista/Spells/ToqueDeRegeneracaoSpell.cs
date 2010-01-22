using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Cleric
{
	public class ToqueDeRegeneracaoSpell : SeminaristaSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Toque de Regeneração", 
		                                                "Toque de Regeneração",
		                                                //SpellCircle.Third,
		                                                212,
		                                                9041
		                                               );

        public override SpellCircle Circle {
            get { return SpellCircle.First; }
        }

		public override int RequiredTithing{ get{ return 15; } }
		public override double RequiredSkill{ get{ return 20.0; } }
		public override double CastDelay{ get{ return 3.0; } }
		
		private static Hashtable tabelaEfeito = new Hashtable();
		
		/**
		 * True se o alvo esta sobre efeito de cura. 
		 */
		public static bool sobreEfeitoCura(Mobile alvo) {
			return tabelaEfeito.Contains(alvo);
		}
		
		/*
		 * Remove o efeito de cura. 
		 */ 
		public static void removerEfeito( Mobile m ) {
			Timer t = (Timer)tabelaEfeito[m];

			if ( t != null ) {
				t.Stop();
				tabelaEfeito.Remove( m );
			}
		}

		public static void adicionarEfeito( Mobile m , Timer t) {
			tabelaEfeito.Add(m, t);
		}
		
		public ToqueDeRegeneracaoSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}

		public override void OnCast() {
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m ) {
			if ( !Caster.CanSee( m ) ){
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}

			if ( sobreEfeitoCura( m ) ){
				Caster.LocalOverheadMessage( MessageType.Regular, 0x481, false, "O alvo já esta sobre o efeito de magia de cura." );
			}

			else if ( CheckBSequence( m, false ) )
			{
				SpellHelper.Turn( Caster, m );

				Timer t = new InternalTimer( m, Caster );
				t.Start();
				
				adicionarEfeito(m, t);
				
				m.PlaySound( 0x202 );
				m.FixedParticles( 0x376A, 1, 62, 9923, 3, 3, EffectLayer.Waist );
				m.FixedParticles( 0x3779, 1, 46, 9502, 5, 3, EffectLayer.Waist );
				m.SendMessage( "Os seus ferimentos estão sendo curados por um poder de regeneração." );
			}

			FinishSequence();
		}

		private class InternalTarget : Target {
			private ToqueDeRegeneracaoSpell m_Owner;

			public InternalTarget( ToqueDeRegeneracaoSpell owner ) : base( 1, false, TargetFlags.Beneficial ) {
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
		
		private class InternalTimer : Timer
		{
			private Mobile dest;
			private Mobile source;
			private DateTime NextTick;
			private DateTime Expire;

			public InternalTimer( Mobile m, Mobile from ) : base( TimeSpan.FromSeconds( 1 ), TimeSpan.FromSeconds( 1 ) ) {
				dest = m;
				source = from;
				Priority = TimerPriority.FiftyMS;
				
				Expire = DateTime.Now + TimeSpan.FromSeconds( 30.0 );
			}

			protected override void OnTick()
			{
				if ( !dest.CheckAlive() ) {
					Stop();
					removerEfeito( dest );
				}

				if ( DateTime.Now < NextTick ) {
					return;
				}

				if ( DateTime.Now >= NextTick )	{
					double heal = Utility.RandomMinMax( 6, 9 ) + source.Skills[SkillName.Magery].Value / 50.0;
					heal *= ClericDivineFocusSpell.GetScalar( source );

					dest.Heal( (int)heal );

					dest.PlaySound( 0x202 );
					dest.FixedParticles( 0x376A, 1, 62, 9923, 3, 3, EffectLayer.Waist );
					dest.FixedParticles( 0x3779, 1, 46, 9502, 5, 3, EffectLayer.Waist );
					NextTick = DateTime.Now + TimeSpan.FromSeconds( 4 );
				}

				if ( DateTime.Now >= Expire ) {
					Stop();
					removerEfeito(dest);
				}
			}
		}
	}
}
