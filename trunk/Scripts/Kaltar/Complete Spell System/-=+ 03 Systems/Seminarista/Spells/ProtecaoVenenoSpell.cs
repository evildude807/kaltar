using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Cleric
{
	public class ProtecaoVenenoSpell : SeminaristaSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Proteção Veneno",
                                                        "Proteção Veneno",
		                                                //SpellCircle.Third,
		                                                212,
		                                                9041
		                                               );

        //armazena todos os jogadores sobre o efeito da magia
        private static Dictionary<Mobile, Mobile> contemMagia = new Dictionary<Mobile, Mobile>();
		
        public override SpellCircle Circle { get { return SpellCircle.First; } }
		public override int RequiredTithing{ get{ return 5; } }
		public override double RequiredSkill{ get{ return 20.0; } }
		public override double CastDelay{ get{ return 5.0; } }
		public override int RequiredMana   { get{ return 30; } }
		
		public ProtecaoVenenoSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}

		/**
		 * True se o alvo esta sobre efeito de cura. 
		 */
		public static bool sobreEfeito(Mobile alvo) {
            return contemMagia.ContainsKey(alvo);
		}
		
		/*
		 * Remove o efeito de cura. 
		 */ 
		public static void removerEfeito( Mobile m ) {
            contemMagia.Remove(m);
		}

		public static void adicionarEfeito(Mobile m) {
            contemMagia.Add(m, m);
		}
		
		public override void OnCast() {
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m ) {
			
			if ( !Caster.CanSee( m ) ) {
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			if ( sobreEfeito( m ) ){
				Caster.LocalOverheadMessage( MessageType.Regular, 0x481, false, "O alvo já esta sobre o efeito de magia Proteção Veneno." );
			}
			else if ( CheckBSequence( m, false ) ) {
				SpellHelper.Turn( Caster, m );
				
                //proteção
				double valorSkill = Caster.Skills[DamageSkill].Value;

                int bonus = (int)(valorSkill / 10);
                int protecao = Utility.RandomMinMax(Math.Max(1, bonus - 3), bonus + Utility.Random(5));
				
                //duracao
				TimeSpan duracao = TimeSpan.FromMinutes(Caster.Skills[CastSkill].Value / 10 + Utility.Random(2));

                //adicona a resistencia
                ResistanceMod[] resistencias = new ResistanceMod[1];

                resistencias[0] = new ResistanceMod(ResistanceType.Poison, protecao);

                foreach (ResistanceMod resis in resistencias) {
                    Caster.AddResistanceMod(resis);
                }
			    
                //efeito
				m.PlaySound( 0x202 );
				m.FixedParticles( 0x373A, 10, 15, 5012, 0x450, 3, EffectLayer.Waist );
				m.SendMessage( "Você esta sobre o efeito da magia Proteção Veneno" );
				
				adicionarEfeito(m);

                Timer t = new InternalTimer(m, resistencias, duracao);
                t.Start();
			}

			FinishSequence();
		}

		private class InternalTarget : Target {
			private ProtecaoVenenoSpell m_Owner;

			public InternalTarget( ProtecaoVenenoSpell owner ) : base( 1, false, TargetFlags.Beneficial ) {
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
            private ResistanceMod[] resistencias;
			private BuffInfo bf;

            public InternalTimer(Mobile m, ResistanceMod[] resistencias, TimeSpan delay ) : base( delay ) {
				alvo = m;
                this.resistencias = resistencias;
				
				bf = new BuffInfo( BuffIcon.Bless, 1075843, new TextDefinition("Proteção Veneno"));
				BuffInfo.AddBuff( m,  bf);
			}

			public void DoExpire() {

                foreach (ResistanceMod resis in resistencias)
                {
                    alvo.RemoveResistanceMod(resis);
                }

				BuffInfo.RemoveBuff( alvo,  bf);
				removerEfeito( alvo );
                alvo.SendMessage("O efeito da magia Toque de Resistência acabou.");
				Stop();
			}

			protected override void OnTick() {
        		DoExpire();
			}

		}		
	}
}
