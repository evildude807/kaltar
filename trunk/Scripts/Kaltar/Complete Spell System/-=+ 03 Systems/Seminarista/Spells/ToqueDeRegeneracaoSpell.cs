using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Cleric
{
	public class ToqueDeRegeneracaoSpell : SeminaristaSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
                                                        "Toque da Regeneração", 
		                                                "Toque da Regeneração",
		                                                //SpellCircle.Third,
		                                                212,
		                                                9041
		                                               );

        public override SpellCircle Circle { get { return SpellCircle.First; } }
		public override int RequiredTithing{ get{ return 15; } }
		public override double RequiredSkill{ get{ return 20.0; } }
		public override double CastDelay{ get{ return 3.0; } }
		public override int RequiredMana   { get{ return    20; } }

        //armazena todos os jogadores sobre o efeito da magia
        private static Dictionary<Mobile, Mobile> contemMagia = new Dictionary<Mobile, Mobile>();
		
		/**
		 * True se o alvo esta sobre efeito de cura. 
		 */
		public static bool sobreEfeitoCura(Mobile alvo) {
            return contemMagia.ContainsKey(alvo);
		}
		
		/*
		 * Remove o efeito de cura. 
		 */ 
		public static void removerEfeito( Mobile m ) {
            contemMagia.Remove(m);
		}

		public static void adicionarEfeito( Mobile m) {
            contemMagia.Add(m, m);
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
			else if ( sobreEfeitoCura( m ) ){
				Caster.LocalOverheadMessage( MessageType.Regular, 0x481, false, "O alvo já esta sobre o efeito de magia de cura." );
			}
			else if ( CheckBSequence( m, false ) ) {
				SpellHelper.Turn( Caster, m );

                int duracao = (int)(Caster.Skills[DamageSkill].Value / 2);

                Timer t = new InternalTimer(m, Caster, duracao);
                t.Start();

				adicionarEfeito(m);
				
				m.PlaySound( 0x202 );
				m.FixedParticles( 0x3779, 1, 46, 9502, 5, 3, EffectLayer.Waist );
				m.SendMessage( "Os seus ferimentos estão sendo curados por um poder de regeneração." );
			}
			else {
				Caster.LocalOverheadMessage( MessageType.Regular, 0x481, false, "Não foi possível conjurar a magia." );
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
		
		private class InternalTimer : Timer {
			private Mobile alvo;
			private Mobile caster;
			private BuffInfo bf;
            private int duracao;

			public InternalTimer( Mobile m, Mobile from, int duracao ) : base( TimeSpan.FromSeconds( 1 ), TimeSpan.FromSeconds( 2 ), duracao ) {
				alvo = m;
				caster = from;
                this.duracao = duracao;

				bf = new BuffInfo( BuffIcon.Bless, 1075843, new TextDefinition("Toque da Regeneração"));
				BuffInfo.AddBuff( alvo,  bf);
			}

			protected override void OnTick() {
                duracao--;

                if (!alvo.CheckAlive() || alvo.Hits >= alvo.HitsMax) {
                    parar();
				}
				else {
					double toHeal = 1;
	
					SpellHelper.Heal( (int)toHeal, alvo, caster, true);
					
					alvo.FixedParticles( 0x3779, 1, 62, 9923, 3, 3, EffectLayer.Waist );
				}

                if (duracao == 0)
                {
                    parar();
                }
			}

            private void parar()
            {
                removerEfeito(alvo);
                BuffInfo.RemoveBuff(alvo, bf);
                Stop();
            }
		}
	}
}
